using NUnit.Framework;

namespace tdd_with_csharp;

public class CalculatorTests
{
    private Caculator _caculator = new Caculator();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void first_1_second_2_result_should_3()
    {
        ResultShoud(3, 1, 2);
    }

    private void ResultShoud(int expected, int first, int second)
    {
        Assert.AreEqual(expected, _caculator.Sum(first, second));
    }
}