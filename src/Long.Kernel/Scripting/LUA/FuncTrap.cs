using Canyon.Game.Scripting.Attributes;
using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.States;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Shared.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Long.Kernel.Scripting.LUA
{
	public sealed partial class LuaProcessor
	{
		[LuaFunction]
		public bool DelMapTrap(int MapId, int TrapType)
		{
			GameMap gameMap = MapManager.GetMap(MapId);
			if (gameMap == null)
			{
				return false;
			}

			foreach (var mapTrap in gameMap.QueryRoles(x => x is MapTrap trap && trap.Type == TrapType))
			{
				mapTrap.QueueAction(mapTrap.LeaveMapAsync);
			}
			return true;
		}

		[LuaFunction]
		public bool MoveTrap_SetCount(int TrapType, int MapId, int count)
		{
			GameMap gameMap = MapManager.GetMap(MapId);
			if (gameMap == null)
			{
				return false;
			}
			return true;
		}

		[LuaFunction]
		public bool CreateMapTrap(int Type, int Look, int OwnerId, int MapId, int PosX, int PosY, int Data, int PosCX, int PosCY)
		{
			if (MapManager.GetMap(MapId) == null)
			{
				logger.Error($"Invalid map for {MapId}:LuaFunction:CreateMapTrap");
				return false;
			}

			MapTrap trap = new MapTrap(new DbTrap
			{
				TypeId = (uint)Type,
				Look = (uint)Look,
				OwnerId = (uint)OwnerId,
				Data = (uint)Data,
				MapId = (uint)MapId,
				PosX = (ushort)PosX,
				PosY = (ushort)PosY,
				Id = (uint)IdentityManager.Traps.GetNextIdentity
			});

			if (!trap.InitializeAsync().Result)
			{
				logger.Error($"could not start trap for LuaFunction:CreateMapTrap");
				return false;
			}

			trap.QueueAction(trap.EnterMapAsync);
			return true;
		}
	}
}
