using ChangeCalculator.Models;

namespace ChangeCalculator.Services;

public class ChangeCalculatorService
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

    public List<ChangeItem> CalculateChange(decimal amountGiven, decimal productPrice)
    {
        int remaining = (int)Math.Round((amountGiven - productPrice) * 100);

        List<ChangeItem> result = new();

        foreach (var denomination in _denominations)
        {
            int count = remaining / denomination.ValueInPence;

            if (count > 0)
            {
                result.Add(new ChangeItem
                {
                    Denomination = denomination,
                    Count = count
                });

                remaining %= denomination.ValueInPence;
            }
        }

        return result;
    }
}