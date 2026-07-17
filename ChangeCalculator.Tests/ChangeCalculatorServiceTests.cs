using System.Linq;
using ChangeCalculator.Services;
using Xunit;

namespace ChangeCalculator.Tests;

public class ChangeCalculatorServiceTests
{
    private readonly ChangeCalculatorService _service = new();

    [Fact]
    public void CalculateChange_WithExactAmount_ReturnsNoChange()
    {
        var result = _service.CalculateChange(5.50m, 5.50m);

        Assert.Empty(result);
    }

    [Fact]
    public void CalculateChange_WithTwentyAndFiveFifty_ReturnsCorrectDenominations()
    {
        var result = _service.CalculateChange(20.00m, 5.50m);

        Assert.Collection(result,
            item =>
            {
                Assert.Equal(1000, item.Denomination.ValueInPence);
                Assert.Equal("£10", item.Denomination.DisplayName);
                Assert.Equal(1, item.Count);
            },
            item =>
            {
                Assert.Equal(200, item.Denomination.ValueInPence);
                Assert.Equal("£2", item.Denomination.DisplayName);
                Assert.Equal(2, item.Count);
            },
            item =>
            {
                Assert.Equal(50, item.Denomination.ValueInPence);
                Assert.Equal("50p", item.Denomination.DisplayName);
                Assert.Equal(1, item.Count);
            });

        Assert.Equal(4, result.Sum(item => item.Count));
    }

    [Fact]
    public void CalculateChange_NegativeAmountGiven_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _service.CalculateChange(-1m, 0m));
    }

    [Fact]
    public void CalculateChange_ProductPriceGreaterThanAmountGiven_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _service.CalculateChange(5.00m, 6.00m));
    }
}
