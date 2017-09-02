using System;
using System.Collections.Generic;
using System.Text;

namespace Units
{
	public partial struct Dimension
	{
		public int Length { get; }
		public int Mass { get; }
		public int Time { get; }
		public int Current { get; }
		public int Temperature { get; }
		public int Substance { get; }
		public int LuminousIntensity { get; }
		public int Angle { get; }

		private int? hashCode;

		public Dimension(int length = 0, int mass = 0, int time = 0, int current = 0, int temperature = 0, int substance = 0, int luminousIntensity = 0, int angle = 0)
		{
			Length = length;
			Mass = mass;
			Time = time;
			Current = current;
			Temperature = temperature;
			Substance = substance;
			LuminousIntensity = luminousIntensity;
			Angle = angle;
			hashCode = null;
		}

		public static Dimension operator +(Dimension lhs, Dimension rhs)
		{
			return new Dimension(lhs.Length + rhs.Length, lhs.Mass + rhs.Mass, lhs.Time + rhs.Time, lhs.Current + rhs.Current, lhs.Temperature + rhs.Temperature, lhs.Substance + rhs.Substance, lhs.LuminousIntensity + rhs.LuminousIntensity, lhs.Angle + rhs.Angle);
		}

		public static Dimension operator -(Dimension lhs, Dimension rhs)
		{
			return new Dimension(lhs.Length - rhs.Length, lhs.Mass - rhs.Mass, lhs.Time - rhs.Time, lhs.Current - rhs.Current, lhs.Temperature - rhs.Temperature, lhs.Substance - rhs.Substance, lhs.LuminousIntensity - rhs.LuminousIntensity, lhs.Angle - rhs.Angle);
		}

		public static Dimension operator -(Dimension rhs)
		{
			return new Dimension(-rhs.Length, -rhs.Mass, -rhs.Time, -rhs.Current, -rhs.Temperature, -rhs.Substance, -rhs.LuminousIntensity, -rhs.Angle);
		}

		public static bool operator ==(Dimension lhs, Dimension rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Dimension lhs, Dimension rhs)
		{
			return !lhs.Equals(rhs);
		}

		public override bool Equals(object obj)
		{
			if (obj is Dimension dim) { return Equals(dim); }
			return base.Equals(obj);
		}

		public bool Equals(Dimension other)
		{
			return Length == other.Length && Mass == other.Mass && Time == other.Time && Current == other.Current && Temperature == other.Temperature && Substance == other.Substance && LuminousIntensity == other.LuminousIntensity && Angle == other.Angle;
		}

		public override int GetHashCode()
		{
			return hashCode ?? (hashCode = Length.GetHashCode() ^ Util.ShiftAndWrap(Mass.GetHashCode(), 2) ^ Util.ShiftAndWrap(Time.GetHashCode(), 4) ^ Util.ShiftAndWrap(Current.GetHashCode(), 6) ^ Util.ShiftAndWrap(Temperature.GetHashCode(), 8) ^ Util.ShiftAndWrap(Substance.GetHashCode(), 10) ^ Util.ShiftAndWrap(LuminousIntensity.GetHashCode(), 12) ^ Util.ShiftAndWrap(Angle.GetHashCode(), 14)).Value;
		}

		public override string ToString()
		{
			string GetUnitText(int exponent, string abbr)
			{
				if (exponent == 0) { return ""; }
				if (exponent == 1) { return abbr; }
				return $"{abbr}^{exponent}";
			}
			void AddUnitText(ref string text, int exponent, string abbr)
			{
				var unitText = GetUnitText(exponent, abbr);
				if (unitText != "")
				{
					if (text != "") { text += "*"; }
					text += unitText;
				}
			}

			var dimension = "";
			AddUnitText(ref dimension, Length, "m");
			AddUnitText(ref dimension, Mass, "kg");
			AddUnitText(ref dimension, Time, "s");
			AddUnitText(ref dimension, Current, "A");
			AddUnitText(ref dimension, Temperature, "K");
			AddUnitText(ref dimension, Substance, "mol");
			AddUnitText(ref dimension, LuminousIntensity, "cd");
			AddUnitText(ref dimension, Angle, "rad");

			if (dimension == "") { return "1"; }
			return dimension;
		}
	}
}
