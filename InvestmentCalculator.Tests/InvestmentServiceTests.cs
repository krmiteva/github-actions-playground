using InvestmentCalculator.Services;

namespace InvestmentCalculator.Tests;

[TestFixture]
public class InvestmentServiceTests
{
    private InvestmentService _service = null!;

    [SetUp]
    public void SetUp() => _service = new InvestmentService();

    [Test]
    public void Calculate_NoContributions_GrowsWithCompoundInterest()
    {
        decimal result = _service.Calculate(1000m, 1, 0m, 10m);

        Assert.That(result, Is.EqualTo(1100.00m));
    }

    [Test]
    public void Calculate_ZeroReturnRate_OnlyAddsContributions()
    {
        // 12 months * 100 = 1200 contributions + 1000 initial
        decimal result = _service.Calculate(1000m, 1, 100m, 0m);

        Assert.That(result, Is.EqualTo(2200.00m));
    }

    [Test]
    public void Calculate_ZeroInitialAndContributions_ReturnsZero()
    {
        decimal result = _service.Calculate(0m, 10, 0m, 7m);

        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public void Calculate_WithContributionsAndReturn_ReturnsCorrectAmount()
    {
        // Known value: $1000 initial, 5% annual rate, $100/month, 10 years
        decimal result = _service.Calculate(1000m, 10, 100m, 5m);

        Assert.That(result, Is.EqualTo(17157.12m));
    }

    [Test]
    public void Calculate_LongTimeHorizon_ReturnsCorrectAmount()
    {
        decimal result = _service.Calculate(10000m, 30, 500m, 7m);

        Assert.That(result, Is.EqualTo(686108.05m));
    }

    [Test]
    public void Calculate_RoundsToTwoDecimalPlaces()
    {
        decimal result = _service.Calculate(1234.56m, 3, 78.90m, 4.5m);

        string resultStr = result.ToString();
        int dotIndex = resultStr.IndexOf('.');
        if (dotIndex >= 0)
            Assert.That(resultStr.Length - dotIndex - 1, Is.LessThanOrEqualTo(2));
    }
}
