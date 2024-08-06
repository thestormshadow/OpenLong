using Canyon.Game.Scripting.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Kernel.Scripting.LUA
{
	public sealed partial class LuaProcessor
	{
		[LuaFunction]
		public bool MyMath_ChanceSuccess(int value)
		{
			if (value <= 0)
				return false;

			return value >= MyMath_Generate(1, 120);

		}

		[LuaFunction]
		public Boolean MyMath_Success(Double Chance)
		{
			var result = ((Double)MyMath_Generate(1, 1000000)) / 10000 >= 100 - Chance;
			return result;
		}

		[LuaFunction]
		public Int32 MyMath_Generate(Int32 Min, Int32 Max)
		{
			if (Max != Int32.MaxValue)
				Max++;

			Int32 Value = 0;
			Value = new Tools.RandomLite().Next(Min, Max);
			return Value;
		}

		[LuaFunction]
		public Int32 MyMath_Generate(UInt32 Min, UInt32 Max)
		{
			if (Max != Int32.MaxValue)
				Max++;

			Int32 Value = 0;
			Value = new Tools.RandomLite().Next((int)Min, (int)Max);
			return Value;
		}
	}
}
