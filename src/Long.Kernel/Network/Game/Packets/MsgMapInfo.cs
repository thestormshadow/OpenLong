using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgMapInfo : MsgBase<GameClient>
    {
        public MsgMapInfo(uint mapId, uint mapDoc, ulong flags)
        {
            MapIdentity = mapId;
            MapDoc = mapDoc;
            Flags = flags;
        }

        public uint MapIdentity { get; set; }
        public uint MapDoc { get; set; }
        public ulong Flags { get; set; }

        /// <summary>
        ///     Encodes the packet structure defined by this message class into a byte packet
        ///     that can be sent to the client. Invoked automatically by the client's send
        ///     method. Encodes using byte ordering rules interoperable with the game client.
        /// </summary>
        /// <returns>Returns a byte packet of the encoded packet.</returns>
        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgMapInfo);
            writer.Write(MapIdentity);
            writer.Write(MapDoc);
            writer.Write(Flags);
            return writer.ToArray();
        }
    }
}
