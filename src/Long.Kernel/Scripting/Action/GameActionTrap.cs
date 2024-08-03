using Long.Database.Entities;
using Long.Kernel.States.Items;
using Long.Kernel.States;
using Long.Shared.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Long.Kernel.Managers;
using Long.Kernel.States.User;
using Long.Kernel.States.World;

namespace Long.Kernel.Scripting.Action
{
	public partial class GameAction
	{
		/**
         * 
         * ActionTrapCreate = 2101,
            ActionTrapErase = 2102,
            ActionTrapCount = 2103,
            ActionTrapAttr = 2104, 
            ActionTrapInstanceDelete = 2105,
        */

		private static async Task<bool> ExecuteActionTrapCreateAsync(DbAction action, string param, Character user, Role role, Item item, string[] inputs)
		{
			string[] splitParams = SplitParam(param, 9);
			if (splitParams.Length < 7)
			{
				logger.Error($"Invalid param length ExecuteActionTrapCreate {action.Id}");
				return false;
			}

			uint type = uint.Parse(splitParams[0]),
				look = uint.Parse(splitParams[1]),
				owner = uint.Parse(splitParams[2]),
				idMap = uint.Parse(splitParams[3]);
			ushort posX = ushort.Parse(splitParams[4]),
				posY = ushort.Parse(splitParams[5]),
				data = ushort.Parse(splitParams[6]);

			if (MapManager.GetMap(idMap) == null)
			{
				logger.Error($"Invalid map for ExecuteActionTrapCreate {idMap}:{action.Id}");
				return false;
			}

			MapTrap trap = new MapTrap(new DbTrap
			{
				TypeId = type,
				Look = look,
				OwnerId = owner,
				Data = data,
				MapId = idMap,
				PosX = posX,
				PosY = posY,
				Id = (uint)IdentityManager.Traps.GetNextIdentity
			});

			if (!await trap.InitializeAsync())
			{
				logger.Error($"could not start trap for ExecuteActionTrapCreate {action.Id}");
				return false;
			}

			trap.QueueAction(trap.EnterMapAsync);
			return true;
		}

		private static async Task<bool> ExecuteActionTrapEraseAsync(DbAction action, string param, Character user, Role role, Item item, string[] inputs)
		{
			MapTrap trap = role as MapTrap;
			if (trap == null)
				return false;

			trap.QueueAction(trap.LeaveMapAsync);
			return true;
		}

		private static async Task<bool> ExecuteActionTrapCountAsync(DbAction action, string param, Character user, Role role, Item item, string[] inputs)
		{
			return true;
		}

		private static async Task<bool> ExecuteActionTrapAttrAsync(DbAction action, string param, Character user, Role role, Item item, string[] inputs)
		{
			return true;
		}

		private static async Task<bool> ExecuteActionTrapTypeDeleteAsync(DbAction action, string param, Character user, Role role, Item item, string[] inputs)
		{
			string[] splitParams = SplitParam(" ");
			if (splitParams.Length < 2)
			{

				return false;
			}

			uint idMap = uint.Parse(splitParams[0]);
			uint trapType = uint.Parse(splitParams[1]);

			GameMap gameMap = MapManager.GetMap(idMap);
			if (gameMap == null)
			{
				return false;
			}

			foreach (var mapTrap in gameMap.QueryRoles(x => x is MapTrap trap && trap.Type == trapType))
			{
				mapTrap.QueueAction(mapTrap.LeaveMapAsync);
			}
			return true;
		}
	}
}
