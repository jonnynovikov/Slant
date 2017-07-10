﻿using System;
using static LanguageExt.Prelude;

namespace LanguageExt.UnitsOfMeasure
{
    /// <summary>
    /// Numeric time-span squared value
    /// </summary>
    public struct TimeSq :
        IComparable<TimeSq>,
        IEquatable<TimeSq>
    {
        readonly double Value;

        internal TimeSq(double length)
        {
            Value = length;
        }

        public override string ToString() =>
            Value + " s²";

        public bool Equals(TimeSq other) =>
            Value.Equals(other.Value);

        public bool Equals(TimeSq other, double epsilon) =>
            Math.Abs(other.Value - Value) < epsilon;

        public override bool Equals(object obj) =>
            obj == null
                ? false
                : obj is Length
                    ? Equals((Length)obj)
                    : false;

        public override int GetHashCode() =>
            Value.GetHashCode();

        public int CompareTo(TimeSq other) =>
            Value.CompareTo(other.Value);

        public TimeSq Append(TimeSq rhs) =>
            new TimeSq(Value + rhs.Value);

        public TimeSq Subtract(TimeSq rhs) =>
            new TimeSq(Value - rhs.Value);

        public TimeSq Multiply(double rhs) =>
            new TimeSq(Value * rhs);

        public TimeSq Divide(double rhs) =>
            new TimeSq(Value / rhs);

        public static TimeSq operator *(TimeSq lhs, double rhs) =>
            lhs.Multiply(rhs);

        public static TimeSq operator *(double lhs, TimeSq rhs) =>
            rhs.Multiply(lhs);

        public static TimeSq operator /(TimeSq lhs, double rhs) =>
            lhs.Divide(rhs);

        public static TimeSq operator +(TimeSq lhs, TimeSq rhs) =>
            lhs.Append(rhs);

        public static TimeSq operator -(TimeSq lhs, TimeSq rhs) =>
            lhs.Subtract(rhs);

        public static double operator /(TimeSq lhs, TimeSq rhs) =>
            lhs.Value / rhs.Value;

        public static bool operator ==(TimeSq lhs, TimeSq rhs) =>
            lhs.Equals(rhs);

        public static bool operator !=(TimeSq lhs, TimeSq rhs) =>
            !lhs.Equals(rhs);

        public static bool operator >(TimeSq lhs, TimeSq rhs) =>
            lhs.Value > rhs.Value;

        public static bool operator <(TimeSq lhs, TimeSq rhs) =>
            lhs.Value < rhs.Value;

        public static bool operator >=(TimeSq lhs, TimeSq rhs) =>
            lhs.Value >= rhs.Value;

        public static bool operator <=(TimeSq lhs, TimeSq rhs) =>
            lhs.Value <= rhs.Value;

        public TimeSq Round() =>
            new TimeSq(Math.Round(Value));

        public Time Sqrt() =>
            new Time(Math.Sqrt(Value));

        public TimeSq Abs() =>
            new TimeSq(Math.Abs(Value));

        public TimeSq Min(TimeSq rhs) =>
            new TimeSq(Math.Min(Value, rhs.Value));

        public TimeSq Max(TimeSq rhs) =>
            new TimeSq(Math.Max(Value, rhs.Value));

        public double Seconds2 => Value;
    }
}
