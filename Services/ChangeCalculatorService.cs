using ChangeCalculator.Models;

namespace ChangeCalculator.Services;

public interface IChangeCalculatorService
{
    IReadOnlyList<ChangeItem> CalculateChange(decimal amountGiven, decimal productPrice);
}

public sealed class ChangeCalculatorService : IChangeCalculatorService
{
    private readonly List<Denomination> _denominations = new()
    {
        new() { ValueInPence = 5000, DisplayName = "£50" },
        new() { ValueInPence = 2000, DisplayName = "£20" },
        new() { ValueInPence = 1000, DisplayName = "£10" },
        new() { ValueInPence = 500, DisplayName = "£5" },
        new() { ValueInPence = 200, DisplayName = "£2" },
        new() { ValueInPence = 100, DisplayName = "£1" },
        new() { ValueInPence = 50, DisplayName = "50p" },
        new() { ValueInPence = 20, DisplayName = "20p" },
        new() { ValueInPence = 10, DisplayName = "10p" },
        new() { ValueInPence = 5, DisplayName = "5p" },
        new() { ValueInPence = 2, DisplayName = "2p" },
        new() { ValueInPence = 1, DisplayName = "1p" }
    };

    public IReadOnlyList<ChangeItem> CalculateChange(decimal amountGiven, decimal productPrice)
    {
        if (amountGiven < 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(amountGiven), "Amount given cannot be negative.");
        }

        if (productPrice < 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(productPrice), "Product price cannot be negative.");
        }

        if (productPrice > amountGiven)
        {
            throw new ArgumentException("Product price cannot exceed the amount given.", nameof(productPrice));
        }

        int remaining = (int)Math.Round((amountGiven - productPrice) * 100m, MidpointRounding.AwayFromZero);

        var result = new List<ChangeItem>();

        foreach (var denomination in _denominations)
        {
            int count = remaining / denomination.ValueInPence;
            if (count <= 0)
            {
                continue;
            }

            result.Add(new ChangeItem
            {
                Denomination = denomination,
                Count = count
            });

            remaining %= denomination.ValueInPence;
        }

        return result;
    }
}