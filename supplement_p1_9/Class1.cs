using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace supplement_p1_9;

public class Class1
{
    public class InvalidSequenceException : Exception
    {
        public InvalidSequenceException()
        : base("Invalid sequence generated: three consecutive numbers are equal to or below 0.5"){}

        public InvalidSequenceException(string message) : base(message) {}   
    }

    public class FloatingPointSequence : IEnumerable<double>
    {
        private Random _random = new Random();

        public IEnumerator<double> GetEnumerator()
        {
            int consecutiveLowCount = 0;

            while (true)
            {
                double number = _random.NextDouble();
                yield return number;

                if (number <= 0.5)
                {
                    consecutiveLowCount++;
                }
                else{
                    consecutiveLowCount = 0;
                }

                if (consecutiveLowCount >= 3)
                {
                    throw new InvalidSequenceException();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Quarter : IEquatable<Quarter>, IComparable<Quarter>
    {
        public double Value { get; private set; }

        public Quarter(Double value)
        {
            if (value < 0.0 || value > 1.0)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0.0 and 1.0");
            Value = value;
        }
        private int GetQuarterIndex()
        {
            if (Value < 0.25) return 0;
            else if (Value < 0.5) return 1;
            else if (Value < 0.75) return 2;
            else return 3;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Quarter);
        }

        public bool Equals(Quarter other)
        {
            if (other is null)
            return false;
            return this.GetQuarterIndex() == other.GetQuarterIndex();
        }

        public override int GetHashCode()
        {
            return GetQuarterIndex().GetHashCode();
        }

        public static bool operator ==(Quarter a, Quarter b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (a is null || b is null)
                return false;
            return a.GetQuarterIndex() == b.GetQuarterIndex();
        }

        public static bool operator !=(Quarter a, Quarter b)
        {
            return !(a == b);
        }

        public static bool operator <(Quarter a, Quarter b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException();
            return a.GetQuarterIndex() < b.GetQuarterIndex();
        }

        public static bool operator >(Quarter a, Quarter b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException();
            return a.GetQuarterIndex() > b.GetQuarterIndex();
        }

        public static bool operator <=(Quarter a, Quarter b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException();
            return a.GetQuarterIndex() <= b.GetQuarterIndex();
        }

        public static bool operator >=(Quarter a, Quarter b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException();
            return a.GetQuarterIndex() >= b.GetQuarterIndex();
        }

        public int CompareTo(Quarter other)
        {
            if (other is null)
                return 1;
            return this.GetQuarterIndex().CompareTo(other.GetQuarterIndex());
        }

        public override string ToString()
        {
            return $"Quarter(Value: {Value})";
        }
    }

}
