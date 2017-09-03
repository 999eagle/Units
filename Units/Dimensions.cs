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
		public static readonly Dimension ElectricCurrentDimension = new Dimension(current: 1);
		public static readonly Dimension TemperatureDimension = new Dimension(temperature: 1);
		public static readonly Dimension SubstanceDimension = new Dimension(substance: 1);
		public static readonly Dimension LuminousIntensityDimension = new Dimension(luminousIntensity: 1);
		public static readonly Dimension PlaneAngleDimension = new Dimension(angle: 1);

		// Dimenions of derived units with special names
		public static readonly Dimension SolidAngleDimension = PlaneAngleDimension * PlaneAngleDimension;
		public static readonly Dimension FrequencyDimension = ScalarDimension / TimeDimension;
		public static readonly Dimension ForceDimension = MassDimension * LengthDimension / (TimeDimension ^ 2);
		public static readonly Dimension PressureDimension = ForceDimension / (LengthDimension ^ 2);
		public static readonly Dimension EnergyDimension = ForceDimension * LengthDimension;
		public static readonly Dimension PowerDimension = EnergyDimension / TimeDimension;
		public static readonly Dimension ElectricChargeDimension = TimeDimension * ElectricCurrentDimension;
		public static readonly Dimension VoltageDimension = PowerDimension / ElectricCurrentDimension;
		public static readonly Dimension CapacitanceDimension = ElectricChargeDimension / VoltageDimension;
		public static readonly Dimension ElectricResistanceDimension = VoltageDimension / ElectricCurrentDimension;
		public static readonly Dimension ConductanceDimension = ElectricCurrentDimension / VoltageDimension;
		public static readonly Dimension MagneticFluxDimension = VoltageDimension * TimeDimension;
		public static readonly Dimension MagneticFluxDensityDimension = MagneticFluxDimension / (LengthDimension ^ 2);
		public static readonly Dimension InductanceDimension = MagneticFluxDimension / ElectricCurrentDimension;
		public static readonly Dimension LuminousFluxDimension = LuminousIntensityDimension * SolidAngleDimension;
		public static readonly Dimension IlluminanceDimension = LuminousFluxDimension / (LengthDimension ^ 2);

		// Dimensions of other derived units
		public static readonly Dimension AreaDimension = LengthDimension ^ 2;
		public static readonly Dimension VolumeDimension = LengthDimension ^ 3;
		public static readonly Dimension DensityDimension = MassDimension / VolumeDimension;
		public static readonly Dimension VelocityDimension = LengthDimension / TimeDimension;
		public static readonly Dimension FlowDimension = VolumeDimension / TimeDimension;
		public static readonly Dimension AccelerationDimension = VelocityDimension / TimeDimension;
		public static readonly Dimension TorqueDimension = ForceDimension * LengthDimension;
		public static readonly Dimension DynamicViscosityDimension = PressureDimension * TimeDimension;
		public static readonly Dimension KinematicViscosityDimension = DynamicViscosityDimension / DensityDimension;
		public static readonly Dimension ElectricDipoleDimension = ElectricChargeDimension * LengthDimension;
		public static readonly Dimension LuminanceDimension = LuminousIntensityDimension / (LengthDimension ^ 2);
	}
}
