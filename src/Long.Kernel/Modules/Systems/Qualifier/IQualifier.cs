﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Kernel.Modules.Systems.Qualifier
{
	public interface IQualifier : IGameEvent
	{
		public bool IsInsideMatch(uint idUser);
		public int GetPlayerRanking(uint idUser);
	}
}
