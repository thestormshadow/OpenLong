using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Fate;
using Long.Kernel.Modules.Systems.Rank;
using Long.Kernel.Network.Game;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Rank.Repositories;
using Long.Module.Rank.States;
using Long.Network.Packets;
using Serilog;
using static Long.Kernel.Modules.Systems.Rank.IDynamicRankManager;

namespace Long.Module.Rank.Network
{
    public sealed class MsgRank : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgRank>();

        public RequestType Mode { get; set; }
        public uint Identity { get; set; }
        public ushort Data1 { get; set; }
        public ushort PageNumber { get; set; }

        public List<string> Strings { get; set; } = new();
        public List<QueryStruct> Infos { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();               // 0
            Type = (PacketType)reader.ReadUInt16();     // 2
            Mode = (RequestType)reader.ReadUInt32();    // 4
            Identity = reader.ReadUInt32();             // 8
            Data1 = reader.ReadUInt16();                // 12
            PageNumber = reader.ReadUInt16();           // 14
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgRank);
            writer.Write((uint)Mode);     // 4
            writer.Write(Identity); // 8
            writer.Write(Data1); // 12
            writer.Write(PageNumber);      // 14
            if (Mode == RequestType.QueryInfo || Mode == RequestType.QueryIcon || Mode == RequestType.RequestRank)
            {
                writer.Write(Infos.Count);     // 16
                writer.Write(0);               // 20
                foreach (QueryStruct info in Infos)
                {
                    writer.Write(info.Type);     // 24
                    writer.Write(info.Amount);   // 32
                    writer.Write(info.Identity); // 40
                    writer.Write(info.Identity); // 44
                    writer.Write(info.Name, 16); // 48
                    writer.Write(info.Name, 16); // 64
                }
            }
            else
            {
                writer.Write(1);
                writer.Write(0);
            }
            return writer.ToArray();
        }

        public struct QueryStruct
        {
            public ulong Type;
            public ulong Amount;
            public uint Identity;
            public string Name;
        }

        public enum RankType : byte
        {
            Flower,
            ChiDragon,
            ChiPhoenix,
            ChiTiger,
            ChiTurtle
        }

        public enum RequestType
        {
            None,
            RequestRank,
            QueryInfo,
            QueryIcon = 5
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            switch (Mode)
            {
                case RequestType.RequestRank:
                    {
                        int ipp = 10;
                        List<UserRanking> ranking;
                        switch (Identity)
                        {
                            case RedRose:
                            case WhiteRose:
                            case Orchid:
                            case Tulip:
                            case RedRose + 400:
                            case WhiteRose + 400:
                            case Orchid + 400:
                            case Tulip + 400:
                                {
                                    ranking = DynaRankRepository.Get(Identity, 0, 100);
                                    break;
                                }

                            case DragonFate:
                            case PhoenixFate:
                            case TigerFate:
                            case TurtleFate:
                                {
                                    ranking = DynaRankRepository.Get(Identity, 0, 50);
                                    break;
                                }

                            case InnerStrength:
                                {
                                    ranking = DynaRankRepository.Get(Identity, 0, 100);
                                    break;
                                }

                            default:
                                {
                                    logger.Warning("Request rank type unhandled {0}", Identity);
                                    return;
                                }
                        }

                        if (PageNumber * ipp > ranking.Count)
                        {
                            int pageLimit = ranking.Count / ipp;
                            PageNumber = (ushort)pageLimit;
                        }

                        Data1 = (ushort)ranking.Count;
                        foreach (var info in ranking.Skip(PageNumber * ipp).Take(ipp))
                        {
                            Infos.Add(new QueryStruct
                            {
                                Amount = (ulong)info.Value,
                                Identity = info.Identity,
                                Name = info.Name,
                                Type = info.Position
                            });
                        }
                        await user.SendAsync(this);
                        break;
                    }

                case RequestType.QueryInfo:
                    {
                        await SubmitFlowerRankIconAsync(user);
                        await SubmitFlowerRankInformationAsync(user);
                        if (ModuleManager.FateManager != null && user.Fate != null)
                        {
                            await ModuleManager.DynamicRankManager.SubmitUserFateRankAsync(user, IFate.FateType.Dragon);
                            await ModuleManager.DynamicRankManager.SubmitUserFateRankAsync(user, IFate.FateType.Phoenix);
                            await ModuleManager.DynamicRankManager.SubmitUserFateRankAsync(user, IFate.FateType.Tiger);
                            await ModuleManager.DynamicRankManager.SubmitUserFateRankAsync(user, IFate.FateType.Turtle);
                        }
                        if (ModuleManager.NeiGongManager != null)
                        {
                            await ModuleManager.DynamicRankManager.SubmitUserRankAsync(user, InnerStrength);
                        }
                        break;
                    }
            }
        }

        public static async Task SubmitFlowerRankInformationAsync(Character user)
        {
            if (user.Flower != null)
            {
                await user.SendAsync(new MsgRank
                {
                    Mode = RequestType.QueryIcon,
                    Infos = new List<QueryStruct>()
                });

                UserRanking redRoseRank = null,
                    whiteRoseRank = null,
                    orchidRank = null,
                    tulipRank = null;

                const uint ADD_FIRST_RANK = 10000;
                const uint ADD_SECOND_RANK = 20000;
                const uint ADD_THIRD_RANK = 40000;

                uint rankType = 0;
                int sum = user.Gender == 1 ? 400 : 0;
                if (user.FlowerRed > 0)
                {
                    redRoseRank = DynaRankRepository.GetUserPos(user.Identity, (uint)(RedRose + sum), 100);
                    if (redRoseRank != null)
                    {
                        var msg = new MsgRank();
                        msg.Mode = RequestType.QueryInfo;
                        rankType = msg.Identity = (uint)(RedRose + sum);
                        if (redRoseRank.Position <= 3)
                        {
                            rankType += ADD_FIRST_RANK;
                        }
                        else if (redRoseRank.Position <= 10)
                        {
                            rankType += ADD_SECOND_RANK;
                        }
                        else if (redRoseRank.Position <= 50)
                        {
                            rankType += ADD_THIRD_RANK;
                        }
                        msg.Infos.Add(new QueryStruct
                        {
                            Type = 1,
                            Amount = (ulong)redRoseRank.Value,
                            Identity = user.Identity,
                            Name = user.Name
                        });
                        await user.SendAsync(msg);
                    }
                }
                if (user.FlowerWhite > 0)
                {
                    whiteRoseRank = DynaRankRepository.GetUserPos(user.Identity, (uint)(WhiteRose + sum), 100);
                    if (whiteRoseRank != null)
                    {
                        var msg = new MsgRank();
                        msg.Mode = RequestType.QueryInfo;
                        rankType = msg.Identity = (uint)(WhiteRose + sum);
                        if (whiteRoseRank.Position <= 3)
                        {
                            rankType += ADD_FIRST_RANK;
                        }
                        else if (whiteRoseRank.Position <= 10)
                        {
                            rankType += ADD_SECOND_RANK;
                        }
                        else if (whiteRoseRank.Position <= 50)
                        {
                            rankType += ADD_THIRD_RANK;
                        }
                        msg.Infos.Add(new QueryStruct
                        {
                            Type = 1,
                            Amount = (ulong)whiteRoseRank.Value,
                            Identity = user.Identity,
                            Name = user.Name
                        });
                        await user.SendAsync(msg);
                    }
                }
                if (user.FlowerOrchid > 0)
                {
                    orchidRank = DynaRankRepository.GetUserPos(user.Identity, (uint)(Orchid + sum), 100);
                    if (orchidRank != null)
                    {
                        var msg = new MsgRank();
                        msg.Mode = RequestType.QueryInfo;
                        rankType = msg.Identity = (uint)(Orchid + sum);
                        if (orchidRank.Position <= 3)
                        {
                            rankType += ADD_FIRST_RANK;
                        }
                        else if (orchidRank.Position <= 10)
                        {
                            rankType += ADD_SECOND_RANK;
                        }
                        else if (orchidRank.Position <= 50)
                        {
                            rankType += ADD_THIRD_RANK;
                        }
                        msg.Infos.Add(new QueryStruct
                        {
                            Type = 1,
                            Amount = (ulong)orchidRank.Value,
                            Identity = user.Identity,
                            Name = user.Name
                        });
                        await user.SendAsync(msg);
                    }
                }
                if (user.FlowerTulip > 0)
                {
                    tulipRank = DynaRankRepository.GetUserPos(user.Identity, (uint)(Tulip + sum), 100);
                    if (tulipRank != null)
                    {
                        var msg = new MsgRank();
                        msg.Mode = RequestType.QueryInfo;
                        rankType = msg.Identity = (uint)(Tulip + sum);
                        if (tulipRank.Position <= 3)
                        {
                            rankType += ADD_FIRST_RANK;
                        }
                        else if (tulipRank.Position <= 10)
                        {
                            rankType += ADD_SECOND_RANK;
                        }
                        else if (tulipRank.Position <= 50)
                        {
                            rankType += ADD_THIRD_RANK;
                        }
                        msg.Infos.Add(new QueryStruct
                        {
                            Type = 1,
                            Amount = (ulong)tulipRank.Value,
                            Identity = user.Identity,
                            Name = user.Name
                        });
                        await user.SendAsync(msg);
                    }
                }

                if (rankType != user.Flower.Charm)
                {
                    user.Flower.Charm = rankType;
                    await user.Screen.SynchroScreenAsync();
                }

                await user.SendAsync(new MsgRank
                {
                    Mode = RequestType.QueryIcon
                });
            }
        }

        public static async Task SubmitFlowerRankIconAsync(Character user)
        {
            if (user.Flower != null)
            {
                await user.SendAsync(new MsgFlower
                {
                    Mode = user.Gender != 1 ? MsgFlower.RequestMode.QueryFlower : MsgFlower.RequestMode.QueryGift,
                    Identity = user.Identity,
                    RedRoses = user.FlowerRed,
                    RedRosesToday = user.Flower.FlowerToday.RedRose,
                    WhiteRoses = user.FlowerWhite,
                    WhiteRosesToday = user.Flower.FlowerToday.WhiteRose,
                    Orchids = user.FlowerOrchid,
                    OrchidsToday = user.Flower.FlowerToday.Orchids,
                    Tulips = user.FlowerTulip,
                    TulipsToday = user.Flower.FlowerToday.Tulips
                });
            }
        }
    }
}
