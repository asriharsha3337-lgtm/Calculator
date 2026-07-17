using ChangeCalculator.Models;

namespace ChangeCalculator.Services;

public interface IConsole
{
    void WriteLine(string? value = null);
    void Write(string value);
    string? ReadLine();
}

public class TextConsole : IConsole
{
    public void WriteLine(string? value = null) => Console.WriteLine(value);
    public void Write(string value) => Console.Write(value);
    public string? ReadLine() => Console.ReadLine();
}

public class ChangeCalculatorApp
{
    private readonly IChangeCalculatorService _calculatorService;
    private readonly IConsole _console;

    public ChangeCalculatorApp(IChangeCalculatorService calculatorService, IConsole console)
    {
        _calculatorService = calculatorService;
        _console = console;
    }

    public int Run()
    {
        _console.WriteLine("=== UK Change Calculator ===");
        _console.WriteLine();

        _console.Write("Amount Given (£): ");
        string? amountInput = _console.ReadLine();
        _console.Write("Product Price (£): ");
        string? priceInput = _console.ReadLine();

        if (!decimal.TryParse(amountInput, out decimal amountGiven))
        {
            _console.WriteLine("Invalid amount.");
            return 1;
        }

        if (!decimal.TryParse(priceInput, out decimal productPrice))
        {
            _console.WriteLine("Invalid product price.");
            return 1;
        }

        if (productPrice > amountGiven)
        {
            _console.WriteLine("Amount given must be greater than or equal to the product price.");
            return 1;
        }

        var result = _calculatorService.CalculateChange(amountGiven, productPrice);

        _console.WriteLine();
        _console.WriteLine("Your change is:");

        foreach (var item in result)
        {
            _console.WriteLine($"{item.Count} x {item.Denomination.DisplayName}");
        }

        return 0;
    }
}
