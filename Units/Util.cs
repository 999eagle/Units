using System;
using System.Collections.Generic;
using System.Text;

namespace Units
{
	class Util
	{
		public static int GCD(int a, int b)
		{
			while (b != 0)
			{
				var t = b;
				b = a % b;
				a = t;
			}
			return a;
		}
	}
}
