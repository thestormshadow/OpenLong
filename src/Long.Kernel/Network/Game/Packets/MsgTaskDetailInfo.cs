using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgTaskDetailInfo : MsgBase<GameClient>
    {
        public uint Identity { get; set; }
        public uint TaskIdentity { get; set; }
        public int Data0 { get; set; }
        public int Data1 { get; set; }
        public int Data2 { get; set; }
        public int Data3 { get; set; }
        public int Data4 { get; set; }
        public int Data5 { get; set; }
        public int Data6 { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Identity = reader.ReadUInt32();
            TaskIdentity = reader.ReadUInt32();
            Data0 = reader.ReadInt32();
            Data1 = reader.ReadInt32();
            Data2 = reader.ReadInt32();
            Data3 = reader.ReadInt32();
            Data4 = reader.ReadInt32();
            Data5 = reader.ReadInt32();
            Data6 = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgTaskDetailInfo);
            writer.Write(Identity);
            writer.Write(TaskIdentity);
            writer.Write(Data0);
            writer.Write(Data1);
            writer.Write(Data2);
            writer.Write(Data3);
            writer.Write(Data4);
            writer.Write(Data5);
            writer.Write(Data6);
            return writer.ToArray();
        }

        public override Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            var taskDetail =  user.TaskDetail.QueryTaskData(TaskIdentity);
            if (taskDetail != null) 
            {
                Data0 = taskDetail.Data1;
                Data1 = taskDetail.Data2;
                Data2 = taskDetail.Data3;
                Data3 = taskDetail.Data4;
                Data4 = taskDetail.Data5;
                Data5 = taskDetail.Data6;
                Data6 = taskDetail.Data7;
            }
            return client.SendAsync(this);
        }
    }
}
