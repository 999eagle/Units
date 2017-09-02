using System;
using System.Collections.Generic;
using System.Text;

namespace Units
{
	public partial struct Dimension
	{
		// Basic dimensions
		public static readonly Dimension ScalarDimension = new Dimension();

		public static readonly Dimension LengthDimension = new Dimension(length: 1);
		public static readonly Dimension MassDimension = new Dimension(mass: 1);
		public static readonly Dimension TimeDimension = new Dimension(time: 1);
		public static readonly Dimension CurrentDimension = new Dimension(current: 1);
		public static readonly Dimension TemperatureDimension = new Dimension(temperature: 1);
		public static readonly Dimension SubstanceDimension = new Dimension(substance: 1);
		public static readonly Dimension LuminousIntensityDimension = new Dimension(luminousIntensity: 1);
		public static readonly Dimension AngleDimension = new Dimension(angle: 1);

		// Dimenions of derived units
		public static readonly Dimension SolidAngleDimension = AngleDimension * AngleDimension;
		public static readonly Dimension FrequencyDimension = ScalarDimension / TimeDimension;
		public static readonly Dimension ForceDimension = MassDimension * LengthDimension / (TimeDimension ^ 2);
		public static readonly Dimension PressureDimension = ForceDimension / (LengthDimension ^ 2);
		public static readonly Dimension EnergyDimension = ForceDimension * LengthDimension;
		public static readonly Dimension PowerDimension = EnergyDimension / TimeDimension;
		public static readonly Dimension ChargeDimension = TimeDimension * CurrentDimension;
		public static readonly Dimension VoltageDimension = PowerDimension / CurrentDimension;
		public static readonly Dimension CapacitanceDimension = ChargeDimension / VoltageDimension;
		public static readonly Dimension ResistanceDimension = VoltageDimension / CurrentDimension;
		public static readonly Dimension ConductanceDimension = CurrentDimension / VoltageDimension;
		public static readonly Dimension MagneticFluxDimension = VoltageDimension * TimeDimension;
		public static readonly Dimension MagneticFluxDensityDimension = MagneticFluxDimension / (LengthDimension ^ 2);
		public static readonly Dimension InductanceDimension = MagneticFluxDimension / CurrentDimension;
		public static readonly Dimension LuminousFluxDimension = LuminousIntensityDimension * SolidAngleDimension;
		public static readonly Dimension IlluminanceDimension = LuminousFluxDimension / (LengthDimension ^ 2);
	}
}
