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

		public Measurement(double value, string unit) : this(value, Unit.Parse(unit)) { }

		public Measurement ToBaseUnits() => ConvertTo(Unit.GetUnitForDimension(Unit.Dimension));

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
			// I hate temperatures... How do you add 30°C to 86°F? Fuck these unit shifts I have to carry around everywhere...
			// Even this doesn't make any sense! What should 30°C+30°C be? 60°C? 606.3K because 30°C=273.15K?
			// And now start doing stuff in Fahrenheit and Rankine! What's 50°R+50°C? It's a useless number!
			if (lhs.Unit.Shift != rhs.Unit.Shift) throw new InvalidOperationException("Differently shifted units can't be added.");
			var rhsConverted = rhs.ConvertTo(lhs.Unit);
			return rhsConverted + lhs;
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
