using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Module.Guide.Network
{
    public sealed class MsgGuideInfo : MsgBase<GameClient>
    {
        public ushort Blessing { get; set; }
        public ushort Composition { get; set; }
        public uint EnroleDate { get; set; }
        public ulong Experience { get; set; }
        public byte[] Fill57 { get; set; } = new byte[3];
        public uint Identity { get; set; }
        public bool IsOnline { get; set; }
        public byte Level { get; set; }
        public uint Mesh { get; set; }

        public RequestMode Mode { get; set; }
        public List<string> Names { get; set; } = new();
        public ushort PkPoints;
        public byte Profession { get; set; }
        public uint SenderIdentity { get; set; }
        public uint SharedBattlePower { get; set; }
        public uint Syndicate { get; set; }
        public uint SyndicatePosition { get; set; }
        public uint BetrayHour { get; set; }
        public byte Unknown38 { get; set; }
        public uint SyndicateTitle { get; set; }
        public uint Unknown48 { get; set; }
        public uint Unknown52 { get; set; }
        public uint Unknown60 { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Mode = (RequestMode)reader.ReadUInt32();
            SenderIdentity = reader.ReadUInt32();
            Identity = reader.ReadUInt32();
            Mesh = reader.ReadUInt32();
            SharedBattlePower = reader.ReadUInt32();
            BetrayHour = reader.ReadUInt32();
            EnroleDate = reader.ReadUInt32();
            Level = reader.ReadByte();
            Profession = reader.ReadByte();
            PkPoints = reader.ReadUInt16();
            Syndicate = reader.ReadUInt32();
            SyndicatePosition = reader.ReadUInt32();
            SyndicateTitle = reader.ReadUInt32();
            Unknown48 = reader.ReadUInt32();
            Unknown52 = reader.ReadUInt32();
            IsOnline = reader.ReadBoolean();
            Fill57 = reader.ReadBytes(3);
            Unknown60 = reader.ReadUInt32();
            Experience = reader.ReadUInt64();
            Blessing = reader.ReadUInt16();
            Composition = reader.ReadUInt16();
            Names = reader.ReadStrings();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgGuideInfo);
            writer.Write((int)Mode); // 4
            writer.Write(SenderIdentity); // 8
            writer.Write(Identity); // 12
            writer.Write(Mesh); // 16
            writer.Write(SharedBattlePower); // 20
            writer.Write(BetrayHour); // 24
            writer.Write(EnroleDate); // 28
            writer.Write(Level); // 32
            writer.Write(Profession); // 33
            writer.Write(PkPoints); // 34
            writer.Write(Syndicate); // 36
            writer.Write(SyndicatePosition); // 40
            writer.Write(SyndicateTitle); // 44
            writer.Write(Unknown48); // 48
            writer.Write(Unknown52); // 52
            writer.Write(IsOnline); // 56
            writer.Write(Fill57); // 57
            writer.Write(Unknown60); // 60
            writer.Write(Experience); // 64
            writer.Write(Blessing); // 72
            writer.Write(Composition); // 74
            writer.Write(Names); // 76
            return writer.ToArray();
        }

        public enum RequestMode
        {
            None,
            Mentor,
            Apprentice
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (Mode == RequestMode.Mentor)
            {
                if (user.Guide.Tutor != null)
                {
                    await user.Guide.Tutor.SendTutorAsync();
                }
            }
            else if (Mode == RequestMode.Apprentice)
            {
                await user.Guide.SynchroStudentsAsync();
            }
        }
    }
}
