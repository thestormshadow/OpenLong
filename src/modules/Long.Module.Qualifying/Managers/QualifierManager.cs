using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Qualifier;
using Long.Module.Qualifying.States.UserQualifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Module.Qualifying.Managers
{
	public class QualifierManager : IQualifier
	{
		public bool IsInsideMatch(uint idUser)
		{
			var qualifier = EventManager.GetEvent<ArenaQualifier>();
			if (qualifier == null)
			{
				return false;
			}
			return qualifier.IsInsideMatch(idUser);
		}
	}
}
