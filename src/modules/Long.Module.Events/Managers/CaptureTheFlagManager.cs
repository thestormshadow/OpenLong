using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Competion;
using Long.Module.Events.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Module.Events.Managers
{
	public class CaptureTheFlagManager : ICaptureTheFlag
	{
		public async Task EndEventAsync()
		{
			var ctf = EventManager.GetEvent<CaptureTheFlag>();
			await ctf.EndEventAsync();
		}

		public async Task PrepareEventAsync()
		{
			var ctf = EventManager.GetEvent<CaptureTheFlag>();
			await ctf.PrepareEventAsync();
		}
	}
}
