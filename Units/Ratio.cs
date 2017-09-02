using System;
using System.Collections.Generic;
using System.Text;

namespace Units
{
	public struct Ratio
	{
		public int Numerator { get; }
		public int Denominator { get; }

		public bool IsValid { get => Denominator != 0; }
		
		public Ratio(int numerator) : this(numerator, 1) { }
		public Ratio(int numerator, int denominator)
		{
			Numerator = numerator;
			Denominator = denominator;
		}

		public Ratio GetReduced()
		{
			if (!IsValid) { return new Ratio(0, 0); }
			int gcd = Util.GCD(Numerator, Denominator);
			if (Denominator < 0 && gcd > 0) { gcd *= -1; } // make sure that the denominator ends up positive
			return new Ratio(Numerator / gcd, Denominator / gcd);
		}

		public static Ratio operator *(Ratio lhs, Ratio rhs)
		{
			if (!lhs.IsValid || !rhs.IsValid) { return new Ratio(0, 0); }
			return new Ratio(lhs.Numerator * rhs.Numerator, lhs.Denominator * rhs.Denominator);
		}

		public static Ratio operator /(Ratio lhs, Ratio rhs)
		{
			if (!lhs.IsValid || !rhs.IsValid) { return new Ratio(0, 0); }
			return new Ratio(lhs.Numerator * rhs.Denominator, lhs.Denominator * rhs.Numerator);
		}

		public static Ratio operator +(Ratio lhs, Ratio rhs)
		{
			if (!lhs.IsValid || !rhs.IsValid) { return new Ratio(0, 0); }
			int gcd = Util.GCD(lhs.Denominator, rhs.Denominator);
			int lhsFactor = rhs.Denominator / gcd;
			int rhsFactor = lhs.Denominator / gcd;
			return new Ratio(lhs.Numerator * lhsFactor + rhs.Numerator * rhsFactor, lhs.Denominator * lhsFactor);
		}

		public static Ratio operator -(Ratio lhs, Ratio rhs)
		{
			if (!lhs.IsValid || !rhs.IsValid) { return new Ratio(0, 0); }
			int gcd = Util.GCD(lhs.Denominator, rhs.Denominator);
			int lhsFactor = rhs.Denominator / gcd;
			int rhsFactor = lhs.Denominator / gcd;
			return new Ratio(lhs.Numerator * lhsFactor - rhs.Numerator * rhsFactor, lhs.Denominator * lhsFactor);
		}

		public static Ratio operator -(Ratio rhs)
		{
			if (!rhs.IsValid) { return new Ratio(0, 0); }
			return new Ratio(-rhs.Numerator, rhs.Denominator);
		}

		public static implicit operator double(Ratio ratio)
		{
			if (!ratio.IsValid) { return double.NaN; }
			return (double)ratio.Numerator / ratio.Denominator;
		}

		public static bool operator ==(Ratio lhs, Ratio rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Ratio lhs, Ratio rhs)
		{
			return !lhs.Equals(rhs);
		}

		public override bool Equals(object obj)
		{
			if (obj is Ratio rat) { return Equals(rat); }
			return base.Equals(obj);
		}

		public bool Equals(Ratio other)
		{
			return other.Numerator * Denominator == Numerator * other.Denominator;
		}

		public override int GetHashCode()
		{
			return ((double)this).GetHashCode();
		}

		public override string ToString()
		{
			if (!IsValid) { return "NaN"; }
			else if (Denominator == 1) { return $"{Numerator}"; }
			else { return $"{Numerator}/{Denominator}"; }
		}
	}
}
