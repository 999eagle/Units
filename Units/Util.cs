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

		public static Ratio GetPrefixRatio(char prefix)
		{
			switch (prefix)
			{
				case 'h': return new Ratio(1, 100);
				case 'k': return new Ratio(1, 1000);
				case 'M': return new Ratio(1, 1000_000);
				case 'G': return new Ratio(1, 1000_000_000);
				//case 'T': return new Ratio(1, 1000_000_000_000);
				//case 'P': return new Ratio(1, 1000_000_000_000_000);
				//case 'E': return new Ratio(1, 1000_000_000_000_000_000);
				//case 'Z': return new Ratio(1, 1000_000_000_000_000_000_000);
				//case 'Y': return new Ratio(1, 1000_000_000_000_000_000_000_000);
				case 'c': return new Ratio(100);
				case 'm': return new Ratio(1000);
				case 'μ': return new Ratio(1000_000);
				case 'n': return new Ratio(1000_000_000);
				//case 'p': return new Ratio(1000_000_000_000);
				//case 'f': return new Ratio(1000_000_000_000_000);
				//case 'a': return new Ratio(1000_000_000_000_000_000);
				//case 'z': return new Ratio(1000_000_000_000_000_000_000);
				//case 'y': return new Ratio(1000_000_000_000_000_000_000_000);
				default: return new Ratio();
			}
		}
	}
}
