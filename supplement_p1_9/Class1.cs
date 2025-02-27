using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace supplement_p1_9;

public class Class1
{
    /// <summary>
    /// Exception thrown when an invalid sequence is genrated,
    /// is thrown when three consecutive numbers generated are 
    /// equal to or below 0.5
    /// </summary>
    public class InvalidSequenceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of InvalidSequenceException class
        /// with a default error message
        /// </summary>
        public InvalidSequenceException()
        : base("Invalid sequence generated: three consecutive numbers are equal to or below 0.5"){}

        /// <summary>
        /// Initializes a new instance of the InvalidaSequenceException class
        /// with a specified error message
        /// </summary>
        /// <param name="message"> The error message that explains the reason for the explanation</param>
        public InvalidSequenceException(string message) : base(message) {}   
    }

    /// <summary>
    /// An An enumerable sequence of random floating point numbers between 0.0 and 1.0.
    /// If three consecutive numbers generated are less than or equal to 0.5, 
    /// InvalidSequenceException is thrown
    /// </summary>
    public class FloatingPointSequence : IEnumerable<double>
    {
        private Random _random = new Random();

        /// <summary>
        /// Returns an enumerator that iterates through the sequence
        /// </summary>
        /// <returns> An enumerator for the floating point sequence </returns>
        /// <exception cref="InvalidSequenceException"> Thrown when tthree
        //  consecutive numbers generated are less than or equal to 0.5 </exception>
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

        /// <summary>
        /// Returns an enumerator that iterates throough the sequence
        /// </summary>
        /// <returns> An enumerator for the floating point sequence </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Represents a quarter of the unit interval [0.0, 1.0].
    /// Quarters are defined as:
    ///   0: [0.0, 0.25)
    ///   1: [0.25, 0.5)
    ///   2: [0.5, 0.75)
    ///   3: [0.75, 1.0]
    /// Two quarters are considered equal if they fall within the same interval.
    /// </summary>
    public class Quarter : IEquatable<Quarter>, IComparable<Quarter>
    {
        /// <summary>
        /// Gets the underlying floating point value
        /// </summary>
        public double Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the "Quarter" class
        /// </summary>
        /// <param name="value">A floating point value bewtween 0.0 and 1.0 inclusive</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is not bewtween 0.0 and 1.0</exception>
        public Quarter(Double value)
        {
            if (value < 0.0 || value > 1.0)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0.0 and 1.0");
            Value = value;
        }
        /// <summary>
        /// Determines the quarter index based on the value
        /// </summary>
        /// <returns> An integer reprsenting the quarters 0: 
        /// 0: [0.0, 0.25)
        /// 1: [0.25, 0.5)
        /// 2: [0.5, 0.75)
        /// 3: [0.75, 1.0]
        // </returns>
        private int GetQuarterIndex()
        {
            if (Value < 0.25) return 0;
            else if (Value < 0.5) return 1;
            else if (Value < 0.75) return 2;
            else return 3;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current
        /// based on the quarter interval
        /// </summary>
        /// <param name="obj"> The object to compare with the current quarter</param>
        /// <returns>true if the specified object is a "Quarter" and falls in the same quarter interval; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Quarter);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current 
        /// quarter, based on the quarter interval
        /// </summary>
        /// <param name="other">the quarter to compare with the current quarter</param>
        /// <returns> true if specified object is a "quarter" and falls in the same
        // quarter interval; otherwise, false. </returns>
        public bool Equals(Quarter other)
        {
            if (other is null)
            return false;
            return this.GetQuarterIndex() == other.GetQuarterIndex();
        }

        /// <summary>
        /// Returns a hash code for the quarter based on its quarter interval
        /// </summary>
        /// <returns>A hash code for the quarter</returns>
        public override int GetHashCode()
        {
            return GetQuarterIndex().GetHashCode();
        }

        /// <summary>
        /// Equality operator that checks if two quarters fall in the same interval.
        /// </summary>
        /// <param name="a">The first quarter.</param>
        /// <param name="b">The second quarter</param>
        /// <returns>true if both quarters are in the same interval; otherwise, false.</returns>
        public static bool operator ==(Quarter a, Quarter b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (a is null || b is null)
                return false;
            return a.GetQuarterIndex() == b.GetQuarterIndex();
        }

        /// <summary>
        /// Inequality operator that checks if two quarters fall in different intervals.
        /// </summary>
        /// <param name="a">The first quarter.</param>
        /// <param name="b">The second quarter.</param>
        /// <returns>true if the quarters are in different intervals; otherwise, false.</returns>
        public static bool operator !=(Quarter a, Quarter b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Less than operator comparing the quarter intervals of two quarters
        /// </summary>
        /// <param name="a">The first quarter.</param>
        /// <param name="b">The second quarter.</param>
        /// <returns>true if the first quarter's interval is less than the second's; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either quarter is null.</exception>
        public static bool operator <(Quarter a, Quarter b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException();
            return a.GetQuarterIndex() < b.GetQuarterIndex();
        }

        /// <summary>
        /// Greater than operator comparing the quarter intervals of two quarters.
        /// </summary>
        /// <param name="a">The first quarter.</param>
        /// <param name="b">The second quarter</param>
        /// <returns>true if the first quarter's interval is greater than the second's; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either quarter is null.</exception>
        public static bool operator >(Quarter a, Quarter b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException();
            return a.GetQuarterIndex() > b.GetQuarterIndex();
        }

        /// <summary>
        /// Less than or equal operator comparing the quarter intervals of two quarters.
        /// </summary>
        /// <param name="a">The first quarter.</param>
        /// <param name="b">The second quarter.</param>
        /// <returns>true if the first quarter's interval is less than or equal to the second's; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either quarter is null.</exception>
        public static bool operator <=(Quarter a, Quarter b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException();
            return a.GetQuarterIndex() <= b.GetQuarterIndex();
        }

        /// <summary>
        /// Greater than or equal operator comparing the quarter intervals of two quarters.
        /// </summary>
        /// <param name="a">The first quarter</param>
        /// <param name="b">The second quarter.</param>
        /// <returns>true if the first quarter's interval is greater than or equal to the second's; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either quarter is null.</exception>
        public static bool operator >=(Quarter a, Quarter b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException();
            return a.GetQuarterIndex() >= b.GetQuarterIndex();
        }

        /// <summary>
        /// Compares the current quarter with another quarter
        /// </summary>
        /// <param name="other">A quarter to compare with this quarter.</param>
        /// <returns>A value less than zero if this quarter is less than <paramref name="other"/>;
        /// zero if this quarter is equal to <paramref name="other"/>;
        /// and a value greater than zero if this quarter is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(Quarter other)
        {
            if (other is null)
                return 1;
            return this.GetQuarterIndex().CompareTo(other.GetQuarterIndex());
        }

        /// <summary>
        /// Return a string representation of the quarter
        /// </summary>
        /// <returns> A string that represents the quarter</returns>
        public override string ToString()
        {
            return $"Quarter(Value: {Value})";
        }
    }

}
