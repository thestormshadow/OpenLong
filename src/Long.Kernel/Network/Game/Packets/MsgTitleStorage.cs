using Long.Kernel.States.User;
using Long.Network.Packets;
using ProtoBuf;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgTitleStorage 
        : MsgProtoBufBase<GameClient, MsgTitleStoragePB>
    {
        private static readonly ILogger logger = Log.ForContext<MsgTitleStorage>();

        public MsgTitleStorage()
            : base(PacketType.MsgTitleStorage)
        {
        }

        public MsgTitleStorage(TitleStorageAction action, uint life, uint data1, uint data2)
            : base(PacketType.MsgTitleStorage)
        {
            Data = new()
            {
                Action = (uint)action,
                Life = life,
                Data = data1,
                Data2 = data2,
                Info = new()
            };
        }

        public MsgTitleStorage(TitleStorageAction action, uint life, uint data1, uint data2, MsgTitleInfoPB titleInfo)
            : base(PacketType.MsgTitleStorage)
        {
            Data = new()
            {
                Action = (uint)action,
                Life = life,
                Data = data1,
                Data2 = data2,
                Info = new()
                {
                    titleInfo
                }
            };
        }

        public enum TitleStorageAction
        {
            UseStorageUnit,
            UpdateUserData,
            DelUserData,
            DelUserDataByType,
            Equip,
            Unequip,
            Load,
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            switch ((TitleStorageAction)Data.Action)
            {
                case TitleStorageAction.Equip:
                    {
                        await user.TitleStorage.EquipAsync(Data.Data, Data.Data2);
                        break;
                    }

                case TitleStorageAction.Unequip:
                    {
                        await user.TitleStorage.UnEquipAsync(Data.Data, Data.Data2);
                        break;
                    }

                default:
                    {
                        logger.Warning("MsgTitleStorage->ProcessAsync({0}) missing handler", (TitleStorageAction)Data.Action);
                        break;
                    }
            }
        }
    }

    [ProtoContract]
    public struct MsgTitleStoragePB
    {
        [ProtoMember(1, IsRequired = true)]
        public uint Action { get; set; }
        [ProtoMember(2, IsRequired = true)]
        public uint Life { get; set; }
        [ProtoMember(3, IsRequired = true)]
        public uint Data { get; set; }
        [ProtoMember(4, IsRequired = true)]
        public uint Data2 { get; set; }
        [ProtoMember(5, IsRequired = true)]
        public List<MsgTitleInfoPB> Info { get; set; }
    }

    [ProtoContract]
    public struct MsgTitleInfoPB
    {
        [ProtoMember(1, IsRequired = true)]
        public uint Type { get; set; }
        [ProtoMember(2, IsRequired = true)]
        public uint Title { get; set; }
        [ProtoMember(3, IsRequired = true)]
        public uint Status { get; set; }
        [ProtoMember(4, IsRequired = true)]
        public uint DelTime { get; set; }
    }
}
