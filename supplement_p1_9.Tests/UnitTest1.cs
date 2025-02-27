namespace supplement_p1_9.Tests;

public class UnitTest12
{
    [Fact]
    public void TestQuarterEquality()
    {
        Class1.Quarter q1 = new Class1.Quarter(0.1);
        Class1.Quarter q2 = new Class1.Quarter(0.2);
        Assert.Equal(q1, q2);
        Assert.True(q1 == q2);

    }
    [Fact]
    public void TestQuarterInequality()
    {
        // 0.2 falls in [0.0, 0.25) and 0.3 falls in [0.25, 0.5)
        Class1.Quarter q1 = new Class1.Quarter(0.2);
        Class1.Quarter q2 = new Class1.Quarter(0.3);
        Assert.NotEqual(q1, q2);
        Assert.True(q1 != q2);
        Assert.True(q1 < q2);
    }
    
    [Fact]
    public void TestQuarterComparisonOperators()
    {
        // Create quarters in successive intervals:
        // 0.1 in [0.0, 0.25), 0.3 in [0.25, 0.5), 0.6 in [0.5, 0.75), and 0.9 in [0.75, 1.0]
        Class1.Quarter q1 = new Class1.Quarter(0.1);
        Class1.Quarter q2 = new Class1.Quarter(0.3);
        Class1.Quarter q3 = new Class1.Quarter(0.6);
        Class1.Quarter q4 = new Class1.Quarter(0.9);

        Assert.True(q1 < q2);
        Assert.True(q2 < q3);
        Assert.True(q3 < q4);
        Assert.True(q4 > q1);
        Assert.True(q2 <= q2);
        Assert.True(q3 >= q3);
    }
    
    [Fact]
    public void TestQuarterInvalidValue()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Class1.Quarter(-0.1));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Class1.Quarter(1.1));
    }
    private class TestFloatingPointSequence : IEnumerable<double>
    {
        private readonly double[] _numbers;

        public TestFloatingPointSequence(double[] numbers)
        {
            _numbers = numbers;
        }

        public IEnumerator<double> GetEnumerator()
        {
            int consecutiveLowCount = 0;
            foreach (var number in _numbers)
            {
                yield return number;
                if (number <= 0.5)
                    consecutiveLowCount++;
                else
                    consecutiveLowCount = 0;

                if (consecutiveLowCount >= 3)
                    throw new Class1.InvalidSequenceException();
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }

    [Fact]
    public void TestFloatingPointSequenceExceptionThrown()
    {
      double[] numbers = { 0.7, 0.4, 0.3, 0.5, 0.8 };
    var testSequence = new TestFloatingPointSequence(numbers);
    var enumerator = testSequence.GetEnumerator();

    // First number should be returned without issue.
    Assert.True(enumerator.MoveNext());
    Assert.Equal(0.7, enumerator.Current);

    // Next two numbers (0.4 and 0.3) are returned.
    Assert.True(enumerator.MoveNext());
    Assert.Equal(0.4, enumerator.Current);
    Assert.True(enumerator.MoveNext());
    Assert.Equal(0.3, enumerator.Current);

    // The next call to MoveNext() will yield 0.5 (still does not throw yet).
    Assert.True(enumerator.MoveNext());
    Assert.Equal(0.5, enumerator.Current);

    // Now calling MoveNext() again should throw the exception.
    Assert.Throws<Class1.InvalidSequenceException>(() => enumerator.MoveNext());
  }

    [Fact]
    public void TestFloatingPointSequenceNoException()
    {
        // A sequence that alternates to avoid three consecutive numbers <= 0.5.
        double[] numbers = { 0.7, 0.4, 0.6, 0.3, 0.8 };
        var testSequence = new TestFloatingPointSequence(numbers);
        var enumerator = testSequence.GetEnumerator();

        List<double> results = new List<double>();
        while (true)
        {
            try
            {
                if (!enumerator.MoveNext())
                    break;
                results.Add(enumerator.Current);
            }
            catch (Class1.InvalidSequenceException)
            {
                Assert.True(false, "InvalidSequenceException was thrown unexpectedly.");
            }
        }
        Assert.Equal(numbers, results);
    }
}
