using ChangeCalculator.Models;

namespace ChangeCalculator.Services;

public sealed class ChangeCalculatorApp
{
    private readonly IChangeCalculatorService _calculatorService;
    private readonly IChangeCalculatorValidator _validator;

    public ChangeCalculatorApp(IChangeCalculatorService calculatorService, IChangeCalculatorValidator validator)
    {
        _calculatorService = calculatorService ?? throw new ArgumentNullException(nameof(calculatorService));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public int Run()
    {
        Console.WriteLine("=== UK Change Calculator ===");
        Console.WriteLine();

        Console.Write("Amount Given (£): ");
        string? amountInput = Console.ReadLine();

        Console.Write("Product Price (£): ");
        string? priceInput = Console.ReadLine();

        if (!_validator.TryParseAmounts(amountInput, priceInput, out decimal amountGiven, out decimal productPrice, out string? errorMessage))
        {
            Console.WriteLine(errorMessage);
            return 1;
        }

        IReadOnlyList<ChangeItem> changeItems = _calculatorService.CalculateChange(amountGiven, productPrice);

        Console.WriteLine();
        Console.WriteLine("Your change is:");

        if (changeItems.Count == 0)
        {
            Console.WriteLine("No change required.");
            return 0;
        }

        foreach (var item in changeItems)
        {
            Console.WriteLine($"{item.Count} x {item.Denomination.DisplayName}");
        }

        return 0;
    }
}
