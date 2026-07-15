namespace ChangeCalculator.Models;

public class Denomination
{
    public int ValueInPence { get; set; }

    public string DisplayName { get; set; } = "";
}

public class ChangeItem
{
    public Denomination Denomination { get; set; } = null!;

    public int Count { get; set; }
}