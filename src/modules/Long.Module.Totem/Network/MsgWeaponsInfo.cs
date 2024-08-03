using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Network.Packets;
using static Long.Kernel.Modules.Systems.Totem.ITotem;

namespace Long.Module.Totem.Network
{
    public sealed class MsgWeaponsInfo : MsgBase<GameClient>
    {
        public List<TotemPoleStruct> Items = new();
        public uint Action { get; set; }
        public int Data1 { get; set; }
        public int Data2 { get; set; }
        public int TotemType { get; set; }
        public int TotalInscribed { get; set; }
        public int SharedBattlePower { get; set; }
        public int Enhancement { get; set; }
        public uint EnhancementExpiration { get; set; }
        public int Donation { get; set; }
        public int Count { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = reader.ReadUInt32();
            Data1 = reader.ReadInt32();
            Data2 = reader.ReadInt32();
            TotemType = reader.ReadInt32();
            TotalInscribed = reader.ReadInt32();
            SharedBattlePower = reader.ReadInt32();
            Enhancement = reader.ReadInt32();
            EnhancementExpiration = reader.ReadUInt32();
            Donation = reader.ReadInt32();
            Count = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgWeaponsInfo);
            writer.Write(Action);                // 4
            writer.Write(Data1);                 // 8
            writer.Write(Data2);                 // 12
            writer.Write(TotemType);             // 16
            writer.Write(TotalInscribed);        // 20 
            writer.Write(SharedBattlePower);     // 24
            writer.Write(Enhancement);           // 28
            writer.Write(EnhancementExpiration); // 32
            writer.Write(Donation);              // 36
            writer.Write(Count = Items.Count);   // 40
            foreach (TotemPoleStruct item in Items)
            {
                writer.Write(item.ItemIdentity);   // 0
                writer.Write(item.Position);       // 4
                writer.Write(item.PlayerName, 16); // 8
                writer.Write(item.Type);           // 24
                writer.Write(item.Quality);        // 28
                writer.Write(item.Addition);       // 29
                writer.Write(item.SocketOne);      // 30
                writer.Write(item.SocketTwo);      // 31
                writer.Write(item.BattlePower);    // 32
                writer.Write(item.Donation);       // 36
            }

            return writer.ToArray();
        }

        public struct TotemPoleStruct
        {
            public uint ItemIdentity { get; set; }
            public int Position { get; set; }
            public string PlayerName { get; set; }
            public uint Type { get; set; }
            public byte Quality { get; set; }
            public byte Addition { get; set; }
            public byte SocketOne { get; set; }
            public byte SocketTwo { get; set; }
            public int BattlePower { get; set; }
            public int Donation { get; set; }
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (user?.Syndicate?.Totem == null)
            {
                return;
            }

            ISyndicate syn = user.Syndicate;
            if (Action == 0)
            {
                if (TotemType == (int)TotemPoleType.None)
                {
                    return;
                }

                await syn.Totem.SendTotemsAsync(user, (TotemPoleType)TotemType, Data1);
            }
        }
    }
}
