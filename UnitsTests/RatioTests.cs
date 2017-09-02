using Microsoft.VisualStudio.TestTools.UnitTesting;

using Units;

namespace UnitsTests
{
	[TestClass]
	public class RatioTests
	{
		[TestMethod]
		public void TestConversion()
		{
			Assert.AreEqual(double.NaN, new Ratio());
			Assert.AreEqual(double.NaN, new Ratio(0, 0));
			Assert.AreEqual(double.NaN, new Ratio(1, 0));
			Assert.AreEqual(0.0, new Ratio(0));
			Assert.AreEqual(0.5, new Ratio(1, 2));
			Assert.AreEqual(2.0, new Ratio(2, 1));
		}

		[TestMethod]
		public void TestAddition()
		{
			var a = new Ratio();
			var b = new Ratio(1);
			var c = new Ratio(1, 2);
			var d = new Ratio(-6);
			var e = new Ratio(-4, -2);

			Assert.AreEqual(double.NaN, a + b);
			Assert.AreEqual(1.5, b + c);
			Assert.AreEqual(-5.0, b + d);
			Assert.AreEqual(2.5, c + e);
			Assert.AreEqual(-4.0, d + e);
			Assert.AreEqual(-5.5, c + d);
		}

		[TestMethod]
		public void TestSubtraction()
		{
			var a = new Ratio();
			var b = new Ratio(1);
			var c = new Ratio(1, 2);
			var d = new Ratio(-6);
			var e = new Ratio(-4, -2);

			Assert.AreEqual(double.NaN, a - b);
			Assert.AreEqual(0.5, b - c);
			Assert.AreEqual(7.0, b - d);
			Assert.AreEqual(-1.5, c - e);
			Assert.AreEqual(8.0, e - d);
			Assert.AreEqual(6.5, c - d);
		}

		[TestMethod]
		public void TestMultiplication()
		{
			var a = new Ratio();
			var b = new Ratio(1);
			var c = new Ratio(1, 2);
			var d = new Ratio(-6);
			var e = new Ratio(-4, -2);

			Assert.AreEqual(double.NaN, a * b);
			Assert.AreEqual(0.5, b * c);
			Assert.AreEqual(-6.0, b * d);
			Assert.AreEqual(1.0, c * e);
			Assert.AreEqual(-12.0, e * d);
			Assert.AreEqual(-3.0, c * d);
		}

		[TestMethod]
		public void TestDivision()
		{
			var a = new Ratio();
			var b = new Ratio(1);
			var c = new Ratio(1, 2);
			var d = new Ratio(-6);
			var e = new Ratio(-4, -2);
			var f = new Ratio(0);

			Assert.AreEqual(double.NaN, a / b);
			Assert.AreEqual(double.NaN, b / f);
			Assert.AreEqual(2.0, b / c);
			Assert.AreEqual(-(1.0/6), b / d);
			Assert.AreEqual(0.25, c / e);
			Assert.AreEqual(-(1.0/3), e / d);
			Assert.AreEqual(-(1.0/12), c / d);
		}

		[TestMethod]
		public void TestReduce()
		{
			var a = new Ratio();
			var b = new Ratio(1);
			var c = new Ratio(1, 2);
			var d = new Ratio(-6);
			var e = new Ratio(-2, -4);
			var f = new Ratio(2, -4);

			Assert.AreEqual(a, a.GetReduced());
			Assert.AreEqual(b, b.GetReduced());
			Assert.AreEqual(c, c.GetReduced());
			Assert.AreEqual(d, d.GetReduced());
			Assert.AreEqual(c, e.GetReduced());
			Assert.AreEqual(-c, f.GetReduced());
		}

		[TestMethod]
		public void TestEquality()
		{
			var a = new Ratio();
			var b = new Ratio(1);
			var c = new Ratio(1, 2);
			var d = new Ratio(-6);
			var e = new Ratio(-2, -4);
			var f = new Ratio(2, -4);

			Assert.IsFalse(a == b);
			Assert.IsFalse(b == c);
			Assert.IsTrue(b != c);
			Assert.IsTrue(a == a);
			Assert.IsTrue(c == e);
			Assert.IsFalse(f == e);
			Assert.IsTrue(d == -6);
			Assert.IsFalse(d.Equals(-6));
		}
	}
}
