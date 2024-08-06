using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Kernel.Scripting
{
	internal class Tools
	{
		public class RandomLite
		{
			private volatile uint x;
			private volatile uint y;
			private volatile uint z;
			private volatile uint w;

			public RandomLite()
			  : this(Environment.TickCount)
			{
			}

			public RandomLite(int seed)
			{
				this.Reseed(seed);
			}

			public void Reseed(int seed)
			{
				this.x = (uint)seed;
				this.y = 842502087U;
				this.z = 3579807591U;
				this.w = 273326509U;
			}

			public int Next()
			{
				uint num1 = this.x ^ this.x << 11;
				this.x = this.y;
				this.y = this.z;
				this.z = this.w;
				this.w = (uint)((int)this.w ^ (int)(this.w >> 19) ^ ((int)num1 ^ (int)(num1 >> 8)));
				uint num2 = this.w & (uint)int.MaxValue;
				if (num2 == (uint)int.MaxValue)
					return this.Next();
				return (int)num2;
			}

			public int Next(int upperBound)
			{
				if (upperBound < 0)
					throw new ArgumentOutOfRangeException(nameof(upperBound), (object)upperBound, "upperBound must be >=0");
				if (upperBound == 0)
					return 0;
				return this.Next() % (upperBound + 1);
			}

			public int Next(int lowerBound, int upperBound)
			{
				if (lowerBound > upperBound)
					throw new ArgumentOutOfRangeException(nameof(upperBound), (object)upperBound, "upperBound must be >=lowerBound");
				if (lowerBound == upperBound)
					return lowerBound;
				return lowerBound + this.Next() % (upperBound + 1 - lowerBound);
			}
		}
	}
}
