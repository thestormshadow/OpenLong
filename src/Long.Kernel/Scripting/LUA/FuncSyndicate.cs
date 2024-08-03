using Canyon.Game.Scripting.Attributes;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.States.User;
using static Long.Kernel.Scripting.LUA.LuaScriptConst;

namespace Long.Kernel.Scripting.LUA
{
    public sealed partial class LuaProcessor
    {
        private ISyndicate GetSyndicate(int synId)
        {
            if (synId < 0)
            {
                return user.Syndicate;
            }

            return SyndicateManager.GetSyndicate(synId);
        }

        [LuaFunction]
        public long GetSynInt(int guildId, int index)
        {
            ISyndicate syndicate = GetSyndicate(guildId);
            if (syndicate == null)
            {
                return 0;
            }

            switch (index)
            {
                case G_SYNDICATE_MONEY:
                    {
                        return syndicate.Money;
                    }
                case G_SYNDICATE_MEMBER_AMOUNT:
                    {
                        return syndicate.MemberCount;
                    }
                case G_SYNDICATE_EMONEY:
                    {
                        return syndicate.ConquerPoints;
                    }
                case G_SYNDICATE_LEVEL:
                    {
                        return syndicate.Level;
                    }
            }
            return 0;
        }

        [LuaFunction]
        public string GetSynStr(int guildId, int index)
        {
            ISyndicate syndicate = GetSyndicate(guildId);
            if (syndicate == null)
            {
                return StrNone;
            }

            switch (index)
            {
                case G_SYNDICATE_NAME:
                    {
                        return syndicate.Name;
                    }
                case G_SYNDICATE_LEADER_NAME:
                    {
                        return syndicate.Leader?.UserName ?? StrNone;
                    }
            }

            return StrNone;
        }

        [LuaFunction]
        public long GetSynMemberInt(int userId, int index)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return 0;
            }

            switch (index)
            {
                case G_SYN_MEMBER_ATTR_RANK:
                    {
                        return (long)user.SyndicateRank;
                    }
                case G_SYN_MEMBER_ATTR_PROFFER:
                    {
                        return user.SyndicateMember.Silvers;
                    }
            }

            return 0;
        }

        [LuaFunction]
        public string GetSynMemberStr(int userId, int index)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return StrNone;
            }

            switch (index)
            {
                case G_SYN_MEMBER_ATTR_RANK:
                    {
                        return user.SyndicateRank.ToString();
                    }
            }

            return StrNone;
        }

        [LuaFunction]
        public int GetVexillumRank(int guildId)
        {
            // TODO implement Vexillum
            return 1;
        }

        [LuaFunction]
        public int GetBrickQuality(int userId)
        {
            return -1;
        }
    }
}
