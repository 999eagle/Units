using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Units;

namespace UnitsTests
{
	[TestClass]
	public class MeasurementTests
	{
		[TestMethod]
		public void TestAddition()
		{
			var cm = new Measurement(50, Unit.Centimeter);
			var m = new Measurement(1, Unit.Meter);
			var i = new Measurement(6, Unit.Inch);
			var f = new Measurement(1, Unit.Foot);
			var s = new Measurement(1, Unit.Second);

			Assert.IsTrue((cm + m).Unit == Unit.Centimeter);
			Assert.IsTrue((cm + m).Value == 150);

			Assert.IsTrue((m + cm).Unit == Unit.Meter);
			Assert.IsTrue((m + cm).Value == 1.5);

			Assert.IsTrue((i + f).Unit == Unit.Inch);
			Assert.IsTrue((i + f).Value == 18);

			Assert.IsTrue((f + i).Unit == Unit.Foot);
			Assert.IsTrue((f + i).Value == 1.5);

			Assert.ThrowsException<InvalidOperationException>(() => s + m);
		}

		[TestMethod]
		public void TestSubtraction()
		{
			var cm = new Measurement(50, Unit.Centimeter);
			var m = new Measurement(1, Unit.Meter);
			var s = new Measurement(1, Unit.Second);

			Assert.IsTrue((cm - m).Unit == Unit.Centimeter);
			Assert.IsTrue((cm - m).Value == -50);

			Assert.IsTrue((m - cm).Unit == Unit.Meter);
			Assert.IsTrue((m - cm).Value == 0.5);

			Assert.ThrowsException<InvalidOperationException>(() => s - m);
		}
	}
}
