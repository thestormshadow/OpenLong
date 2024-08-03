using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Network.Packets;
using Serilog;

namespace Long.Module.Syndicate.Network
{
    public sealed class MsgSynRecruitAdvertisingOpt : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgSynRecruitAdvertisingOpt>();

        public AdvertisingOpt Action { get; set; }
        public uint Identity { get; set; }

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (AdvertisingOpt)reader.ReadInt32();
            Identity = reader.ReadUInt32();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgSynRecruitAdvertisingOpt);
            writer.Write((int)Action);
            writer.Write(Identity);
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            switch (Action)
            {
                case AdvertisingOpt.Join:
                    {
                        await ModuleManager.SyndicateManager.JoinByAdvertisingAsync(client.Character, (ushort)Identity);
                        break;
                    }

                case AdvertisingOpt.Recruit:
                    {
                        if (client.Character.Syndicate == null)
                        {
                            return;
                        }

                        if (ModuleManager.SyndicateManager.HasSyndicateAdvertise(client.Character.SyndicateIdentity))
                        {
                            await ModuleManager.SyndicateManager.SubmitEditAdvertiseScreenAsync(client.Character);
                        }
                        else
                        {
                            await client.SendAsync(new MsgSynRecuitAdvertising()
                            {
                                Identity = client.Character.SyndicateIdentity
                            });
                        }
                        break;
                    }

                default:
                    {
                        logger.Warning("Action [{0}] is not being handled.\n" + PacketDump.Hex(Encode()), Action);
                        break;
                    }
            }
        }

        public enum AdvertisingOpt
        {
            Join = 1,
            Recruit
        }
    }
}
