using System;
using System.Collections.Generic;
using System.Linq;
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
			Assert.AreEqual(Unit.Kilogram / Unit.Second, Unit.Parse("kg/s"));
			Assert.AreEqual(Unit.Kilogram / Unit.Second, Unit.Parse("kg / s"));
			Assert.AreEqual(Unit.Kilogram / Unit.Second, Unit.Parse("kg * s^-1"));
			Assert.AreEqual(Unit.Kilogram / Unit.Second, Unit.Parse("kg * s/s ^ 2"));

			Assert.AreEqual(Unit.Kilogram, Unit.Parse("kg"));

			var u = Unit.Parse("Ms"); // mega-second
			Assert.IsTrue(u.Dimension == Dimension.TimeDimension);
			Assert.AreEqual(1000000, new Measurement(1, u).ConvertTo("s").Value);

			Assert.AreEqual(Unit.Kilogram / Unit.Second, Unit.Parse("Mg / ks"));

			Assert.AreEqual(Unit.Scalar, Unit.Parse("kg^0*s/Hz^-1"));

			Assert.IsFalse(Unit.TryParse("xs", out u));
			Assert.ThrowsException<FormatException>(() => Unit.Parse("xs"));
			Assert.ThrowsException<FormatException>(() => Unit.Parse("f^-1/Hz"));
		}

		[TestMethod]
		public void TestPrefixes()
		{
			Assert.AreEqual(100, new Measurement(1, Unit.Meter).ConvertTo(Unit.Centimeter).Value);
			Assert.AreEqual(100, new Measurement(1, Unit.Second).ConvertTo("cs").Value);
			Assert.AreEqual(3000, new Measurement(1, Unit.Yard).ConvertTo("mft").Value);
			Assert.AreEqual(0.01, new Measurement(1, Unit.Second).ConvertTo("hs").Value);
			Assert.AreEqual(1000_000_000, new Measurement(1, Unit.Second).ConvertTo("ns").Value);
			Assert.AreEqual(1000_000, new Measurement(1, Unit.Second).ConvertTo("μs").Value);
			Assert.AreEqual(0.00_000_0001, new Measurement(1, Unit.Second).ConvertTo("Gs").Value);
		}

		[TestMethod]
		public void TestUnitDimensions()
		{
			Unit.TryParse("", out _); // access anything to ensure that .cctor is called

			var unitClasses = typeof(Unit).GetNestedTypes().Where(t => t.IsClass && t.IsNestedPublic);
			foreach (var unitClass in unitClasses)
			{
				var units = unitClass.GetFields().Where(f => f.IsStatic && f.IsPublic && f.FieldType == typeof(Unit)).Select(f => (Unit)f.GetValue(null));
				var targetDimension = units.First().Dimension;
				foreach (var unit in units.Skip(1))
				{
					if (unit.Dimension != targetDimension) Assert.Fail($"{unit.Name} in class {unitClass.Name} has wrong dimension!");
				}
			}
		}
	}
}
