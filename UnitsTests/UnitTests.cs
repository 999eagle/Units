using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Units;

namespace UnitsTests
{
	[TestClass]
	public class UnitTests
	{
		[TestMethod]
		public void TestUnits()
		{
			Assert.IsTrue(Unit.SquareMeter == (Unit.Meter ^ 2));
			Assert.IsFalse(Unit.SquareMeter == (Unit.Meter * Unit.Foot));
			Assert.IsTrue(Unit.SquareMeter == (Unit.Meter * Unit.Meter));
			Assert.IsTrue(Unit.SquareMeter == ((Unit.Meter ^ 8) * (Unit.CubicMeter ^ -2)));

			var u = Unit.GetUnitForDimension(Dimension.MassDimension / Dimension.TimeDimension);
			Assert.AreEqual(Unit.Kilogram / Unit.Second, u);

			Assert.AreEqual(Unit.Kilogram, Unit.GetUnitForDimension(Dimension.MassDimension));
		}

		[TestMethod]
		public void TestParsing()
		{
			Unit ParseUnit(string text)
			{
				if (!Unit.TryParseUnit(text, out var unit)) throw new Exception();
				return unit;
			}

			Assert.AreEqual(Unit.Kilogram / Unit.Second, ParseUnit("kg/s"));
			Assert.AreEqual(Unit.Kilogram / Unit.Second, ParseUnit("kg / s"));
			Assert.AreEqual(Unit.Kilogram / Unit.Second, ParseUnit("kg * s^-1"));
			Assert.AreEqual(Unit.Kilogram / Unit.Second, ParseUnit("kg * s/s ^ 2"));

			Assert.AreEqual(Unit.Kilogram, ParseUnit("kg"));

			var u = ParseUnit("Ms"); // mega-second
			Assert.IsTrue(u.Dimension == Dimension.TimeDimension);
			Assert.AreEqual(1000000, new Measurement(1, u).ConvertTo("s").Value);

			Assert.AreEqual(Unit.Kilogram / Unit.Second, ParseUnit("Mg / ks"));

			Assert.AreEqual(Unit.Scalar, ParseUnit("kg^0*s/Hz^-1"));
		}

		[TestMethod]
		public void TestPrefixes()
		{
			Assert.AreEqual(100, new Measurement(1, Unit.Meter).ConvertTo(Unit.Centimeter).Value);
			Assert.AreEqual(100, new Measurement(1, Unit.Second).ConvertTo("cs").Value);
			Assert.AreEqual(3000, new Measurement(1, Unit.Yard).ConvertTo("mft").Value);
		}
	}
}
