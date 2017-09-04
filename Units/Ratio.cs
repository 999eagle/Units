using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Units
{
	public struct Ratio
	{
		public BigInteger Numerator { get; }
		public BigInteger Denominator { get; }

		public bool IsValid { get => Denominator != 0; }
		
		public Ratio(BigInteger numerator) : this(numerator, 1) { }
		public Ratio(BigInteger numerator, BigInteger denominator)
		{
			Numerator = numerator;
			Denominator = denominator;
		}

		public Ratio GetReduced()
		{
			if (!IsValid) { return new Ratio(0, 0); }
			var gcd = BigInteger.GreatestCommonDivisor(Numerator, Denominator);
			if (Denominator < 0 && gcd > 0) { gcd *= -1; } // make sure that the denominator ends up positive
			return new Ratio(Numerator / gcd, Denominator / gcd);
		}

		public Ratio GetInverse()
		{
			if (Numerator == 0) { return new Ratio(0, 0); }
			return new Ratio(Denominator, Numerator);
		}

		public static Ratio operator *(Ratio lhs, Ratio rhs)
		{
			if (!lhs.IsValid || !rhs.IsValid) { return new Ratio(0, 0); }
			return new Ratio(lhs.Numerator * rhs.Numerator, lhs.Denominator * rhs.Denominator);
		}

		public static Ratio operator /(Ratio lhs, Ratio rhs)
		{
			if (!lhs.IsValid || !rhs.IsValid) { return new Ratio(0, 0); }
			var num = lhs.Numerator * rhs.Denominator;
			var den = lhs.Denominator * rhs.Numerator;
			if (den.Sign < 0) { den = -den; num = -num; }
			return new Ratio(num, den);
		}

		public static Ratio operator +(Ratio lhs, Ratio rhs)
		{
			if (!lhs.IsValid || !rhs.IsValid) { return new Ratio(0, 0); }
			var gcd = BigInteger.GreatestCommonDivisor(lhs.Denominator, rhs.Denominator);
			var lhsFactor = rhs.Denominator / gcd;
			var rhsFactor = lhs.Denominator / gcd;
			return new Ratio(lhs.Numerator * lhsFactor + rhs.Numerator * rhsFactor, lhs.Denominator * lhsFactor);
		}

		public static Ratio operator -(Ratio lhs, Ratio rhs)
		{
			if (!lhs.IsValid || !rhs.IsValid) { return new Ratio(0, 0); }
			var gcd = BigInteger.GreatestCommonDivisor(lhs.Denominator, rhs.Denominator);
			var lhsFactor = rhs.Denominator / gcd;
			var rhsFactor = lhs.Denominator / gcd;
			return new Ratio(lhs.Numerator * lhsFactor - rhs.Numerator * rhsFactor, lhs.Denominator * lhsFactor);
		}

		public static Ratio operator -(Ratio rhs)
		{
			if (!rhs.IsValid) { return new Ratio(0, 0); }
			return new Ratio(-rhs.Numerator, rhs.Denominator);
		}

		public static explicit operator double(Ratio ratio)
		{
			if (!ratio.IsValid) { return double.NaN; }

			var value = (double)BigInteger.DivRem(ratio.Numerator, ratio.Denominator, out var fractionalNumerator);
			if (fractionalNumerator == 0) { return value; }
			var precisionLeft = 16 - value.ToString().Length;
			if (precisionLeft <= 0) { return value; }

			var power = Math.Pow(10, precisionLeft);
			var adjustedNumerator = fractionalNumerator * (BigInteger)power / ratio.Denominator;
			return value + ((double)adjustedNumerator / power);
		}

		public static explicit operator Ratio(double value)
		{
			if (value == 0) return new Ratio(0, 1);
			if (double.IsNaN(value)) return new Ratio(0, 0);

			return GetNearestRatio(value, 1e-11);
		}

		public static implicit operator Ratio(int value)
		{
			return new Ratio(value);
		}

		public static implicit operator Ratio(long value)
		{
			return new Ratio(value);
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
			if (!IsValid && !other.IsValid) return true;
			if (!IsValid || !other.IsValid) return false;
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

		public string ToDecimalString(int precision)
		{
			if (!IsValid) { return "NaN"; }
			if (precision <= 0) { throw new ArgumentOutOfRangeException($"{nameof(precision)} must be larger than 0."); }

			var wholePart = BigInteger.DivRem(Numerator, Denominator, out var fractionalNumerator);
			if (fractionalNumerator == 0) { return wholePart.ToString() + ".0"; }

			var adjustedNumerator = fractionalNumerator * BigInteger.Pow(10, precision);
			var decimalPlaces = adjustedNumerator / Denominator;
			if (decimalPlaces == 0) return "0.0";

			var sb = new StringBuilder(wholePart.ToString());
			sb.EnsureCapacity(sb.Length + precision + 1);
			sb.Append('.');
			sb.Append(decimalPlaces.ToString($"D{precision}"));

			return sb.ToString();
		}

		public double ToDouble()
		{
			return (double)this;
		}

		public static Ratio GetNearestRatio(double target, double precision)
		{
			var nearestRatio = new Ratio(0);
			int steps = 0;
			while (Math.Abs(target - (double)nearestRatio) > precision)
			{
				if (steps > 20) { break; }
				nearestRatio = cfracStep(target, steps++);
			}
			return nearestRatio;

			Ratio cfracStep(double number, int maxSteps)
			{
				var integerPart = checked((long)number);
				var fracPart = number - integerPart;
				if (maxSteps > 0 && fracPart != 0)
				{
					return new Ratio(integerPart) + cfracStep(1 / fracPart, maxSteps - 1).GetInverse();
				}
				else return new Ratio(integerPart);
			}
		}
	}
}
