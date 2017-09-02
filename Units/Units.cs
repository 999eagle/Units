using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Units
{
	public partial struct Unit
	{
		public static readonly Unit Meter = new Unit("m", Dimension.LengthDimension);
		public static readonly Unit Kilogram = new Unit("kg", Dimension.MassDimension);
		public static readonly Unit Second = new Unit("s", Dimension.TimeDimension);
		public static readonly Unit Ampere = new Unit("A", Dimension.CurrentDimension);
		public static readonly Unit Kelvin = new Unit("K", Dimension.TemperatureDimension);
		public static readonly Unit Mole = new Unit("mol", Dimension.SubstanceDimension);
		public static readonly Unit Candela = new Unit("cd", Dimension.LuminousIntensityDimension);
		public static readonly Unit Radians = new Unit("rad", Dimension.AngleDimension);

		public static readonly Unit Kilometer = new Unit("km", Meter, new Ratio(1000));

		public static readonly Unit Celsius = new Unit("°C", Kelvin, new Ratio(1), new Ratio(-27315, 100));
		public static readonly Unit Fahrenheit = new Unit("°F", Celsius, new Ratio(9, 5), new Ratio(32));

		public static readonly Unit SquareMeter = Meter * Meter;
		public static readonly Unit CubicMeter = Meter * Meter * Meter;

		public static readonly Unit MetersPerSecond = Meter / Second;

		public static readonly Unit Newton = new Unit("N", Kilogram * Meter / (Second * Second), new Ratio(1));

		private static IList<Unit> knownUnits =
			typeof(Unit).GetFields()
			.Where(f => f.IsStatic && f.IsPublic && f.FieldType == typeof(Unit))
			.Select(f => (Unit)f.GetValue(null))
			.ToList();

		public static Unit GetUnitForDimension(Dimension dim)
		{
			if (knownUnits == null) new Unit("", dim);
			var units = knownUnits.Where(u => u.Dimension == dim);
			if (units.Any())
			{
				var unit = units.FirstOrDefault(u => u.Scale == 1 && u.Shift == 0);
				if (unit.Dimension == dim) return unit;
				else return units.First();
			}
			else
			{
				return new Unit("", dim);
			}
		}

		public static Unit GetKnownUnit(Dimension dim, Ratio scale, Ratio shift)
		{
			if (knownUnits == null) return new Unit(dim, scale, shift);
			var knownUnit = knownUnits.FirstOrDefault(u => u.Dimension == dim && u.Scale == scale && u.Shift == shift);
			if (knownUnit.Dimension == dim) return knownUnit;
			else return new Unit(dim, scale, shift);
		}

		public static Unit GetKnownUnit(Unit unit)
		{
			if (knownUnits == null) return unit;
			var knownUnit = knownUnits.FirstOrDefault(u => u == unit);
			if (knownUnit.Dimension == unit.Dimension) return knownUnit;
			else return unit;
		}
	}
}
