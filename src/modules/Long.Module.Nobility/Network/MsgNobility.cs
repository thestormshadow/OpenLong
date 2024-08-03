using Long.Kernel;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Nobility;
using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Module.Peerage.Managers;
using Long.Network.Packets;

namespace Long.Module.Peerage.Network
{
    public sealed class MsgNobility : MsgBase<GameClient>
    {
        public MsgNobility()
        {
            Data = 0;
        }

        public MsgNobility(NobilityAction action, ushort maxPerPage, ushort maxPages)
        {
            Action = action;
            DataLow2 = maxPages;
            DataHigh = maxPerPage;
        }

        public NobilityAction Action { get; set; }
        public ulong Data { get; set; }

        public uint DataLow
        {
            get => (uint)(Data - ((ulong)DataHigh << 32));
            set => Data = (ulong)DataHigh << 32 | value;
        }

        public ushort DataLow1
        {
            get => (ushort)(DataLow - (DataLow2 << 16));
            set => DataLow = (uint)(DataLow2 << 16 | value);
        }

        public ushort DataLow2
        {
            get => (ushort)(DataLow >> 16);
            set => DataLow = (uint)(value << 16) | DataLow;
        }

        public uint DataHigh
        {
            get => (uint)(Data >> 32);
            set => Data = (ulong)value << 32 | Data;
        }

        public ushort DataHigh1
        {
            get => (ushort)(DataHigh - (DataHigh2 << 16));
            set => DataHigh = (uint)(DataHigh2 << 16 | value);
        }

        public ushort DataHigh2
        {
            get => (ushort)(DataHigh >> 16);
            set => DataHigh = (uint)(value << 16) | DataHigh;
        }

        public uint Data1 { get; set; }
        public uint Data2 { get; set; }
        public uint Data3 { get; set; }
        public uint Data4 { get; set; }
        public byte[] Trash { get; set; }
        public int DonationMode { get; set; }
        public int Unknown0 { get; set; }
        public int Unknown1 { get; set; }
        public int Unknown2 { get; set; }

        public List<string> Strings { get; set; } = new List<string>();
        public List<NobilityStruct> Rank { get; set; } = new List<NobilityStruct>();
        public List<RankingStruct> RemainingDonation { get; set; } = new List<RankingStruct>();

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
            Action = (NobilityAction)reader.ReadUInt32();
            Data = reader.ReadUInt64();
            Data1 = reader.ReadUInt32();
            Data2 = reader.ReadUInt32();
            Data3 = reader.ReadUInt32();
            Data4 = reader.ReadUInt32();
            Trash = reader.ReadBytes(72);
            DonationMode = reader.ReadInt32();
            Unknown0 = reader.ReadInt32();
            Unknown1 = reader.ReadInt32();
            Unknown2 = reader.ReadInt32();
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
            writer.Write((ushort)PacketType.MsgNobility);
            writer.Write((uint)Action); // 4
            if (Action == NobilityAction.QueryRemainingSilver)
            {
                foreach (var pos in RemainingDonation)
                {
                    writer.Write(pos.Donation);
                    writer.Write(uint.MaxValue);
                    writer.Write((int)pos.Position);
                }
            }
            else
            {
                writer.Write(Data); // 8
                writer.Write(Data1); // 16
                writer.Write(Data2); // 20
                writer.Write(Data3); // 24
                writer.Write(Data4); // 28
                writer.Write(new byte[72]); // 32-104?            
                writer.Write(DonationMode); // 104
                writer.Write(0); // 108
                writer.Write(0); // 112
                writer.Write(0); // 116
            }

            if (Action == NobilityAction.List)
            {
                foreach (var rank in Rank)
                {
                    writer.Write(rank.Identity);
                    writer.Write(rank.LookFace);
                    writer.Write(rank.LookFace);
                    writer.Write(rank.Name, 16);
                    writer.Write(0);
                    writer.Write(rank.Donation);
                    writer.Write((uint)rank.Rank);
                    writer.Write(rank.Position);
                }
            }
            else
            {
                writer.Write(Strings);
            }

            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            const int MIN_LEVEL = 70;
            const int MIN_DONATION = 3_000_000;
            const int MAX_DONATION = 1_000_000_000;

            Character user = client.Character;
            switch (Action)
            {
                case NobilityAction.Donate:
                    {
                        if (user.Level < MIN_LEVEL)
                        {
                            await user.SendAsync(StrRes.StrPeerageDonateErrBelowLevel);
                            return;
                        }

                        if (Data < MIN_DONATION)
                        {
                            await user.SendAsync(StrRes.StrPeerageDonateErrBelowUnderline);
                            return;
                        }

                        if (Data > MAX_DONATION)
                        {
                            await user.SendAsync(StrRes.StrPeerageDonateOverflow);
                            return;
                        }

                        if (DonationMode == 0)
                        {
                            if (!await user.SpendMoneyAsync((long)Data, true))
                            {
                                return;
                            }
                        }
                        else
                        {
                            const uint EMONEY_COEFICIENT = 50_000;
                            int emoneyCost = (int)(Data / EMONEY_COEFICIENT);
                            if (!await user.SpendBoundConquerPointsAsync(Character.EmoneyOperationType.Nobility, emoneyCost, true))
                            {
                                return;
                            }
                        }

                        await ModuleManager.NobilityManager.DonateAsync(user, Data);
                        break;
                    }

                case NobilityAction.List:
                    {
                        await ModuleManager.NobilityManager.SendRankingAsync(user, DataLow1);
                        break;
                    }

                case NobilityAction.QueryRemainingSilver:
                    {
                        if (user.NobilityRank == NobilityRank.King)
                        {
                            await user.SendAsync(this);
                            return;
                        }
                        
                        foreach (NobilityRank rank in Enum.GetValues<NobilityRank>().Where(x => x > NobilityRank.Serf))
                        {
                            RemainingDonation.Add(new RankingStruct
                            {
                                Donation = ModuleManager.NobilityManager.GetNextRankSilver(rank, user.Nobility.Donation),
                                Position = rank
                            });
                        }
                        await user.SendAsync(this);
                        break;
                    }
            }
        }

        public struct RankingStruct
        {
            public NobilityRank Position;
            public ulong Donation;
        }

        public struct NobilityStruct
        {
            public uint Identity;
            public uint LookFace;
            public string Name;
            public ulong Donation;
            public NobilityRank Rank;
            public int Position;
        }

        public enum NobilityAction : uint
        {
            None,
            Donate,
            List,
            Info,
            QueryRemainingSilver
        }
    }
}
