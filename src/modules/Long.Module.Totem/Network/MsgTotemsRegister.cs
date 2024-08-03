using Long.Kernel.Network.Game;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Module.Totem.Network
{
    public sealed class MsgTotemsRegister : MsgBase<GameClient>
    {
        public int Count => Items.Count;
        public List<uint> Items { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            int count = reader.ReadByte();
            for (int i = 0; i < count; i++)
            {
                Items.Add(reader.ReadUInt32());
            }
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgTotemsRegister);
            writer.Write((byte)Count);
            foreach (var item in Items)
            {
                writer.Write(item);
            }
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (user.SyndicateIdentity == 0)
            {
                return;
            }

            List<uint> items = new();
            foreach (var idItem in Items)
            {
                Item item = user.UserPackage.FindItemByIdentity(idItem);
                if (item == null)
                {
                    continue;
                }

                if (item.SyndicateIdentity != 0)
                {
                    continue;
                }

                if (!await user.Syndicate.Totem.InscribeItemAsync(user, item))
                {
                    continue;
                }

                items.Add(idItem);
            }

            await user.SendAsync(new MsgTotemsRegister
            {
                Items = items
            });
        }
    }
}
