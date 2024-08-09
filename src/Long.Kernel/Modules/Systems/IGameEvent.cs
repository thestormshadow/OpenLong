using Long.Kernel.States;
using Long.Kernel.States.Magics;
using Long.Kernel.States.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Long.Kernel.States.Events.GameEvent;

namespace Long.Kernel.Modules.Systems
{
	public interface IGameEvent
	{
		EventType Identity { get; }
		GameMap Map { get; protected set; }
		bool IsInscribed(uint idUser);
		Task OnDailyResetAsync();
		Task OnBeKillAsync(Role attacker, Role target, Magic magic = null);
	}
}
