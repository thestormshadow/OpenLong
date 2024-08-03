using Canyon.Game.Scripting.Attributes;
using Long.Kernel.Managers;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using static Long.Kernel.Scripting.LUA.LuaScriptConst;

namespace Long.Kernel.Scripting.LUA
{
    public sealed partial class LuaProcessor
    {
        private BaseNpc GetNpc(int npcId)
        {
            if (npcId <= 0)
            {
                return role as BaseNpc;
            }
            else
            {
                return RoleManager.FindRole<BaseNpc>((uint)npcId);
            }
        }

        [LuaFunction]
        public long GetNpcInt(int npcId, int index)
        {
            BaseNpc npc = GetNpc(npcId);
            if (npc == null)
            {
                return 0;
            }

            switch (index)
            {
                case G_NPC_ID: return npc.Identity;
                case G_NPC_OwnerID: return npc.OwnerIdentity;
                case G_NPC_OwnerType: return npc.OwnerType;
                case G_NPC_Type: return npc.Type;
                case G_NPC_LookFace: return npc.Mesh;
                case G_NPC_MapID: return npc.MapIdentity;
                case G_NPC_PosX: return npc.X;
                case G_NPC_PosY: return npc.Y;
                case G_NPC_Data0: return npc.Data0;
                case G_NPC_Data1: return npc.Data1;
                case G_NPC_Data2: return npc.Data2;
                case G_NPC_Data3: return npc.Data3;
                case G_NPC_MaxLife: return npc.MaxLife;
                case G_NPC_Life: return npc.Life;
                default:
                    return 0;
            }
        }

        [LuaFunction]
        public string GetNpcStr(int npcId, int index)
        {
            BaseNpc npc = GetNpc(npcId);
            if (npc == null)
            {
                return StrNone;
            }

            switch (index)
            {
                case G_NPC_Name: return npc.Name;
                case G_NPC_DataStr: return npc.DataStr;
                default: return GetNpcInt(npcId, index).ToString();
            }
        }

        [LuaFunction]
        public bool NpcMove(int npcId, int idMap, int x, int y)
        {
            BaseNpc npc = GetNpc(npcId);
            if (npc == null)
            {
                return false;
            }
            GameMap map = GetGameMap(idMap);
            if (map == null)
            {
                return false;
            }
            if (map.IsValidPoint(x, y))
            {
                return NpcPosition_Set(npcId, (uint)idMap, x, y);
            }
            return false;
        }

        [LuaFunction]
        public bool NpcPosition_Set(int npcId, uint idMap, int x, int y)
        {
            BaseNpc npc = GetNpc(npcId);
            if (npc == null)
            {
                return false;
            }
            return npc.ChangePosAsync(idMap, (ushort)x, (ushort)y).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public int GetNpcCount(int userId, int index, object data)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return 0;
            }

            if (index == G_NPC_COUNT_ALL)
            {
                return user.Map.QueryRoles().Count;
            }
            else if (index == G_NPC_COUNT_FURNITURE)
            {
                return user.Map.QueryRoles(x => x is BaseNpc npc && npc.Type == BaseNpc.ROLE_FURNITURE_NPC && npc.Type == BaseNpc.ROLE_3DFURNITURE_NPC).Count;
            }
            else if (index == G_NPC_COUNT_NAME)
            {
                return user.Map.QueryRoles(x => x is BaseNpc && x.Name.Equals(data.ToString())).Count;
            }
            else if (index == G_NPC_COUNT_TYPE)
            {
                return user.Map.QueryRoles(x => x is BaseNpc npc && npc.Type == int.Parse(data.ToString())).Count;
            }

            return 0;
        }
    }
}
