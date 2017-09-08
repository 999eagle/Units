using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Units
{
	public partial struct Unit
	{
		#region Basic Units
		public static readonly Unit Scalar = new Unit("1", Dimension.ScalarDimension);

		#region Length Units
		public static class Length
		{
			public static readonly Unit Meter = new Unit("m", Dimension.LengthDimension);

			public static readonly Unit Nanometer = new Unit("nm", Meter, new Ratio(1000_000_000));
			public static readonly Unit Micrometer = new Unit("µm", Meter, new Ratio(1000_000));
			public static readonly Unit Millimeter = new Unit("mm", Meter, new Ratio(1000));
			public static readonly Unit Centimeter = new Unit("cm", Meter, new Ratio(100));
			public static readonly Unit Kilometer = new Unit("km", Meter, new Ratio(1, 1000));

			public static readonly Unit AstronomicalUnit = new Unit("au", Meter, new Ratio(1, 149597870700));
			public static readonly Unit LightYear = new Unit("ly", Meter, new Ratio(1, 9_460_730_472_480_800));
			public static readonly Unit Parsec = new Unit("pc", AstronomicalUnit, new Ratio(1000_000, 206_264_806_247));
			public static readonly Unit Attoparsec = new Unit("apc", Parsec, new Ratio(1000_000_000_000_000_000));

			public static readonly Unit Inch = new Unit("in", Centimeter, new Ratio(100, 254));
			public static readonly Unit Foot = new Unit("ft", Inch, new Ratio(1, 12));
			public static readonly Unit Yard = new Unit("yd", Foot, new Ratio(1, 3));
			public static readonly Unit Rod = new Unit("rd", Foot, new Ratio(10, 165));
			public static readonly Unit Chain = new Unit("ch", Rod, new Ratio(1, 4));
			public static readonly Unit Furlong = new Unit("fur", Chain, new Ratio(1, 10));
			public static readonly Unit Mile = new Unit("mi", Yard, new Ratio(1, 1760));

			public static readonly Unit NauticalMile = new Unit("nmi", Meter, new Ratio(1, 1852));

			public static readonly Unit Point = new Unit("pt", Inch, new Ratio(72));
			public static readonly Unit Twip = new Unit("tw", Inch, new Ratio(1440));

			public static readonly Unit BeardSecond = new Unit("bs", Nanometer, new Ratio(1, 10));
			public static readonly Unit Smoot = new Unit("smoot", Inch, new Ratio(1, 67));
		}
		public static readonly Unit Meter = Length.Meter;
		public static readonly Unit Centimeter = Length.Centimeter;
		public static readonly Unit Kilometer = Length.Kilometer;
		public static readonly Unit Inch = Length.Inch;
		public static readonly Unit Foot = Length.Foot;
		public static readonly Unit Yard = Length.Yard;
		public static readonly Unit Mile = Length.Mile;
		#endregion

		#region Mass Units
		public static class Mass
		{
			public static readonly Unit Kilogram = new Unit("kg", Dimension.MassDimension);
			public static readonly Unit Gram = new Unit("g", Kilogram, new Ratio(1000));
			public static readonly Unit Tonne = new Unit("t", Kilogram, new Ratio(1, 1000));
			public static readonly Unit Carat = new Unit("ct", Gram, new Ratio(5));

			public static readonly Unit Pound = new Unit("lb", Kilogram, new Ratio(1_000_000_00, 0_453_592_37));
			public static readonly Unit Ounce = new Unit("oz", Pound, new Ratio(16));
			public static readonly Unit Grain = new Unit("gr", Pound, new Ratio(7000));
			public static readonly Unit TroyOunce = new Unit("ozt", Grain, new Ratio(1, 480));
			public static readonly Unit Firkin = new Unit("fir", Pound, new Ratio(1, 90));

			public static readonly Unit UnifiedAtomicMassUnit = new Unit("u", Kilogram, new Ratio(BigInteger.Pow(10, 27) * 1_000_000_000, 1_660_539_040));
		}
		public static readonly Unit Kilogram = Mass.Kilogram;
		public static readonly Unit Pound = Mass.Pound;
		public static readonly Unit Ounce = Mass.Ounce;
		#endregion

		#region Time Units
		public static class Time
		{
			public static readonly Unit Second = new Unit("s", Dimension.TimeDimension);
			public static readonly Unit Minute = new Unit("min", Second, new Ratio(1, 60));
			public static readonly Unit Hour = new Unit("h", Minute, new Ratio(1, 60));
			public static readonly Unit Day = new Unit("d", Hour, new Ratio(1, 24));
			public static readonly Unit Week = new Unit("wk", Day, new Ratio(1, 7));
			public static readonly Unit Fortnight = new Unit("fn", Week, new Ratio(1, 2));
			public static readonly Unit Year = new Unit("a", Day, new Ratio(1, 365));
			public static readonly Unit Century = new Unit("c", Year, new Ratio(1, 100));
			public static readonly Unit Microfortnight = new Unit("μfn", Fortnight, new Ratio(1000_000));
		}
		public static readonly Unit Second = Time.Second;
		public static readonly Unit Minute = Time.Minute;
		public static readonly Unit Hour = Time.Hour;
		#endregion

		#region Electric Current Units
		public static class ElectricCurrent
		{
			public static readonly Unit Ampere = new Unit("A", Dimension.ElectricCurrentDimension);
		}
		public static readonly Unit Ampere = ElectricCurrent.Ampere;
		#endregion

		#region Temperature Units
		public static class Temperature
		{
			public static readonly Unit Kelvin = new Unit("K", Dimension.TemperatureDimension);
			public static readonly Unit Celsius = new Unit("°C", Kelvin, new Ratio(1), new Ratio(-27315, 100));
			public static readonly Unit Fahrenheit = new Unit("°F", Celsius, new Ratio(9, 5), new Ratio(32));
			public static readonly Unit Rankine = new Unit("°R", Kelvin, new Ratio(9, 5));
		}
		public static readonly Unit Kelvin = Temperature.Kelvin;
		public static readonly Unit Celsius = Temperature.Celsius;
		public static readonly Unit Fahrenheit = Temperature.Fahrenheit;
		#endregion

		#region Substance Units
		public static class Substance
		{
			public static readonly Unit Mole = new Unit("mol", Dimension.SubstanceDimension);
		}
		public static readonly Unit Mole = Substance.Mole;
		#endregion

		#region Luminous Intensity Units
		public static class LuminousIntensity
		{
			public static readonly Unit Candela = new Unit("cd", Dimension.LuminousIntensityDimension);
		}
		public static readonly Unit Candela = LuminousIntensity.Candela;
		#endregion

		#region Plane Angle Units
		public static class PlaneAngle
		{
			public static readonly Unit Radian = new Unit("rad", Dimension.PlaneAngleDimension);
			public static readonly Unit Degree = new Unit("°", Radian, new Ratio(180_000_000_000, 3_141592654));
			public static readonly Unit ArcMinute = new Unit("'", Degree, new Ratio(60));
			public static readonly Unit ArcSecond = new Unit("\"", ArcMinute, new Ratio(60));
			public static readonly Unit Grad = new Unit("grad", Radian, new Ratio(200_000_000_000, 3_141592654));
		}
		public static readonly Unit Radian = PlaneAngle.Radian;
		public static readonly Unit Degree = PlaneAngle.Degree;
		public static readonly Unit Grad = PlaneAngle.Grad;
		#endregion

		#endregion

		// No dependencies
		#region Solid Angle Units
		public static class SolidAngle
		{
			public static readonly Unit Steradian = new Unit("sr", Dimension.SolidAngleDimension);
		}
		public static readonly Unit Steradian = SolidAngle.Steradian;
		#endregion

		// No dependencies
		#region Frequency Units
		public static class Frequency
		{
			public static readonly Unit Hertz = new Unit("Hz", Dimension.FrequencyDimension);
		}
		public static readonly Unit Herz = Frequency.Hertz;
		#endregion

		// No dependencies
		#region Voltage Units
		public class Voltage
		{
			public static readonly Unit Volt = new Unit("V", Dimension.VoltageDimension);
		}
		public static readonly Unit Volt = Voltage.Volt;
		#endregion

		// No dependencies
		#region Capacitance Units
		public static class Capacitance
		{
			public static readonly Unit Farad = new Unit("F", Dimension.CapacitanceDimension);
		}
		public static readonly Unit Farad = Capacitance.Farad;
		#endregion

		// No dependencies
		#region Electric Resistance Units
		public static class ElectricResistance
		{
			public static readonly Unit Ohm = new Unit("Ω", Dimension.ElectricResistanceDimension);
		}
		public static readonly Unit Ohm = ElectricResistance.Ohm;
		#endregion

		// No dependencies
		#region Conductance Units
		public static class Conductance
		{
			public static readonly Unit Siemens = new Unit("S", Dimension.ConductanceDimension);
		}
		#endregion

		// No dependencies
		#region Magnetic Flux Units
		public static class MagneticFlux
		{
			public static readonly Unit Weber = new Unit("Wb", Dimension.MagneticFluxDimension);
			public static readonly Unit Maxwell = new Unit("Mx", Weber, new Ratio(100000000));
		}
		#endregion

		// No dependencies
		#region Magnetic Flux Density Units
		public static class MagneticFluxDensity
		{
			public static readonly Unit Tesla = new Unit("T", Dimension.MagneticFluxDensityDimension);
			public static readonly Unit Gauss = new Unit("G", Tesla, new Ratio(10000));
		}
		#endregion

		// No dependencies
		#region Inductance Units
		public static class Inductance
		{
			public static readonly Unit Henry = new Unit("H", Dimension.InductanceDimension);
		}
		#endregion

		// No dependencies
		#region Luminous Flux Units
		public static class LuminousFlux
		{
			public static readonly Unit Lumen = new Unit("lm", Dimension.LuminousFluxDimension);
		}
		#endregion

		// Depends on Length
		#region Area Units
		public static class Area
		{
			public static readonly Unit SquareMeter = new Unit("m^2", Length.Meter * Length.Meter);
			public static readonly Unit SquareKilometer = new Unit("km^2", Length.Kilometer * Length.Kilometer);
			public static readonly Unit SquareInch = new Unit("sq in", Length.Inch * Length.Inch);
			public static readonly Unit SquareFoot = new Unit("sq ft", Length.Foot * Length.Foot);
			public static readonly Unit SquareYard = new Unit("sq yd", Length.Yard * Length.Yard);
			public static readonly Unit SquareMile = new Unit("sq mi", Length.Mile * Length.Mile);

			public static readonly Unit Are = new Unit("a", SquareMeter, new Ratio(1, 100));
			public static readonly Unit Hectare = new Unit("ha", Are, new Ratio(1, 100));
		}
		public static readonly Unit SquareMeter = Area.SquareMeter;
		public static readonly Unit SquareInch = Area.SquareInch;
		public static readonly Unit SquareFoot = Area.SquareFoot;
		#endregion

		// Depends on Length
		#region Volume Units
		public static class Volume
		{
			public static readonly Unit CubicMeter = new Unit("m^3", Length.Meter * Length.Meter * Length.Meter);
			public static readonly Unit CubicInch = new Unit("cu in", Length.Inch * Length.Inch * Length.Inch);
			public static readonly Unit CubicFoot = new Unit("cu ft", Length.Foot * Length.Foot * Length.Foot);
			public static readonly Unit CubicYard = new Unit("cu yd", Length.Yard * Length.Yard * Length.Yard);
			public static readonly Unit CubicMile = new Unit("cu mi", Length.Mile * Length.Mile * Length.Mile);
			public static readonly Unit Liter = new Unit("l", CubicMeter, new Ratio(1000));
			public static readonly Unit Milliliter = new Unit("ml", Liter, new Ratio(1000));

			public static readonly Unit GallonUS = new Unit("gal (US)", CubicInch, new Ratio(1, 231));
			public static readonly Unit QuartUS = new Unit("qt (US)", GallonUS, new Ratio(4));
			public static readonly Unit FluidOunceUS = new Unit("fl oz (US)", GallonUS, new Ratio(128));
			public static readonly Unit PintUS = new Unit("pt (US)", GallonUS, new Ratio(8));
			public static readonly Unit TablespoonUS = new Unit("tbsp (US)", FluidOunceUS, new Ratio(6));
			public static readonly Unit CupUS = new Unit("c (US)", FluidOunceUS, new Ratio(1, 8));

			public static readonly Unit GallonImperial = new Unit("gal (imp)", Liter, new Ratio(1_00000, 4_54609));
			public static readonly Unit QuartImperial = new Unit("qt (imp)", GallonImperial, new Ratio(4));
			public static readonly Unit FluidOunceImperial = new Unit("fl oz (imp)", GallonImperial, new Ratio(160));
			public static readonly Unit PintImperial = new Unit("pt (imp)", GallonImperial, new Ratio(8));
			public static readonly Unit TablespoonImperial = new Unit("tbsp (imp)", FluidOunceImperial, new Ratio(8, 5));
		}
		public static readonly Unit Liter = Volume.Liter;
		public static readonly Unit CubicMeter = Volume.CubicMeter;
		public static readonly Unit CubicFoot = Volume.CubicFoot;
		#endregion

		// Depends on Length, Time
		#region Velocity Units
		public static class Velocity
		{
			public static readonly Unit MeterPerSecond = new Unit("m/s", Length.Meter / Time.Second);
			public static readonly Unit KilometerPerHour = new Unit("km/h", Length.Kilometer / Time.Hour);
			public static readonly Unit KilometerPerMinute = new Unit("km/min", Length.Kilometer / Time.Minute);
			public static readonly Unit KilometerPerSecond = new Unit("km/s", Length.Kilometer / Time.Second);

			public static readonly Unit MilePerHour = new Unit("mph", Length.Mile / Time.Hour);
			public static readonly Unit MilePerMinute = new Unit("mpm", Length.Mile / Time.Minute);
			public static readonly Unit MilePerSecond = new Unit("mps", Length.Mile / Time.Second);
			public static readonly Unit FootPerHour = new Unit("fph", Length.Foot / Time.Hour);
			public static readonly Unit FootPerMinute = new Unit("fpm", Length.Foot / Time.Minute);
			public static readonly Unit FootPerSecond = new Unit("fps", Length.Foot / Time.Second);
			public static readonly Unit InchPerHour = new Unit("iph", Length.Inch / Time.Hour);
			public static readonly Unit InchPerMinute = new Unit("ipm", Length.Inch / Time.Minute);
			public static readonly Unit InchPerSecond = new Unit("ips", Length.Inch / Time.Second);

			public static readonly Unit Knot = new Unit("kn", Length.NauticalMile / Time.Hour);

			public static readonly Unit FurlongPerFortnight = new Unit("fur/fn", Length.Furlong / Time.Fortnight);
			public static readonly Unit AttoparsecPerMicrofortnight = new Unit("apc/μfn", Length.Attoparsec / Time.Microfortnight);

			public static readonly Unit SpeedOfLight = new Unit("c", MeterPerSecond, new Ratio(1, 299_792_458));
		}
		public static readonly Unit MeterPerSecond = Velocity.MeterPerSecond;
		public static readonly Unit KilometerPerHour = Velocity.KilometerPerHour;
		public static readonly Unit MilePerHour = Velocity.MilePerHour;
		public static readonly Unit Knot = Velocity.Knot;
		#endregion

		// Depends on Electric Current, Time
		#region Electric Charge Units
		public static class ElectricCharge
		{
			public static readonly Unit Coulomb = new Unit("C", Dimension.ElectricChargeDimension);
			public static readonly Unit MilliampereHour = new Unit("mA*h", ElectricCurrent.Ampere * Time.Hour, new Ratio(1000));
		}
		public static readonly Unit Coulomb = ElectricCharge.Coulomb;
		public static readonly Unit MilliampereHour = ElectricCharge.MilliampereHour;
		#endregion

		// Depends on Mass, Volume
		#region Density Units
		public static class Density
		{
			public static readonly Unit KilogramPerCubicMeter = new Unit("kg/m^3", Mass.Kilogram / Volume.CubicMeter);
			public static readonly Unit GramPerMilliliter = new Unit("g/ml", Mass.Gram / Volume.Milliliter);
			public static readonly Unit PoundPerCubicFoot = new Unit("lb/ft^3", Mass.Pound / Volume.CubicFoot);
			public static readonly Unit PoundPerCubicInch = new Unit("lb/in^3", Mass.Pound / Volume.CubicInch);
			public static readonly Unit OuncePerCubicFoot = new Unit("oz/ft^3", Mass.Ounce / Volume.CubicFoot);
			public static readonly Unit OuncePerCubicInch = new Unit("oz/in^3", Mass.Ounce / Volume.CubicInch);
		}
		public static readonly Unit KilogramPerCubicMeter = Density.KilogramPerCubicMeter;
		public static readonly Unit GramPerMilliliter = Density.GramPerMilliliter;
		public static readonly Unit PoundPerCubicFoot = Density.PoundPerCubicFoot;
		public static readonly Unit OuncePerCubicInch = Density.OuncePerCubicInch;
		#endregion

		// Depends on Luminous Flux, Area
		#region Illuminance Units
		public static class Illuminance
		{
			public static readonly Unit Lux = new Unit("lx", Dimension.IlluminanceDimension);
			public static readonly Unit LumenPerSquareInch = new Unit("lm/in^2", LuminousFlux.Lumen / Area.SquareInch);
			public static readonly Unit Footcandle = new Unit("fc", LuminousFlux.Lumen / Area.SquareFoot);
		}
		#endregion

		// Depends on Volume, Time
		#region Flow Units
		public static class Flow
		{
			public static readonly Unit CubicMeterPerSecond = new Unit("m^3/s", Volume.CubicMeter / Time.Second);
			public static readonly Unit LiterPerMinute = new Unit("l/min", Volume.Liter / Time.Minute);

			public static readonly Unit CubicInchPerSecond = new Unit("in^3/s", Volume.CubicInch / Time.Second);
			public static readonly Unit CubicInchPerMinute = new Unit("in^3/min", Volume.CubicInch / Time.Minute);
			public static readonly Unit CubicFootPerSecond = new Unit("ft^3/s", Volume.CubicFoot / Time.Second);
			public static readonly Unit CubicFootPerMinute = new Unit("ft^3/min", Volume.CubicFoot / Time.Minute);
		}
		#endregion

		// Depends on Length, Time, Velocity
		#region Acceleration Units
		public static class Acceleration
		{
			public static readonly Unit MeterPerSecondSquared = new Unit("m/s^2", Length.Meter / (Time.Second ^ 2));
			public static readonly Unit StandardGravity = new Unit("g", MeterPerSecondSquared, new Ratio(1_000_00, 9_806_65));
			public static readonly Unit KilometerPerHourPerSecond = new Unit("km/h*s", Velocity.KilometerPerHour / Time.Second);
			public static readonly Unit MilePerHourPerSecond = new Unit("mph/s", Velocity.MilePerHour / Time.Second);
			public static readonly Unit KnotPerSecond = new Unit("kn/s", Velocity.Knot / Time.Second);
		}
		public static readonly Unit MeterPerSecondSquared = Acceleration.MeterPerSecondSquared;
		#endregion

		// Depends on Area, Time
		#region Kinematic Viscosity Units
		public static class KinematicViscosity
		{
			public static readonly Unit SquareMeterPerSecond = new Unit("m^2/s", Area.SquareMeter / Time.Second);
			public static readonly Unit SquareFootPerSecond = new Unit("ft^2/s", Area.SquareFoot / Time.Second);
		}
		#endregion

		// Depends on Electric Charge, Length
		#region Electric Dipole Units
		public static class ElectricDipole
		{
			public static readonly Unit CoulombMeter = new Unit("C*m", ElectricCharge.Coulomb * Length.Meter);
		}
		#endregion

		// Depends on Luminous Intensity, Area
		#region Luminance Units
		public static class Luminance
		{
			public static readonly Unit CandelaPerSquareMeter = new Unit("cd/m^2", LuminousIntensity.Candela / Area.SquareMeter);
			public static readonly Unit CandelaPerSquareFoot = new Unit("cd/ft^2", LuminousIntensity.Candela / Area.SquareFoot);
			public static readonly Unit CandelaPerSquareInch = new Unit("cd/in^2", LuminousIntensity.Candela / Area.SquareInch);
			public static readonly Unit Lambert = new Unit("L", CandelaPerSquareMeter, new Ratio(3_141_592_653, 1000_000_000_000));
		}
		#endregion

		// Depends on Mass, Acceleration, Length, Time
		#region Force Units
		public static class Force
		{
			public static readonly Unit Newton = new Unit("N", Dimension.ForceDimension);
			public static readonly Unit Kilopond = new Unit("kp", Mass.Kilogram * Acceleration.StandardGravity);
			public static readonly Unit KilogramForce = Kilopond;
			public static readonly Unit PoundForce = new Unit("lbf", Mass.Pound * Acceleration.StandardGravity);
			public static readonly Unit OunceForce = new Unit("ozf", Mass.Ounce * Acceleration.StandardGravity);
			public static readonly Unit Poundal = new Unit("pdl", Mass.Pound * Length.Foot / (Time.Second ^ 2));
		}
		public static readonly Unit Newton = Force.Newton;
		public static readonly Unit PoundForce = Force.PoundForce;
		#endregion

		// Depends on Force, Area
		#region Pressure Units
		public static class Pressure
		{
			public static readonly Unit Pascal = new Unit("Pa", Dimension.PressureDimension);
			public static readonly Unit Atmosphere = new Unit("atm", Pascal, new Ratio(1, 101325));
			public static readonly Unit Bar = new Unit("bar", Pascal, new Ratio(1, 100000));
			public static readonly Unit PoundPerSquareInch = new Unit("psi", Force.PoundForce / Area.SquareInch);
			public static readonly Unit PoundalPerSquareInch = new Unit("pdl/sq ft", Force.Poundal / Area.SquareInch);
			public static readonly Unit MillimeterOfMercury = new Unit("mmHg", Pascal, new Ratio(1_000, 133_3224));
			public static readonly Unit MeterOfMercury = new Unit("mHg", MillimeterOfMercury, new Ratio(1, 1000));
		}
		public static readonly Unit Pascal = Pressure.Pascal;
		public static readonly Unit PoundPerSquareInch = Pressure.PoundPerSquareInch;
		#endregion

		// Depends on Force, Length
		#region Torque Units
		public static class Torque
		{
			public static readonly Unit NewtonMeter = new Unit("N*m", Force.Newton * Length.Meter);
			public static readonly Unit MeterKilogramForce = new Unit("m*kgf", Length.Meter * Force.Kilopond);
			public static readonly Unit FootPoundForce = new Unit("ft*lbf", Length.Foot * Force.PoundForce);
			public static readonly Unit FootPoundal = new Unit("ft*pdl", Length.Foot * Force.Poundal);
		}
		#endregion

		// Depends on Pressure, Time, Length, Force, Area, Mass
		#region Dynamic Viscosity Units
		public static class DynamicViscosity
		{
			public static readonly Unit PascalSecond = new Unit("Pa*s", Pressure.Pascal * Time.Second);
			public static readonly Unit PoundPerFootSecond = new Unit("lb/ft*s", Mass.Pound / (Length.Foot * Time.Second));
			public static readonly Unit PoundForceSecondPerSquareFoot = new Unit("lbf*s/ft^2", Force.PoundForce * Time.Second / Area.SquareFoot);
			public static readonly Unit PoundForceSecondPerSquareInch = new Unit("lbf*s/in^2", Force.PoundForce * Time.Second / Area.SquareInch);
		}
		#endregion

		// Depends on Time, Length, Force
		#region Power Units
		public static class Power
		{
			public static readonly Unit Watt = new Unit("W", Dimension.PowerDimension);
			public static readonly Unit Kilowatt = new Unit("kW", Watt, new Ratio(1, 1000));
			public static readonly Unit Horsepower = new Unit("hp", Length.Foot * Force.PoundForce / Time.Second, new Ratio(1, 550));
			public static readonly Unit FootPoundForcePerSecond = new Unit("ft*lbf/s", Length.Foot * Force.PoundForce / Time.Second);
			public static readonly Unit FootPoundForcePerMinute = new Unit("ft*lbf/min", Length.Foot * Force.PoundForce / Time.Minute);
		}
		public static readonly Unit Watt = Power.Watt;
		public static readonly Unit Kilowatt = Power.Kilowatt;
		#endregion

		// Depends on Length, Force, Power, Time
		#region Energy Units
		public static class Energy
		{
			public static readonly Unit Joule = new Unit("J", Dimension.EnergyDimension);
			public static readonly Unit Calorie = new Unit("cal", Joule, new Ratio(1_0000, 4_1855));
			public static readonly Unit Kilocalorie = new Unit("kcal", Calorie, new Ratio(1, 1000));
			public static readonly Unit FootPoundForce = new Unit("ft*lbf", Length.Foot * Force.PoundForce);
			public static readonly Unit FootPoundal = new Unit("ft*pdl", Length.Foot * Force.Poundal);
			public static readonly Unit KilowattHour = new Unit("kW*h", Power.Kilowatt * Time.Hour);
			public static readonly Unit HorsepowerHour = new Unit("hp*h", Power.Horsepower * Time.Hour);
		}
		public static readonly Unit Joule = Energy.Joule;
		public static readonly Unit Kilocalorie = Energy.Kilocalorie;
		public static readonly Unit KilowattHour = Energy.KilowattHour;
		#endregion

		private static IList<Unit> knownUnits =
			typeof(Unit).GetNestedTypes()
			.Where(t => t.IsNestedPublic && t.IsClass)
			.Select(t =>
				t.GetFields()
					.Where(f => f.IsStatic && f.IsPublic && f.FieldType == typeof(Unit))
					.Select(f => (Unit)f.GetValue(null)))
			.Aggregate(Enumerable.Union)
			.Union(new[] { Scalar })
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
				var units = text.Split('*').Select(ParseUnitInternal).ToList();
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
					unit = new Unit(text, unit, ratio);
				}
				result = (new[] { (name: text, exponent: 1) }, unit);
			}

			return (result.basicUnits.ToList(), result.unit, true);
		}

		public static bool TryParse(string text, out Unit result)
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

		public static Unit Parse(string text)
		{
			if (TryParse(text, out var unit)) return unit;
			throw new FormatException("Can't parse unit.");
		}
	}
}
