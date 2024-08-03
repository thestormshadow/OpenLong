using Canyon.Game.Scripting.Attributes;
using Long.Kernel.States.User;

namespace Long.Kernel.Scripting.LUA
{
    public sealed partial class LuaProcessor
    {
        [LuaFunction]
        public bool MagicCheckLev(int userId, int magicType, int magicLevel)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            return user.MagicData.CheckLevel((ushort)magicType, (ushort)magicLevel);
        }

        [LuaFunction]
        public bool MagicCheckType(int userId, int magicType)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }
            return user.MagicData.Magics.ContainsKey((uint)magicType);
        }

        [LuaFunction]
        public bool LearnMagic(int userId, int magicType)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }
            return user.MagicData.CreateAsync((ushort)magicType, 0).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool MagicUpLev(int userId, int magicType)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }
            return user.MagicData.UpLevelByTaskAsync((ushort)magicType).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool MagicAddExp(int userId, int magicType, int exp)
        {
            Character user = GetUser(userId);
            if (user == null || !user.MagicData.Magics.TryGetValue((uint)magicType, out var magic))
            {
                return false;
            }
            return user.MagicData.AwardExpAsync(0, 0, exp, magic).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool MagicAddLevTime(int userId, int magicType, int expLevTime)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            if (!user.MagicData.Magics.TryGetValue((uint)magicType, out var magic))
            {
                return false;
            }

            logger.Warning("MagicAddLevTime(int userId, int magicType, int expLevTime) not implemented");
            return true;
        }
    }
}
