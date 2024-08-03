using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.States;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Kernel.Scripting.Action
{
	public partial class GameAction
	{
		//PENDIENTE
		private static async Task<bool> ExecuteActionMountRacingEventResetAsync(DbAction action, string param, Character user,
																	 Role role, Item item, string[] inputs)
		{
			//HorseRacing horseRacing = EventManager.GetEvent<HorseRacing>();
			//if (horseRacing == null)
			//{
			//	logger.Fatal($"Cannot start horse racing! event not initialized");
			//	return false;
			//}

			//await horseRacing.PrepareStartupAsync(uint.Parse(param));
			return true;
		}
	}
}
