using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgName : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgName>();

        public enum StringAction : byte
        {
            None = 0,
            Fireworks,
            CreateGuild,
            Guild,
            ChangeTitle,
            DeleteRole = 5,
            Mate,
            QueryNpc,
            Wanted,
            MapEffect,
            RoleEffect = 10,
            MemberList,
            KickoutGuildMember,
            QueryWanted,
            QueryPoliceWanted,
            PoliceWanted = 15,
            QueryMate,
            AddDicePlayer,
            DeleteDicePlayer,
            DiceBonus,
            PlayerWave = 20,
            SetAlly,
            SetEnemy,
            CheatInfo = 23, // %caseID 0 0
            DelScreenEffect,
            WhisperWindowInfo = 26,
            BodyEffect,
            AddServerName = 60,
            CurrServerName = 61,
            SetOtherFamilyName = 100
        }

        public List<string> Strings = new();
        public int Timestamp { get; set; }
        public uint Identity { get; set; }

        public ushort X
        {
            get => (ushort)(Identity - (Y << 16));
            set => Identity = (uint)((Y << 16) | value);
        }

        public ushort Y
        {
            get => (ushort)(Identity >> 16);
            set => Identity = (uint)(value << 16) | Identity;
        }

        public StringAction Action { get; set; }

        /// <summary>
        ///     Decodes a byte packet into the packet structure defined by this message class.
        ///     Should be invoked to structure data from the client for processing. Decoding
        ///     follows TQ Digital's byte ordering rules for an all-binary protocol.
        /// </summary>
        /// <param name="bytes">Bytes from the packet processor or client socket</param>
        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Timestamp = reader.ReadInt32();
            Identity = reader.ReadUInt32();
            Action = (StringAction)reader.ReadByte();
            Strings = reader.ReadStrings();
        }

        /// <summary>
        ///     Encodes the packet structure defined by this message class into a byte packet
        ///     that can be sent to the client. Invoked automatically by the client's send
        ///     method. Encodes using byte ordering rules interoperable with the game client.
        /// </summary>
        /// <returns>Returns a byte packet of the encoded packet.</returns>
        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgName);
            writer.Write(Timestamp = Environment.TickCount); // 4
            writer.Write(Identity);      // 8
            writer.Write((byte)Action); // 12
            writer.Write(Strings);       // 13
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            Character targetUser;

            if (user.CurrentServer.HasValue && user.CurrentServerID != RealmManager.ServerIdentity)
            {
                await user.SendCrossMsgAsync(this);
                return;
            }

            switch (Action)
            {
                case StringAction.QueryMate:
                    {
                        targetUser = RoleManager.GetUser(Identity);
                        if (targetUser == null)
                        {
                            return;
                        }

                        Strings[0] = targetUser.MateName;
                        await user.SendAsync(this);
                        break;
                    }

                case StringAction.Guild:
                    {
                        ISyndicate syndicate = SyndicateManager.GetSyndicate((int)Identity);
                        if (syndicate == null)
                        {
                            return;
                        }

                        Strings.Add(syndicate.Name);
                        await user.SendAsync(this);
                        break;
                    }

                case StringAction.MemberList:
                    {
                        if (user.Syndicate == null)
                        {
                            return;
                        }

                        await user.Syndicate.SendMembersAsync((int)Identity, user);
                        break;
                    }

                case StringAction.WhisperWindowInfo:
                    {
                        if (Strings.Count == 0)
                        {
                            await client.SendAsync(this);
                            return;
                        }

                        targetUser = RoleManager.GetUser(Strings[0]);
                        if (targetUser == null)
                        {
                            await user.SendAsync(this);
                            return;
                        }

                        Strings.Add($"{targetUser.Identity} {targetUser.Level} {targetUser.BattlePower} #{targetUser.SyndicateName} #{targetUser.FamilyName} {targetUser.MateName} {(int)(targetUser.NobilityRank)} {targetUser.Gender}");
                        await user.SendAsync(this);
                        break;
                    }

                case StringAction.AddServerName:
                    {
                        Strings.Clear();
                        Strings.Add(user.CurrentServerName);
                        await user.SendAsync(this);
                        break;
                    }

                default:
                    {
                        logger.Warning("MsgName:{0} subtype is unhandled", Action);
                        break;
                    }
            }
        }
    }
}
