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

		public static int ShiftAndWrap(int value, int shift)
		{
			shift = shift & 0x1F;
			uint number = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
			uint wrapped = number >> (32 - shift);
			return BitConverter.ToInt32(BitConverter.GetBytes((number << shift) | wrapped), 0);
		}
	}
}
