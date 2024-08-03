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
		//PENDIENTE DE IMPLEMENTAR
		private static async Task<bool> ExecuteActionCaptureTheFlagCheckAsync(DbAction action, Character user, Role role, Item item, string[] inputs)
		{
			//CaptureTheFlag captureTheFlag = EventManager.GetEvent<CaptureTheFlag>();
			//if (captureTheFlag == null)
			//{
			//	return false;
			//}
			//return captureTheFlag.IsActive;
			return false;
		}

		private static async Task<bool> ExecuteActionCaptureTheFlagExitAsync(DbAction action, Character user, Role role, Item item, string[] inputs)
		{
			//CaptureTheFlag captureTheFlag = EventManager.GetEvent<CaptureTheFlag>();
			//if (captureTheFlag == null)
			//{
			//	return false;
			//}
			// ???
			return false;
		}
	}
}
