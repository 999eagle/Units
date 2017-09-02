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
		public int Intensity { get; }

		private int? hashCode;

		public Dimension(int length = 0, int mass = 0, int time = 0, int current = 0, int temperature = 0, int substance = 0, int intensity = 0)
		{
			Length = length;
			Mass = mass;
			Time = time;
			Current = current;
			Temperature = temperature;
			Substance = substance;
			Intensity = intensity;
			hashCode = null;
		}

		public static Dimension operator +(Dimension lhs, Dimension rhs)
		{
			return new Dimension(lhs.Length + rhs.Length, lhs.Mass + rhs.Mass, lhs.Time + rhs.Time, lhs.Current + rhs.Current, lhs.Temperature + rhs.Temperature, lhs.Substance + rhs.Substance, lhs.Intensity + rhs.Intensity);
		}

		public static Dimension operator -(Dimension lhs, Dimension rhs)
		{
			return new Dimension(lhs.Length - rhs.Length, lhs.Mass - rhs.Mass, lhs.Time - rhs.Time, lhs.Current - rhs.Current, lhs.Temperature - rhs.Temperature, lhs.Substance - rhs.Substance, lhs.Intensity - rhs.Intensity);
		}

		public static Dimension operator -(Dimension rhs)
		{
			return new Dimension(-rhs.Length, -rhs.Mass, -rhs.Time, -rhs.Current, -rhs.Temperature, -rhs.Substance, -rhs.Intensity);
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
			return Length == other.Length && Mass == other.Mass && Time == other.Time && Current == other.Current && Temperature == other.Temperature && Substance == other.Substance && Intensity == other.Intensity;
		}

		public override int GetHashCode()
		{
			return hashCode ?? (hashCode = Length.GetHashCode() ^ Util.ShiftAndWrap(Mass.GetHashCode(), 2) ^ Util.ShiftAndWrap(Time.GetHashCode(), 4) ^ Util.ShiftAndWrap(Current.GetHashCode(), 6) ^ Util.ShiftAndWrap(Temperature.GetHashCode(), 8) ^ Util.ShiftAndWrap(Substance.GetHashCode(), 10) ^ Util.ShiftAndWrap(Intensity.GetHashCode(), 12)).Value;
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
			AddUnitText(ref dimension, Intensity, "cd");

			if (dimension == "") { return "1"; }
			return dimension;
		}
	}
}
