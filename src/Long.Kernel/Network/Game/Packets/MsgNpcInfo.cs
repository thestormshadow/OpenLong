using Long.Kernel.Managers;
using Long.Kernel.Scripting.Action;
using Long.Kernel.States.User;
using Long.Network.Packets;
using System.Security.Cryptography.X509Certificates;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgNpcInfo : MsgBase<GameClient>
    {
        public int Timestamp { get; set; }
        public uint Identity { get; set; }
        public ushort PosX { get; set; }
        public ushort PosY { get; set; }
        public ushort Lookface { get; set; }
        public ushort NpcType { get; set; }
        public ushort Sort { get; set; }
        public uint ShopID { get; set; }
        public uint ServerId { get; set; }

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
            Timestamp = reader.ReadInt32(); // 4
            Identity = reader.ReadUInt32(); // 8
            ShopID = reader.ReadUInt32(); // 12
            PosX = reader.ReadUInt16(); // 16
            PosY = reader.ReadUInt16(); // 18
            Lookface = reader.ReadUInt16(); // 20
            NpcType = reader.ReadUInt16(); // 22
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
            writer.Write((ushort)PacketType.MsgNpcInfo);
            writer.Write(Timestamp); // 4
            writer.Write(Identity); // 8
            writer.Write(ShopID); // 12
            writer.Write(PosX); // 16
            writer.Write(PosY); // 18
            writer.Write(Lookface); // 20            
            writer.Write(NpcType); // 22
            writer.Write(0);
            writer.Write(ServerId);
            return writer.ToArray();
        }

        public override Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (user.CurrentServer.HasValue && user.CurrentServerID != RealmManager.ServerIdentity)
            {
                return user.SendCrossMsgAsync(this);
            }
            return GameAction.ExecuteActionAsync(user.InteractingItem, user, null, null,
                $"{PosX} {PosY} {Lookface} {Identity} {NpcType}");
        }
    }
}
