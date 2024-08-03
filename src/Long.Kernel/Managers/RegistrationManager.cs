#if DEBUG
#define DEBUG_PM_ONLY
#endif

using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Modules;
using Long.Kernel.Network.Game;
using Long.Kernel.States;
using static Long.Kernel.States.User.Character;
using static Long.Kernel.Network.Game.Packets.MsgTalk;
using Long.Kernel.States.Items;
using Long.Kernel.Database;
using System.Collections.Concurrent;
using Long.Kernel.Network.Login;
using Long.Kernel.Network.Login.Packets;

namespace Long.Kernel.Managers
{
    public sealed class RegistrationManager : IRegistrationManager
    {
        private static readonly ILogger logger = Log.ForContext<RegistrationManager>();

        private static readonly ushort[] StartX = [300, 293, 309, 298, 322, 334, 309];
        private static readonly ushort[] StartY = [278, 294, 284, 265, 265, 278, 296];

        // Registration constants
        private static readonly byte[] Hairstyles =
        [
            10, 11, 13, 14, 15, 24, 30, 35, 37, 38, 39, 40
        ];

        private readonly ConcurrentDictionary<string, RegistrationQueue> registrationList = new();

        public static List<uint> Registration { get; } = new();

        public async Task RegisterAsync(GameClient client, uint token, int profession, ushort mesh, string name)
        {
            // Validate that the player has access to character creation
            if (client.Creation == null || token != client.Creation.Token || !Registration.Contains(token))
            {
                await client.SendAsync(RegisterInvalid);
                client.Disconnect();
                return;
            }

            var loginClient = LoginClientSocket.Instance;
            if (loginClient == null)
            {
                logger.Fatal("!!! Registration failed due to Account Server not online !!!");
                await client.SendAsync(RegisterServerNotSetUp);
                return;
            }

            // Check character name availability
            if (await UserRepository.ExistsAsync(name))
            {
                await client.SendAsync(RegisterNameTaken);
                return;
            }

            if (!RoleManager.IsValidName(name))
            {
                await client.SendAsync(RegisterInvalid);
                return;
            }

            BaseClassType baseClass;
            switch (profession)
            {
                case 0:
                case 1:
                    {
                        baseClass = BaseClassType.Taoist;
                        break;
                    }
                case 2:
                case 3:
                    {
                        baseClass = BaseClassType.Trojan;
                        break;
                    }
                case 4:
                case 5:
                    {
                        baseClass = BaseClassType.Archer;
                        break;
                    }
                case 6:
                case 7:
                    {
                        baseClass = BaseClassType.Warrior;
                        break;
                    }
                case 8:
                case 9:
                    {
                        baseClass = BaseClassType.Ninja;
                        break;
                    }
                case 10:
                case 11:
                    {
                        baseClass = BaseClassType.Monk;
                        break;
                    }
                case 12:
                case 13:
                    {
                        baseClass = BaseClassType.Pirate;
                        break;
                    }

                case 14:
                case 15:
                    {
                        baseClass = BaseClassType.DragonWarrior;
                        break;
                    }

                default:
                    {
                        await client.SendAsync(RegisterInvalidProfession);
                        return;
                    }
            }

            // Validate character creation input
            if (!Enum.IsDefined(typeof(BodyType), mesh))
            {
                await client.SendAsync(RegisterInvalidBody);
                return;
            }

            DbPointAllot allot = PointAllotRepository.Get((ushort)((int)baseClass / 10), 1) ?? new DbPointAllot
            {
                Strength = 4,
                Agility = 6,
                Vitality = 12,
                Spirit = 0
            };

#if DEBUG_PM_ONLY
            if (name.Length + 4 > 15)
            {
                name = name[..11];
            }
            name += "[PM]";
#endif

            int posIdx = await NextAsync(StartX.Length) % StartX.Length;

            // Create the character
            var character = new DbUser
            {
                AccountIdentity = client.Creation.AccountId,
                Name = name,
                Mate = 0,
                Mesh = mesh,

                MapID = 1002,
                X = StartX[posIdx],
                Y = StartY[posIdx],

                Profession = (byte)baseClass,
                Level = 1,
                AutoAllot = 1,
                Silver = 10_000,

                Strength = allot.Strength,
                Agility = allot.Agility,
                Vitality = allot.Vitality,
                Spirit = allot.Spirit,
                HealthPoints =
                    (ushort)(allot.Strength * 3
                              + allot.Agility * 3
                              + allot.Spirit * 3
                              + allot.Vitality * 24),
                ManaPoints = (ushort)(allot.Spirit * 5),

                FirstLogin = (uint)UnixTimestamp.Now,
                HeavenBlessing = (uint)UnixTimestamp.FromDateTime(DateTime.Now.AddDays(30))
            };

            // Generate a random look for the character
            var body = (BodyType)mesh;
            uint lookFace = 0;
            switch (body)
            {
                case BodyType.AgileFemale:
                case BodyType.MuscularFemale:
                    {
                        lookFace = baseClass switch
                        {
                            BaseClassType.Ninja => 291,
                            BaseClassType.Monk => 300,
                            BaseClassType.Pirate => 347,
                            BaseClassType.DragonWarrior => 355,
                            _ => 201,
                        };
                        break;
                    }
                default:
                    {
                        lookFace = baseClass switch
                        {
                            BaseClassType.Ninja => 103,
                            BaseClassType.Monk => 109,
                            BaseClassType.Pirate => 134,
                            BaseClassType.DragonWarrior => 164,
                            _ => 1,
                        };
                        break;
                    }
            }

            character.Mesh += lookFace * 10000;
            character.Hairstyle = (ushort)(await NextAsync(3, 9) * 100 + Hairstyles[await NextAsync(0, Hairstyles.Length)]);

            var to = new TimeOut();
            to.Startup(10);

            Guid requestId = Guid.NewGuid();
            RegistrationQueue registration = new RegistrationQueue
            {
                RequestID = requestId.ToString(),
                GameClient = client,
                Token = token,
                User = character,
                TimeOut = to
            };
            registrationList.TryAdd(requestId.ToString(), registration);

            await loginClient.Client.SendAsync(new MsgLoginRequestUserId
            {
                Data = new Long.Network.Packets.Login.MsgLoginRequestUserId<LoginServer>.LoginRequestUserIdPB
                {
                    AccountID = (int)registration.GameClient.AccountIdentity,
                    RequestID = registration.RequestID
                }
            });
        }

        public async Task ResumeRegistrationAsync(string requestId, uint idUser)
        {
            if (!registrationList.TryRemove(requestId, out var request))
            {
                logger.Warning("Could not find registration {0} for newly created user {1}.", requestId, idUser);
                return;
            }

            DbUser character = request.User;
            character.Identity = idUser;

            GameClient client = request.GameClient;
            uint token = request.Token;

            if (idUser == 0)
            {
                await client.SendAsync(RegisterTryAgain);
                return;
            }

            try
            {
                // Save the character and continue with login
                await UserRepository.CreateAsync(character);
                Registration.Remove(client.Creation.Token);

                await GenerateInitialEquipmentAsync(character);

                var args = new TransferAuthArgs
                {
                    AccountID = client.AccountIdentity,
                    AuthorityID = client.AuthorityLevel,
                    IPAddress = client.IpAddress
                };
                RoleManager.SaveLoginRequest(token.ToString(), args);
            }
            catch
            {
                await client.SendAsync(RegisterTryAgain);
                return;
            }

            await client.SendAsync(RegisterOk);
        }

        private static async Task GenerateInitialEquipmentAsync(DbUser user)
        {
            DbNewbieInfo info = NewbieInfoRepository.Get((uint)(user.Profession / 10 * 10));
            if (info == null)
            {
                return;
            }

            if (info.LeftHand != 0)
            {
                await CreateItemAsync(info.LeftHand, user.Identity, Item.ItemPosition.LeftHand);
            }

            if (info.RightHand != 0)
            {
                await CreateItemAsync(info.RightHand, user.Identity, Item.ItemPosition.RightHand);
            }

            if (info.Shoes != 0)
            {
                await CreateItemAsync(info.Shoes, user.Identity, Item.ItemPosition.Boots);
            }

            if (info.Headgear != 0)
            {
                await CreateItemAsync(info.Headgear, user.Identity, Item.ItemPosition.Headwear);
            }

            if (info.Necklace != 0)
            {
                await CreateItemAsync(info.Necklace, user.Identity, Item.ItemPosition.Necklace);
            }

            if (info.Armor != 0)
            {
                await CreateItemAsync(info.Armor, user.Identity, Item.ItemPosition.Armor);
            }

            if (info.Ring != 0)
            {
                await CreateItemAsync(info.Ring, user.Identity, Item.ItemPosition.Ring);
            }

            if (info.Item0 != 0)
            {
                for (var i = 0; i < info.Number0; i++)
                {
                    await CreateItemAsync(info.Item0, user.Identity, Item.ItemPosition.Inventory);
                }
            }

            if (info.Item1 != 0)
            {
                for (var i = 0; i < info.Number1; i++)
                {
                    await CreateItemAsync(info.Item1, user.Identity, Item.ItemPosition.Inventory);
                }
            }

            if (info.Item2 != 0)
            {
                for (var i = 0; i < info.Number2; i++)
                {
                    await CreateItemAsync(info.Item2, user.Identity, Item.ItemPosition.Inventory);
                }
            }

            if (info.Item3 != 0)
            {
                for (var i = 0; i < info.Number3; i++)
                {
                    await CreateItemAsync(info.Item3, user.Identity, Item.ItemPosition.Inventory);
                }
            }

            if (info.Item4 != 0)
            {
                for (var i = 0; i < info.Number4; i++)
                {
                    await CreateItemAsync(info.Item4, user.Identity, Item.ItemPosition.Inventory);
                }
            }

            if (info.Item5 != 0)
            {
                for (var i = 0; i < info.Number5; i++)
                {
                    await CreateItemAsync(info.Item5, user.Identity, Item.ItemPosition.Inventory);
                }
            }

            if (info.Item6 != 0)
            {
                for (var i = 0; i < info.Number6; i++)
                {
                    await CreateItemAsync(info.Item6, user.Identity, Item.ItemPosition.Inventory);
                }
            }

            if (info.Item7 != 0)
            {
                for (var i = 0; i < info.Number7; i++)
                {
                    await CreateItemAsync(info.Item7, user.Identity, Item.ItemPosition.Inventory);
                }
            }

            if (info.Item8 != 0)
            {
                for (var i = 0; i < info.Number8; i++)
                {
                    await CreateItemAsync(info.Item8, user.Identity, Item.ItemPosition.Inventory);
                }
            }

            if (info.Item9 != 0)
            {
                for (var i = 0; i < info.Number9; i++)
                {
                    await CreateItemAsync(info.Item9, user.Identity, Item.ItemPosition.Inventory);
                }
            }

            if (info.Magic0 != 0)
                await CreateMagicAsync(user.Identity, (ushort)info.Magic0);
            if (info.Magic1 != 0)
                await CreateMagicAsync(user.Identity, (ushort)info.Magic1);
            if (info.Magic2 != 0)
                await CreateMagicAsync(user.Identity, (ushort)info.Magic2);
            if (info.Magic3 != 0)
                await CreateMagicAsync(user.Identity, (ushort)info.Magic3);
        }

        private static async Task CreateItemAsync(uint type, uint idOwner, Item.ItemPosition position, byte add = 0,
                                           Item.SocketGem gem1 = Item.SocketGem.NoSocket,
                                           Item.SocketGem gem2 = Item.SocketGem.NoSocket,
                                           byte enchant = 0, byte reduceDmg = 0, bool monopoly = false)
        {
            DbItem item = Item.CreateEntity(type);
            if (item == null)
            {
                return;
            }

            item.Position = (byte)position;
            item.PlayerId = idOwner;
            item.AddLife = enchant;
            item.ReduceDmg = reduceDmg;
            item.Magic3 = add;
            item.Gem1 = (byte)gem1;
            item.Gem2 = (byte)gem2;
            item.Monopoly = (byte)(monopoly ? 3 : 0);
            if (await ServerDbContext.CreateAsync(item))
            {
                item.ChkSum = Item.CalculateCheckSum(item);
                await ServerDbContext.UpdateAsync(item);
            }
        }

        private static Task CreateMagicAsync(uint idOwner, ushort type, byte level = 0)
        {
            return ServerDbContext.CreateAsync(new DbMagic
            {
                Type = type,
                Level = level,
                OwnerId = idOwner
            });
        }

        public struct RegistrationQueue
        {
            public string RequestID { get; set; }
            public uint Token { get; set; }
            public GameClient GameClient { get; set; }
            public DbUser User { get; set; }
            public TimeOut TimeOut { get; set; }
        }
    }
}