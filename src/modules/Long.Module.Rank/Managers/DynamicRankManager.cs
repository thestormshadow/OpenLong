using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Fate;
using Long.Kernel.Modules.Systems.Rank;
using Long.Kernel.States.User;
using Long.Module.Rank.Network;
using Long.Module.Rank.Repositories;
using Microsoft.EntityFrameworkCore;
using static Long.Kernel.States.Npcs.DynamicNpc;

namespace Long.Module.Rank.Managers
{
    public class DynamicRankManager : IDynamicRankManager
    {
        public async Task CreateOrUpdateAsync(uint rankType, uint idUser, long value)
        {
            await using var context = new ServerDbContext();
            var userRank = await context.DynaRankRecs.FirstOrDefaultAsync(x => x.RankType == rankType && x.UserId == idUser);
            if (userRank == null)
            {
                context.DynaRankRecs.Add(new DbDynaRankRec
                {
                    ObjId = idUser,
                    UserId = idUser,
                    RankType = rankType,
                    Value = value
                });
            }
            else
            {
                userRank.Value = value;
            }
            await context.SaveChangesAsync();
        }

        public int QueryUserRank(uint rankType, uint userId, int limit)
        {
            var rank = DynaRankRepository.GetUserPos(userId, rankType, limit);
            if (rank == null)
            {
                return -1;
            }
            return (int)rank.Position;
        }

        public List<DbDynaRankRec> GetRank(uint rankType)
        {
            return DynaRankRepository.QueryRank(rankType);
        }

        public Task SubmitUserFateRankAsync(Character user, IFate.FateType fateType)
        {
            if (ModuleManager.FateManager == null || user.Fate == null)
            {
                return Task.CompletedTask;
            }

            uint rankType;
            switch (fateType)
            {
                case IFate.FateType.Dragon: rankType = IDynamicRankManager.DragonFate; break;
                case IFate.FateType.Phoenix: rankType = IDynamicRankManager.PhoenixFate; break;
                case IFate.FateType.Tiger: rankType = IDynamicRankManager.TigerFate; break;
                case IFate.FateType.Turtle: rankType = IDynamicRankManager.TurtleFate; break;
                default: return Task.CompletedTask;
            }

            if (user.IsOSUser())
            {
                return user.SendAsync(new MsgRank
                {
                    Mode = MsgRank.RequestType.QueryInfo,
                    Identity = rankType,
                    Infos = new List<MsgRank.QueryStruct>
                    {
                        new MsgRank.QueryStruct
                        {
                            Identity = user.Identity,
                            Name = user.Name,
                            Type = 0,
                            Amount = 0
                        }
                    }
                });
            }

            ulong position = (ulong)ModuleManager.FateManager.GetPlayerRank(user.Identity, fateType) + 1;
            ulong score = (ulong)user.Fate.GetScore(fateType);

            return user.SendAsync(new MsgRank
            {
                Mode = MsgRank.RequestType.QueryInfo,
                Identity = rankType,
                Infos = new List<MsgRank.QueryStruct>
                {
                    new MsgRank.QueryStruct
                    {
                        Identity = user.Identity,
                        Name = user.Name,
                        Type = position,
                        Amount = score
                    }
                }
            });
        }

        public Task SubmitUserRankAsync(Character user, int rankType)
        {
            var rank = GetRank((uint)rankType);
            var rankIndex = rank.FindIndex(x => x.UserId == user.Identity);
            if (rankIndex < 0 || rankIndex >= rank.Count)
            {
                return Task.CompletedTask;
            }
            var userRank = rank[rankIndex];
            return user.SendAsync(new MsgRank
            {
                Mode = MsgRank.RequestType.QueryInfo,
                Identity = (uint)rankType,
                Infos = new List<MsgRank.QueryStruct>
                {
                    new MsgRank.QueryStruct
                    {
                        Identity = user.Identity,
                        Name = user.Name,
                        Type = (ulong)(rankIndex + 1),
                        Amount = (ulong)userRank.Value
                    }
                }
            });
        }
    }
}
