using System;
using System.Collections.Generic;
using System.Text;

namespace Units
{
	public struct Measurement
	{
		public double Value { get; }
		public Unit Unit { get; }

		public Measurement(double value, Unit unit)
		{
			Value = value;
			Unit = unit;
		}

		public Measurement ToBaseUnits() => new Measurement((Value - Unit.Shift) / Unit.Scale, Unit.GetUnitForDimension(Unit.Dimension));

		public override string ToString()
		{
			if (Unit.Name != "")
			{
				return $"{Value} {Unit.Name}";
			}
			else
			{
				var baseUnits = ToBaseUnits();
				if (baseUnits.Unit == Unit)
				{
					return $"{(Value - Unit.Shift) / Unit.Scale} {Unit.Dimension}";
				}
				else
				{
					return baseUnits.ToString();
				}
			}
		}

		public Measurement ConvertTo(Unit newUnit)
		{
			var conversion = Unit.ConvertTo(newUnit);
			return new Measurement(Value * conversion.scale + conversion.shift, newUnit);
		}

		public Measurement ConvertTo(string newUnit)
		{
			return ConvertTo(Unit.Parse(newUnit));
		}

		public static Measurement operator *(Measurement lhs, Measurement rhs)
		{
			return new Measurement(lhs.Value * rhs.Value, lhs.Unit * rhs.Unit);
		}

		public static Measurement operator /(Measurement lhs, Measurement rhs)
		{
			return new Measurement(lhs.Value / rhs.Value, lhs.Unit / rhs.Unit);
		}

		public static Measurement operator +(Measurement lhs, Measurement rhs)
		{
			if (!lhs.Unit.CanConvertTo(rhs.Unit)) throw new InvalidOperationException("Adding measurements requires convertible units.");
			if (lhs.Unit == rhs.Unit)
			{
				return new Measurement(lhs.Value + rhs.Value, lhs.Unit);
			}
			else if (rhs.Unit.Shift == 0 && lhs.Unit.Shift != 0)
			{
				var converted = lhs.ConvertTo(rhs.Unit);
				return new Measurement(converted.Value + rhs.Value, rhs.Unit);
			}
			else
			{
				var converted = rhs.ConvertTo(lhs.Unit);
				return new Measurement(lhs.Value + converted.Value, lhs.Unit);
			}
		}

		public static Measurement operator -(Measurement lhs, Measurement rhs)
		{
			if (!lhs.Unit.CanConvertTo(rhs.Unit)) throw new InvalidOperationException("Subtracting measurements requires convertible units.");
			return lhs + (-rhs);
		}

		public static Measurement operator -(Measurement rhs)
		{
			return new Measurement(-rhs.Value, rhs.Unit);
		}
	}
}
