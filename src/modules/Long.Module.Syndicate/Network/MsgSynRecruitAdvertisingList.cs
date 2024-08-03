using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Network.Packets;

namespace Long.Module.Syndicate.Network
{
    public sealed class MsgSynRecruitAdvertisingList : MsgBase<GameClient>
    {

        public int StartIndex { get; set; }
        public int TotalRecords { get; set; }
        public int CurrentPageIndex { get; set; }
        public int Unknown { get; set; }
        public List<AdvertisingStruct> Advertisings { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            StartIndex = reader.ReadInt32();
            TotalRecords = reader.ReadInt32();
            CurrentPageIndex = reader.ReadInt32();
            Unknown = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgSynRecruitAdvertisingList);
            writer.Write(StartIndex);
            writer.Write(Advertisings.Count);
            writer.Write(TotalRecords);
            writer.Write(CurrentPageIndex);
            writer.Write(Unknown);
            foreach (var adv in Advertisings)
            {
                writer.Write(adv.Identity);
                writer.Write(adv.Description, 255);
                writer.Write(adv.Name, 36);
                writer.Write(adv.LeaderName, 17);
                writer.Write(adv.Level);
                writer.Write(adv.Count);
                writer.Write(adv.Funds);
                writer.Write((ushort)(adv.AutoJoin ? 1 : 0));
                writer.Write(adv.DenyProfession);
                writer.Write(adv.Unknown332);
                writer.Write(adv.Unknown336);
                writer.Write(adv.Unknown340);
            }
            return writer.ToArray();
        }

        public override Task ProcessAsync(GameClient client)
        {
            return ModuleManager.SyndicateManager.SubmitAdvertisingListAsync(client.Character, StartIndex);
        }

        public struct AdvertisingStruct
        {
            public uint Identity { get; set; } // 0
            public string Description { get; set; } // 4 [255]
            public string Name { get; set; } // 259 [36]
            public string LeaderName { get; set; } // 295 [17]
            public int Level { get; set; } // 312
            public int Count { get; set; } // 316
            public long Funds { get; set; } // 320
            public bool AutoJoin { get; set; } // 328
            public ushort DenyProfession { get; set; } // 330
            public int Unknown332 { get; set; }
            public int Unknown336 { get; set; }
            public int Unknown340 { get; set; }
        }
    }
}
