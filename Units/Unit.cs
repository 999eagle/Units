using System;

namespace Units
{
	public partial struct Unit
	{
		public string Name { get; }
		public Dimension Dimension { get; }
		public Ratio Scale { get; }
		public Ratio Shift { get; }

		public Unit(Dimension dimension, Ratio scale, Ratio shift) : this("", dimension, scale, shift) { }

		public Unit(string name, Dimension dimension) : this(name, dimension, new Ratio(1)) { }
		public Unit(string name, Dimension dimension, Ratio scale) : this(name, dimension, scale, new Ratio(0)) { }
		public Unit(string name, Dimension dimension, Ratio scale, Ratio shift)
		{
			Name = name;
			Dimension = dimension;
			Scale = scale;
			Shift = shift;
		}

		public Unit(string name, Unit baseUnit) : this(name, baseUnit.Dimension, baseUnit.Scale, baseUnit.Shift) { }
		public Unit(string name, Unit baseUnit, Ratio scale) : this(name, baseUnit, scale, new Ratio(0)) { }
		public Unit(string name, Unit baseUnit, Ratio scale, Ratio shift)
		{
			Name = name;
			Dimension = baseUnit.Dimension;
			Scale = baseUnit.Scale * scale;
			Shift = baseUnit.Shift * scale + shift;
		}

		public static Unit operator *(Unit lhs, Unit rhs)
		{
			if (lhs.Shift != 0 || rhs.Shift != 0) throw new Exception();
			return GetKnownUnit(lhs.Dimension + rhs.Dimension, lhs.Scale * rhs.Scale, new Ratio(0));
		}

		public static Unit operator /(Unit lhs, Unit rhs)
		{
			if (lhs.Shift != 0 || rhs.Shift != 0) throw new Exception();
			return GetKnownUnit(lhs.Dimension - rhs.Dimension, lhs.Scale / rhs.Scale, new Ratio(0));
		}

		public static Unit operator ^(Unit lhs, int rhs)
		{
			if (lhs.Shift != 0) throw new Exception();
			Ratio scale;
			if (rhs < 0)
			{
				scale = new Ratio((int)Math.Pow(lhs.Scale.Denominator, -rhs), (int)Math.Pow(lhs.Scale.Numerator, -rhs));
			}
			else
			{
				scale = new Ratio((int)Math.Pow(lhs.Scale.Numerator, rhs), (int)Math.Pow(lhs.Scale.Denominator, rhs));
			}
			return GetKnownUnit(lhs.Dimension ^ rhs, scale, new Ratio(0));
		}

		public static bool operator ==(Unit lhs, Unit rhs) => lhs.Equals(rhs);
		public static bool operator !=(Unit lhs, Unit rhs) => !lhs.Equals(rhs);
		public bool Equals(Unit other) => Dimension == other.Dimension && Scale == other.Scale && Shift == other.Shift;
		public override bool Equals(object obj) => obj is Unit unit ? Equals(unit) : base.Equals(obj);

		public override int GetHashCode()
		{
			return Dimension.GetHashCode() ^ Util.ShiftAndWrap(Scale.GetHashCode(), 4) ^ Util.ShiftAndWrap(Shift.GetHashCode(), 8);
		}

		public bool CanConvertTo(Unit other) => other.Dimension == Dimension;

		public (Ratio scale, Ratio shift) ConvertTo(Unit other)
		{
			if (!CanConvertTo(other)) { throw new Exception(); }
			var scale = other.Scale / Scale;
			return (scale, other.Shift - Shift * scale);
		}
	}
}
