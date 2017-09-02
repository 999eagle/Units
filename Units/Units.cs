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
		public static readonly Unit SquareMeter = new Unit("m^2", Meter * Meter);
		public static readonly Unit CubicMeter = new Unit("m^3", Meter * Meter * Meter);
		public static readonly Unit Liter = new Unit("l", CubicMeter, new Ratio(1000));
		public static readonly Unit MetersPerSecond = new Unit("m/s", Meter / Second);

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

		private static IEnumerable<(string name, int exponent)> ParseUnits(string text)
		{
			if (text.Contains("/"))
			{
				var split = text.Split('/');
				if (split.Length > 2) return null;
				return ParseUnits(split[0]).Union(ParseUnits(split[1]).Select(t => (t.name, -t.exponent)));
			}
			else if (text.Contains("*"))
			{
				var split = text.Split('*');
				return split.Select(s => ParseUnits(s)).Aggregate(Enumerable.Union);
			}
			else if (text.Contains("^"))
			{
				var split = text.Split('^');
				if (split.Length > 2) return null;
				if (!Int32.TryParse(split[1], out var exp)) return null;
				return new[] { (name: split[0], exponent: exp) };
			}
			else
			{
				return new[] { (name: text, exponent: 1) };
			}
		}

		private static string CreateName(IEnumerable<(string name, int exponent)> units)
		{
			string SingleUnitName(string name, int exponent) => $"{name}{(exponent > 1 ? $"^{exponent}" : "")}";

			string num = String.Join("*", units.Where(t => t.exponent > 0).Select(t => SingleUnitName(t.name, t.exponent)));
			string den = String.Join("*", units.Where(t => t.exponent < 0).Select(t => SingleUnitName(t.name, -t.exponent)));
			if (num == "") { num = "1"; }
			if (den == "") { return num; }
			else { return $"{num}/{den}"; }
		}

		private static (IEnumerable<(string name, int exponent)> basicUnits, Unit unit, bool success) ParseUnitInternal(string text)
		{
			(IEnumerable<(string name, int exponent)> basicUnits, Unit unit, bool success) failed = (null, default, false);
			(IEnumerable<(string name, int exponent)> basicUnits, Unit unit) result;

			if (text.Contains("/"))
			{
				var split = text.Split('/');
				if (split.Length > 2) return failed;
				var num = ParseUnitInternal(split[0]);
				var den = ParseUnitInternal(split[1]);
				if (!num.success || !den.success) return failed;

				result = (num.basicUnits.Union(den.basicUnits.Select(t => (t.name, -t.exponent))), num.unit / den.unit);
			}
			else if (text.Contains("*"))
			{
				var units = text.Split('*').Select(ParseUnitInternal);
				if (units.Any(t => !t.success)) return failed;

				result = (units.Select(t => t.basicUnits).Aggregate(Enumerable.Union), units.Select(t => t.unit).Aggregate(Scalar, (s, t) => s * t));
			}
			else if (text.Contains("^"))
			{
				var split = text.Split('^');
				if (split.Length > 2) return failed;
				if (!Int32.TryParse(split[1], out var exp)) return failed;
				if (exp == 0)
				{
					result = (new[] { (name: "1", exponent: 0) }, Scalar);
				}
				else
				{
					var unit = ParseUnitInternal(split[0]);
					if (!unit.success) return failed;
					result = (new[] { (name: unit.basicUnits.First().name, exponent: exp) }, unit.unit ^ exp);
				}
			}
			else
			{
				text = text.Trim();
				// Try getting a basic defined unit
				var unit = knownUnits.FirstOrDefault(u => u.Name == text);
				if (unit.Dimension == Dimension.ScalarDimension)
				{
					// No basic unit found, try parsing a SI prefix
					if (text.Length <= 1) return failed;
					var prefix = text[0];
					var ratio = Util.GetPrefixRatio(prefix);
					if (!ratio.IsValid) return failed;
					// Valid prefix found, check for a unit again
					var unprefixedName = text.Substring(1);
					unit = knownUnits.FirstOrDefault(u => u.Name == unprefixedName);
					if (unit.Dimension == Dimension.ScalarDimension) return failed;
				}
				result = (new[] { (name: text, exponent: 1) }, unit);
			}

			return (result.basicUnits, result.unit, true);
		}

		public static bool TryParseUnit(string text, out Unit result)
		{
			var parsed = ParseUnitInternal(text);
			if (!parsed.success)
			{
				result = default;
				return false;
			}
			// Create normalized name for parsed unit
			var name = CreateName(parsed.basicUnits);
			var unit = new Unit(name, parsed.unit);
			// Store new unit as known unit
			var knownUnit = knownUnits.FirstOrDefault(u => u == unit);
			if (knownUnit.Dimension != unit.Dimension)
			{
				knownUnits.Add(unit);
				result = unit;
			}
			else
			{
				// return known unit if one exists
				result = knownUnit;
			}
			return true;
		}
	}
}
