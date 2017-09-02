using System;
using System.Collections.Generic;
using System.Text;

namespace Units
{
	public partial struct Dimension
	{
		public static readonly Dimension ScalarDimension = new Dimension();
		public static readonly Dimension LengthDimension = new Dimension(length: 1);
		public static readonly Dimension MassDimension = new Dimension(mass: 1);
		public static readonly Dimension TimeDimension = new Dimension(time: 1);
		public static readonly Dimension CurrentDimension = new Dimension(current: 1);
		public static readonly Dimension TemperatureDimension = new Dimension(temperature: 1);
		public static readonly Dimension SubstanceDimension = new Dimension(substance: 1);
		public static readonly Dimension LuminousIntensityDimension = new Dimension(luminousIntensity: 1);
		public static readonly Dimension AngleDimension = new Dimension(angle: 1);
	}
}
