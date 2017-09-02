using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Units
{
	public partial struct Unit
	{
		// Basic units
		public static readonly Unit Scalar = new Unit("1", Dimension.ScalarDimension);
		public static readonly Unit Meter = new Unit("m", Dimension.LengthDimension);
		public static readonly Unit Kilogram = new Unit("kg", Dimension.MassDimension);
		public static readonly Unit Second = new Unit("s", Dimension.TimeDimension);
		public static readonly Unit Ampere = new Unit("A", Dimension.CurrentDimension);
		public static readonly Unit Kelvin = new Unit("K", Dimension.TemperatureDimension);
		public static readonly Unit Mole = new Unit("mol", Dimension.SubstanceDimension);
		public static readonly Unit Candela = new Unit("cd", Dimension.LuminousIntensityDimension);
		public static readonly Unit Radians = new Unit("rad", Dimension.AngleDimension);

		// Derived units with special names
		public static readonly Unit Steradian = new Unit("sr", Dimension.SolidAngleDimension);
		public static readonly Unit Hertz = new Unit("Hz", Dimension.FrequencyDimension);
		public static readonly Unit Newton = new Unit("N", Dimension.ForceDimension);
		public static readonly Unit Pascal = new Unit("Pa", Dimension.PressureDimension);
		public static readonly Unit Joule = new Unit("J", Dimension.EnergyDimension);
		public static readonly Unit Watt = new Unit("W", Dimension.PowerDimension);
		public static readonly Unit Coulomb = new Unit("C", Dimension.ChargeDimension);
		public static readonly Unit Volt = new Unit("V", Dimension.VoltageDimension);
		public static readonly Unit Farad = new Unit("F", Dimension.CapacitanceDimension);
		public static readonly Unit Ohm = new Unit("Ω", Dimension.ResistanceDimension);
		public static readonly Unit Siemens = new Unit("S", Dimension.ConductanceDimension);
		public static readonly Unit Weber = new Unit("Wb", Dimension.MagneticFluxDimension);
		public static readonly Unit Tesla = new Unit("T", Dimension.MagneticFluxDensityDimension);
		public static readonly Unit Henry = new Unit("H", Dimension.InductanceDimension);
		public static readonly Unit Lumen = new Unit("lm", Dimension.LuminousFluxDimension);
		public static readonly Unit Lux = new Unit("lx", Dimension.IlluminanceDimension);

		// Length Units
		public static readonly Unit Centimeter = new Unit("cm", Meter, new Ratio(100));
		public static readonly Unit Kilometer = new Unit("km", Meter, new Ratio(1, 1000));
		public static readonly Unit Inch = new Unit("in", Centimeter, new Ratio(100, 254));
		public static readonly Unit Foot = new Unit("ft", Inch, new Ratio(1, 12));
		public static readonly Unit Yard = new Unit("yd", Foot, new Ratio(1, 3));
		public static readonly Unit Rod = new Unit("rd", Foot, new Ratio(10, 165));
		public static readonly Unit Chain = new Unit("ch", Rod, new Ratio(1, 4));
		public static readonly Unit Furlong = new Unit("fur", Chain, new Ratio(1, 10));

		// Time Units
		public static readonly Unit Minute = new Unit("min", Second, new Ratio(1, 60));
		public static readonly Unit Hour = new Unit("h", Minute, new Ratio(1, 60));
		public static readonly Unit Day = new Unit("d", Hour, new Ratio(1, 24));
		public static readonly Unit Week = new Unit("wk", Day, new Ratio(1, 7));
		public static readonly Unit Fortnight = new Unit("fn", Week, new Ratio(1, 2));
		public static readonly Unit Year = new Unit("a", Day, new Ratio(1, 365));

		// Temperature units
		public static readonly Unit Celsius = new Unit("°C", Kelvin, new Ratio(1), new Ratio(-27315, 100));
		public static readonly Unit Fahrenheit = new Unit("°F", Celsius, new Ratio(9, 5), new Ratio(32));
		public static readonly Unit Rankine = new Unit("°R", Kelvin, new Ratio(9, 5));

		// Other stuff
		public static readonly Unit Gram = new Unit("g", Kilogram, new Ratio(1000));
		public static readonly Unit SquareMeter = Meter * Meter;
		public static readonly Unit CubicMeter = Meter * Meter * Meter;
		public static readonly Unit Liter = new Unit("l", CubicMeter, new Ratio(1000));
		public static readonly Unit MetersPerSecond = Meter / Second;

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

		public static bool TryParseUnit(string text, out Unit result)
		{
			result = default;
			if (text.Contains("/"))
			{
				var split = text.Split('/');
				if (split.Length > 2) return false;
				if (!TryParseUnit(split[0], out var num) || !TryParseUnit(split[1], out var den)) return false;
				result = num / den;
			}
			else if (text.Contains("*"))
			{
				var units = text.Split('*')
					.Select(s => { var success = TryParseUnit(s, out var unit); return (success, unit); });
				if (units.Any(t => !t.success)) return false;
				result = units.Aggregate(Scalar, (s, t) => s * t.unit);
			}
			else if (text.Contains("^"))
			{
				var split = text.Split('^');
				if (split.Length > 2) return false;
				if (!TryParseUnit(split[0], out var unit)) return false;
				if (!Int32.TryParse(split[1], out var exp)) return false;
				result = unit ^ exp;
			}
			else
			{
				text = text.Trim();
				// Try getting a basic defined unit
				result = knownUnits.FirstOrDefault(u => u.Name == text);
				if (result.Dimension != Dimension.ScalarDimension) return true;
				// No basic unit found, try parsing a SI prefix
				if (text.Length <= 1) return false;
				var prefix = text[0];
				var ratio = Util.GetPrefixRatio(prefix);
				if (!ratio.IsValid) return false;
				// Valid prefix found, check for a unit again
				text = text.Substring(1);
				result = knownUnits.FirstOrDefault(u => u.Name == text);
				if (result.Dimension == Dimension.ScalarDimension) return false;
				result = new Unit(prefix + result.Name, result, ratio);
			}
			return true;
		}
	}
}
