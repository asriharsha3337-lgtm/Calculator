namespace ChangeCalculator.Services;

public interface IChangeCalculatorValidator
{
    bool TryParseAmounts(string? amountInput, string? priceInput, out decimal amountGiven, out decimal productPrice, out string? errorMessage);
}

public sealed class ChangeCalculatorValidator : IChangeCalculatorValidator
{
    public bool TryParseAmounts(string? amountInput, string? priceInput, out decimal amountGiven, out decimal productPrice, out string? errorMessage)
    {
        amountGiven = 0m;
        productPrice = 0m;
        errorMessage = null;

        if (string.IsNullOrWhiteSpace(amountInput) || !decimal.TryParse(amountInput, out amountGiven))
        {
            errorMessage = "Invalid amount.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(priceInput) || !decimal.TryParse(priceInput, out productPrice))
        {
            errorMessage = "Invalid product price.";
            return false;
        }

        if (productPrice < 0m)
        {
            errorMessage = "Product price cannot be negative.";
            return false;
        }

        if (amountGiven < 0m)
        {
            errorMessage = "Amount given cannot be negative.";
            return false;
        }

        if (productPrice > amountGiven)
        {
            errorMessage = "Amount given must be greater than or equal to the product price.";
            return false;
        }

        return true;
    }
}
