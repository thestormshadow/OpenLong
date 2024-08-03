using Long.Kernel.Network.Game;
using Long.Network.Packets;

namespace Long.Module.Team.Network
{
    public sealed class MsgTeamMember : MsgBase<GameClient>
    {
        public List<TeamMember> Members = new();

        public byte Action { get; set; }
        public byte Count { get; set; }
        public byte Unknown0 { get; set; }
        public byte Unknown1 { get; set; }

        /// <summary>
        ///     Encodes the packet structure defined by this message class into a byte packet
        ///     that can be sent to the client. Invoked automatically by the client's send
        ///     method. Encodes using byte ordering rules interoperable with the game client.
        /// </summary>
        /// <returns>Returns a byte packet of the encoded packet.</returns>
        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgTeamMember);
            writer.Write(Action);
            writer.Write((byte)Members.Count);
            writer.Write(Unknown0);
            writer.Write(Unknown1);
            foreach (TeamMember member in Members)
            {
                writer.Write(member.Name, 16); // 0
                writer.Write(member.Identity); // 16
                writer.Write(member.Lookface); // 20
                writer.Write(member.MaxLife); // 24
                writer.Write(member.Life); // 28
            }

            return writer.ToArray();
        }

        public struct TeamMember
        {
            public string Name { get; set; }
            public uint Identity { get; set; }
            public uint Lookface { get; set; }
            public uint MaxLife { get; set; }
            public uint Life { get; set; }
        }

        public const byte ADD_MEMBER_B = 0, DEL_MEMBER_B = 1;
    }
}
