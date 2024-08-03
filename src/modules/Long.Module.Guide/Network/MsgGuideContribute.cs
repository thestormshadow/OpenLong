using Long.Kernel.Network.Game;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Serilog;
using static Long.Kernel.StrRes;

namespace Long.Module.Guide.Network
{
    public sealed class MsgGuideContribute : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgGuideContribute>();

        public ushort Composing { get; set; }
        public ulong Experience { get; set; }
        public ushort HeavenBlessing { get; set; }
        public uint Identity { get; set; }

        public RequestType Mode { get; set; }
        public byte[] Padding { get; set; } = new byte[12];

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Mode = (RequestType)reader.ReadUInt32();
            Identity = reader.ReadUInt32();
            Padding = reader.ReadBytes(12);
            Experience = reader.ReadUInt64();
            HeavenBlessing = reader.ReadUInt16();
            Composing = reader.ReadUInt16();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgGuideContribute);
            writer.Write((int)Mode); // 4
            writer.Write(Identity); // 8
            writer.Write(Padding); // 12
            writer.Write(Experience); // 24
            writer.Write(HeavenBlessing); // 32
            writer.Write(Composing); // 34
            return writer.ToArray();
        }

        public enum RequestType
        {
            ClaimExperience = 1,
            ClaimExperienceBalls,
            ClaimHeavenBlessing,
            ClaimItemAdd,
            Query
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            switch (Mode)
            {
                case RequestType.Query:
                    {
                        break;
                    }
                case RequestType.ClaimExperience:
                case RequestType.ClaimExperienceBalls:
                    {
                        if (user.Map != null && user.Map.IsNoExpMap())
                        {
                            return;
                        }

                        ulong expTime = Experience;
                        if (expTime == 0)
                        {
                            expTime = user.Guide.MentorExpTime;
                        }

                        if (user.Guide.MentorExpTime >= expTime)
                        {
                            await user.AwardExperienceAsync(user.CalculateExpBall((int)expTime), true);

                            user.Guide.MentorExpTime = Math.Max(0, user.Guide.MentorExpTime - expTime);
                            await user.Guide.SaveAsync();
                        }
                        break;
                    }

                case RequestType.ClaimHeavenBlessing:
                    {
                        if (user.Guide.MentorGodTime > 0)
                        {
                            await user.AddBlessingAsync(user.Guide.MentorGodTime);
                            user.Guide.MentorGodTime = 0;
                            await user.Guide.SaveAsync();
                        }

                        break;
                    }

                case RequestType.ClaimItemAdd:
                    {
                        int stoneAmount = user.Guide.MentorAddLevexp / 100;

                        if (!user.UserPackage.IsPackSpare(stoneAmount))
                        {
                            await user.SendAsync(StrYourBagIsFull);
                            return;
                        }

                        for (var i = 0; i < stoneAmount; i++)
                        {
                            if (await user.UserPackage.AwardItemAsync(Item.TYPE_STONE1))
                            {
                                user.Guide.MentorAddLevexp -= 100;
                            }
                        }

                        await user.Guide.SaveAsync();
                        break;
                    }

                default:
                    {
                        await client.SendAsync(this);
                        if (client.Character.IsPm())
                        {
                            await client.SendAsync(new MsgTalk(TalkChannel.Service, $"Missing packet {Type}, Action {Mode}, Length {Length}"));
                        }

                        logger.Warning("Missing packet {0}, Action {1}, Length {2}\n" + PacketDump.Hex(Encode()), Type, Mode, Length);
                        break;
                    }
            }

            Mode = RequestType.Query;
            Experience = (uint)user.Guide.MentorExpTime;
            Composing = user.Guide.MentorAddLevexp;
            HeavenBlessing = user.Guide.MentorGodTime;
            await client.SendAsync(this);
        }
    }
}
