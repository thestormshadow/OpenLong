using Long.Kernel.Network.Game;
using Long.Network.Packets;
using static Long.Kernel.States.User.Character;

namespace Long.Module.JiangHu.Network
{
    public sealed class MsgOwnKongfuPKSetting : MsgBase<GameClient>
    {
        public JiangPkMode Mode { get; set; }

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Mode = (JiangPkMode)reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgOwnKongfuPkSetting);
            writer.Write((int)Mode);
            return writer.ToArray();
        }

        public override Task ProcessAsync(GameClient client)
        {
            client.Character.JiangPkImmunity = Mode;
            return Task.CompletedTask;
        }
    }
}
