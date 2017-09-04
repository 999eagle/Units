using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Units;

namespace UnitsTests
{
	[TestClass]
	public class ConversionTests
	{
		[TestMethod]
		public void TestTemperature()
		{
			var c = new Measurement(-40, Unit.Celsius);
			var f = new Measurement(-40, Unit.Fahrenheit);

			Assert.AreEqual(f, c.ConvertTo(Unit.Fahrenheit));

			var converted = new Measurement(90, Unit.Fahrenheit).ConvertTo(Unit.Celsius).Value;
			if (Math.Abs((double)converted - 32.22222222) > 0.000001) { Assert.Fail(); }
		}

		[TestMethod]
		public void TestVelocity()
		{
			var kmh = new Measurement(36, Unit.Kilometer / Unit.Hour);
			var ms = new Measurement(10, Unit.MeterPerSecond);

			Assert.AreEqual(10, kmh.ConvertTo(Unit.MeterPerSecond).Value);
			Assert.AreEqual(36, ms.ConvertTo(Unit.Kilometer / Unit.Hour).Value);
		}

		[TestMethod]
		public void TestConvertability()
		{
			Assert.IsTrue(Unit.Hour.CanConvertTo(Unit.Second));
			Assert.IsFalse(Unit.Radian.CanConvertTo(Unit.Scalar));
			Assert.IsFalse(Unit.SquareMeter.CanConvertTo(Unit.Meter));
			Assert.IsTrue(Unit.SquareMeter.CanConvertTo(Unit.Length.Furlong * Unit.Foot));
			Assert.IsTrue(Unit.Conductance.Siemens.CanConvertTo(Unit.Ampere / Unit.Volt));
		}
	}
}
