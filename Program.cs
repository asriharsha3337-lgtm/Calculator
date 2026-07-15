using ChangeCalculator.Services;

Console.WriteLine("=== UK Change Calculator ===");
Console.WriteLine();

Console.Write("Amount Given (£): ");
string? amountInput = Console.ReadLine();

Console.Write("Product Price (£): ");
string? priceInput = Console.ReadLine();

if (!decimal.TryParse(amountInput, out decimal amountGiven))
{
    Console.WriteLine("Invalid amount.");
    return;
}

if (!decimal.TryParse(priceInput, out decimal productPrice))
{
    Console.WriteLine("Invalid product price.");
    return;
}

if (productPrice > amountGiven)
{
    Console.WriteLine("Amount given must be greater than or equal to the product price.");
    return;
}

var calculator = new ChangeCalculatorService();

var result = calculator.CalculateChange(amountGiven, productPrice);

Console.WriteLine();
Console.WriteLine("Your change is:");

foreach (var item in result)
{
    Console.WriteLine($"{item.Count} x {item.Denomination.DisplayName}");
}