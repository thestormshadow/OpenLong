using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Qualifying.States.UserQualifier;
using Long.Network.Packets;
using Serilog;

namespace Long.Module.Qualifying.Network
{
    public sealed class MsgQualifierInteractive : MsgBase<GameClient>
    {
		private static readonly ILogger logger = Log.ForContext<MsgQualifierInteractive>();

		public InteractionType Interaction { get; set; }
        public int Option { get; set; }
        public uint Identity { get; set; }
        public string Name { get; set; } = "";
        public int Rank { get; set; }
        public int Profession { get; set; }
        public int Unknown40 { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Interaction = (InteractionType)reader.ReadInt32();
            Option = reader.ReadInt32();
            Identity = reader.ReadUInt32();
            Name = reader.ReadString(16);
            Rank = reader.ReadInt32();
            Profession = reader.ReadInt32();
            Unknown40 = reader.ReadInt32();
            Points = reader.ReadInt32();
            Level = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgQualifyingInteractive);
            writer.Write((uint)Interaction);
            writer.Write((uint)Option);
            writer.Write((uint)Identity);
			writer.Write((uint)0);
			writer.Write(Name, 20);
			writer.Write((uint)Profession);
			writer.Write((uint)Rank);
			writer.Write((uint)Points);
			writer.Write((uint)Level);
            
            
            return writer.ToArray();
        }

        public enum InteractionType
        {
            Inscribe,
            Unsubscribe,
            Countdown,
            Accept,
            GiveUp,
            BuyArenaPoints,
            Match,
            YouAreKicked,
            StartTheFight,
            Dialog,
            EndDialog,
            ReJoin
        }

        public enum QualifierDialogButton : uint
        {
            Lose = 3,
            Win = 1,
            DoGiveUp = 2,
            Accept = 1,
            MatchOff = 3,
            MatchOn = 5,
            SignUp = 0
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = RoleManager.GetUser(client.Character.Identity);
            if (user == null)
            {
                client.Disconnect();
                return;
            }

            var qualifier = EventManager.GetEvent<ArenaQualifier>();
            if (qualifier == null)
            {
                return;
            }

            switch (Interaction)
            {
                case InteractionType.Inscribe:
                    {
                        if (qualifier.HasUser(user.Identity) && !qualifier.IsInsideMatch(user.Identity))
                        {
                            await qualifier.UnsubscribeAsync(user.Identity);
							return;
                        }

                        await qualifier.InscribeAsync(user);
                        break;
                    }

                case InteractionType.Unsubscribe:
                    {
                        await qualifier.UnsubscribeAsync(user.Identity); // no checks because user may be for some reason out of the event...

						break;
                    }

                case InteractionType.Accept:
                    {
                        ArenaQualifierUserMatch match = qualifier.FindMatch(user.Identity);
                        if (match == null)
                        {
                            await qualifier.UnsubscribeAsync(user.Identity);
                            return;
                        }

                        if (match.InvitationExpired)
                        {
                            // do nothing, because thread may remove with defeat for default
                            return;
                        }

                        if (Option == 1)
                        {
                            if (match.Player1.Identity == user.Identity)
                            {
                                if (match.Accepted1)
                                {
                                    return;
                                }

                                match.Accepted1 = true;
                            }
                            else if (match.Player2.Identity == user.Identity)
                            {
                                if (match.Accepted2)
                                {
                                    return;
                                }

                                match.Accepted2 = true;
                            }

                            if (match.Accepted1 && match.Accepted2)
                            {
                                await match.StartAsync();
                            }
                        }
                        else
                        {
                            await match.FinishAsync(null, user);
                        }
                        break;
                    }

                case InteractionType.GiveUp:
                    {
                        ArenaQualifierUserMatch match = qualifier.FindMatchByMap(user.MapIdentity);
                        if (match == null ||
                            !match.IsRunning) // check if running, because if other player gave up first it may not happen twice
                        {
                            await qualifier.UnsubscribeAsync(user.Identity);
                            return;
                        }

                        match.DoGiveUp(user.Identity);
                        break;
                    }

                case InteractionType.BuyArenaPoints:
                    {
                        if (user.QualifierPoints > 0)
                        {
                            return;
                        }

                        if (!await user.SpendMoneyAsync(ArenaQualifier.PRICE_PER_1500_POINTS, true))
                        {
                            return;
                        }

                        user.QualifierPoints += 1500;
                        break;
                    }

                case InteractionType.ReJoin:
                    {
                        await qualifier.UnsubscribeAsync(user.Identity);
                        await qualifier.InscribeAsync(user);
                        break;
                    }

                default:
                    {
                        await client.SendAsync(this);
                        if (client.Character.IsPm())
                        {
                            await client.SendAsync(new MsgTalk(TalkChannel.Service,
                                                               $"Missing packet {Type}, Action {Interaction}, Length {Length}"));
                        }

                        logger.Warning("Missing packet {0}, Action {1}, Length {2}\n{3}",
                                                Type, Interaction, Length, PacketDump.Hex(Encode()));
                        break;
                    }
            }

            await ArenaQualifier.SendArenaInformationAsync(user);
            await user.SendAsync(MsgQualifierFightersList.CreateMsg());
        }
    }
}
