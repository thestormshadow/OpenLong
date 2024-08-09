using Long.Kernel;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Qualifying.Network.States;
using Long.Network.Packets;
using Serilog;
using static Long.Module.Qualifying.Network.MsgQualifierInteractive;

namespace Long.Module.Qualifying.Network
{
    public sealed class MsgTeamArenaInteractive : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgTeamArenaInteractive>();

        public InteractionType Action { get; set; }
        public QualifierDialogButton Option { get; set; }
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
            Action = (InteractionType)reader.ReadInt32();
            Option = (QualifierDialogButton)reader.ReadInt32();
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
            writer.Write((ushort)PacketType.MsgTeamArenaInteractive);
            writer.Write((int)Action); // 4
            writer.Write((int)Option); // 8
            writer.Write(Identity); // 12
            writer.Write(Name, 16); // 16
            writer.Write(Rank); // 32
            writer.Write(Profession); // 36
            writer.Write(Unknown40); // 40
            writer.Write(Points); //44
            writer.Write(Level); // 48
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = RoleManager.GetUser(client.Character.Identity);
            if (user == null)
            {
                client.Disconnect();
                return;
            }

            var qualifier = EventManager.GetEvent<TeamArenaQualifier>();
            if (qualifier == null)
            {
                return;
            }

            switch (Action)
            {
                case InteractionType.Inscribe:
                    {
                        if (user.IsInQualifierEvent())
                        {
                            var currentEvent = user.GetCurrentEvent();
                            if (currentEvent is TeamArenaQualifier check && !check.IsInsideMatch(user.Identity))
                            {
                                await check.UnsubscribeAsync(user.Identity);
                            }
                            else
                            {
                                await user.SendAsync(StrRes.StrEventCannotEnterTwoEvents);
                            }

                            return;
                        }

                        if (qualifier.HasTeamJoined(user.Identity) && !qualifier.IsInsideMatch(user.Identity))
                        {
                            await qualifier.UnsubscribeAsync(user.Identity);
                            return;
                        }

                        await qualifier.InscribeAsync(user);
                        break;
                    }

                case InteractionType.Unsubscribe:
                    {
                        Identity = user.Identity;
                        Name = user.Name;
                        Rank = user.TeamQualifierRank;
                        Profession = user.Profession;
                        Points = (int)user.TeamQualifierPoints;
                        Level = user.Level;
                        await user.SendAsync(this);

                        await qualifier.UnsubscribeAsync(user.Identity);// no checks because user may be for some reason out of the event...
                        break;
                    }

                case InteractionType.Accept:
                    {
                        var match = qualifier.FindMatch(user.Identity);
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

                        if (match.UserHasVoted(user.Identity))
                        {
                            return;
                        }

                        if (Option == QualifierDialogButton.Win)
                        {
                            if (!match.Accept(user.Identity))
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (!match.Deny(user.Identity))
                            {
                                return;
                            }
                        }
                        break;
                    }

                case InteractionType.GiveUp:
                    {
                        var match = qualifier.FindMatchByMap(user.MapIdentity);
                        if (match == null ||
                            !match.IsRunning) // check if running, because if other player gave up first it may not happen twice
                        {
                            await qualifier.UnsubscribeAsync(user.Identity);
                            return;
                        }

                        if (match.AllUsersVoted())
                        {
                            if (match.IsAccepted())
                            {
                                await match.StartAsync();
                            }
                            else if (match.IsDenied(out var winner, out var loser))
                            {
                                await match.FinishAsync(winner, loser);
                            }
                        }
                        break;
                    }

                case InteractionType.BuyArenaPoints:
                    {
                        if (user.TeamQualifierPoints >= 1500)
                        {
                            return;
                        }

                        int price = TeamArenaQualifier.PRICE_PER_1500_POINTS - (int)(TeamArenaQualifier.PRICE_PER_1500_POINTS * (user.TeamQualifierPoints/1500d));

                        if (!await user.SpendMoneyAsync(price, true))
                        {
                            return;
                        }

                        user.TeamQualifierPoints = 1500;
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
                            await client.SendAsync(new MsgTalk(TalkChannel.Service, $"Missing packet {Type}, Action {Action}, Length {Length}"));
                        }

                        logger.Warning("Missing packet {0}, Action {1}, Length {2}\n{3}",
                                                Type, Action, Length, PacketDump.Hex(Encode()));
                        return;
                    }
            }

            await TeamArenaQualifier.SendArenaInformationAsync(user);
            await user.SendAsync(MsgTeamArenaFightingTeamList.CreateMsg());
        }
    }
}
