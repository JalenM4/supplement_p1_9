using System;
using System.Collections;
using System.Collections.Generic;

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
        public double Value 
    }

}
