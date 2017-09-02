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
			if (!Unit.CanConvertTo(newUnit)) { throw new Exception(); }
			var conversion = Unit.ConvertTo(newUnit);
			return new Measurement(Value * conversion.scale + conversion.shift, newUnit);
		}
	}
}
