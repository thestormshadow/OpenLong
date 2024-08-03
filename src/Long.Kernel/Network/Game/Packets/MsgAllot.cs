using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgAllot : MsgBase<GameClient>
    {
        public int Identity { get; set; } // ??
        public int Force { get; set; }
        public int Speed { get; set; }
        public int Health { get; set; }
        public int Soul { get; set; }

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
            reader.ReadInt32();
            Identity = reader.ReadInt32();
            Force = reader.ReadInt32();
            Speed = reader.ReadInt32();
            Health = reader.ReadInt32();
            Soul = reader.ReadInt32();
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
            writer.Write((ushort)PacketType.MsgAllot);
            writer.Write(Environment.TickCount);
            writer.Write(Identity);
            writer.Write(Force);
            writer.Write(Speed);
            writer.Write(Health);
            writer.Write(Soul);
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (user == null || !user.IsAlive)
            {
                return;
            }

            if (Force < 0 || Speed < 0 || Health < 0 || Soul < 0)
            {
                return;
            }

            int total = Force + Speed + Health + Soul;
            if (total <= 0)
            {
                return;
            }

            if (total > user.AttributePoints)
            {
                await user.SendAsync(StrNotEnoughAttributePoints);
                return;
            }

            await user.AddAttributesAsync(ClientUpdateType.Atributes, total * -1);
            await user.AddAttributesAsync(ClientUpdateType.Strength, Force);
            await user.AddAttributesAsync(ClientUpdateType.Agility, Speed);
            await user.AddAttributesAsync(ClientUpdateType.Vitality, Health);
            await user.AddAttributesAsync(ClientUpdateType.Spirit, Soul);
            await user.SendAsync(this);

            if (user.Team != null)
            {
                await user.Team.BroadcastMemberLifeAsync(user, true);
            }
        }
    }
}
