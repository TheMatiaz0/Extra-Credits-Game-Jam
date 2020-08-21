using Cyberultimate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberultimate
{
    /// <summary>
    /// <see cref="Double"/> decorator, acceptable is only value in 0-1 range.
    /// </summary>
    [Serializable]
    public struct Percent : IComparable<Percent>, IComparable<double>, IComparable, IEquatable<double>, IEquatable<Percent>
    {
        public const double MaxValue = 1.0;
        public const double MinValue = 0.0;
        public static readonly Percent Zero = new Percent();
        public static readonly Percent Half = new Percent(0.5f);
        public static readonly Percent Full = new Percent(1);
        private readonly double _Value;


        /// <summary>
        /// Returns value in 0-1 range.
        /// </summary
        public double Value
        {
            get => _Value;

        }
        /// <summary>
        /// Returns value in 0-100 range.
        /// </summary>
        public byte AsByte => (byte)(_Value * 100);
        /// <summary>
        /// Returns value in 0-1 range and float.
        /// </summary>
        public float AsFloat => (float)Value;

        public Percent(double @decimal)
        {

            _Value = Percent.Clamp(@decimal);
        }
        /// <summary>
        /// Returns non-abs difference.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public double Difference(Percent other)
           => this.Value - other.Value;
        public static bool operator ==(Percent a, Percent b)
            => a.Value == b.Value;
        public static bool operator !=(Percent a, Percent b)
            => !(a == b);
        public static bool operator >(Percent a, Percent b)
            => a.Value > b.Value;
        public static bool operator <(Percent a, Percent b)
            => a.Value < b.Value;
        public static bool operator <=(Percent a, Percent b)
            => a == b || a < b;
        public static bool operator >=(Percent a, Percent b)
          => a == b || a > b;
        public static explicit operator double(Percent procent)
            => procent.Value;
        public static explicit operator float(Percent procent)
            => procent.AsFloat;
        public static Percent operator +(Percent a, double b)
         => new Percent(Math.Min(1, a.Value + b));
        public static Percent operator -(Percent a, double b)
        => new Percent(Math.Max(0, a.Value - b));
        public static Percent operator *(Percent a, double b)
            => new Percent(Math.Min(1, a.Value * b));
        public static Percent operator /(Percent a, double b)
            => new Percent(Math.Min(1, a.Value / b));
        public static Percent operator +(Percent a, Percent b)
            => a + b.Value;
        public static Percent operator -(Percent a, Percent b)
            => a - b.Value;
        public static Percent operator *(Percent a, Percent b)
            => a * b.Value;
        public static Percent operator /(Percent a, Percent b)
            => a / b.Value;

        public override bool Equals(object obj)
        {
            if (obj is Percent p)
                return this == p;
            else return false;
        }
        public override string ToString()
        {
            return $"{Value * 100}%";
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        int IComparable<double>.CompareTo(double other)
        {
            return _Value.CompareTo(other);
        }

        int IComparable.CompareTo(object obj)
        {
            return _Value.CompareTo(obj);
        }

        bool IEquatable<double>.Equals(double other)
        {
            return _Value.Equals(other);
        }

        int IComparable<Percent>.CompareTo(Percent other)
        {
            return _Value.CompareTo(other._Value);
        }

        bool IEquatable<Percent>.Equals(Percent other)
        {
            return _Value.Equals(other._Value);
        }
        private static double Clamp(double val)
        {
            return Math.Max(Math.Min(val, 1f), 0f);
        }
        /// <summary>
        /// Creates a percent by a byte value in 0-100 range.
        /// </summary>
        /// <param name="val"></param>
        public static Percent FromByte(byte val)
        {
            return new Percent(val / 100.0);
        }
        /// <summary>
        /// Creates a percent by a double value in 0-1 range.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Percent FromDecimal(double val)
        {
            return new Percent(val);
        }
        /// <summary>
        /// Creates a percent by the value in given range.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static Percent FromValueInRange(double value, (double min, double max) range)
        {
            return Percent.FromDecimal(MathHelper.ReCalculateRange(range, (0, 1), value));
        }
        /// <summary>
        /// Parses percent from a text.
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static Percent Parse(string percent)
        {
            return Percent.FromDecimal(double.Parse(percent.Replace("%", "")) / 100);
        }


    }


}

