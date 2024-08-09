using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Events;
using Long.Module.Events.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Module.Events.Managers
{
	public class LineSkillPKManager : ILineSkillPK
	{        
		public Task ForceStartup()
		{
			var LSPK = EventManager.GetEvent<LineSkillPK>();
			LSPK.ForceStartup();
			return Task.CompletedTask;
		}
		public Task ForceEnd()
		{
			var LSPK = EventManager.GetEvent<LineSkillPK>();
			LSPK.ForceEnd();
			return Task.CompletedTask;
		}		
	}
}
