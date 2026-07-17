using ChangeCalculator.Services;
using Xunit;

namespace ChangeCalculator.Tests;

public class ChangeCalculatorAppTests
{
    [Fact]
    public void Run_WithValidInput_PrintsChangeReport()
    {
        var console = new TestConsole(new[] { "20", "5.50" });
        var app = new ChangeCalculatorApp(new ChangeCalculatorService(), console);

        int exitCode = app.Run();

        Assert.Equal(0, exitCode);
        Assert.Contains("Your change is:", console.Output);
        Assert.Contains("1 x £10", console.Output);
        Assert.Contains("2 x £2", console.Output);
        Assert.Contains("1 x 50p", console.Output);
    }

    [Fact]
    public void Run_WithInvalidAmount_PrintsErrorAndReturnsNonZero()
    {
        var console = new TestConsole(new[] { "abc", "5.50" });
        var app = new ChangeCalculatorApp(new ChangeCalculatorService(), console);

        int exitCode = app.Run();

        Assert.Equal(1, exitCode);
        Assert.Contains("Invalid amount.", console.Output);
    }

    private class TestConsole : IConsole
    {
        private readonly Queue<string?> _inputs;
        private readonly List<string> _output = new();

        public TestConsole(IEnumerable<string?> inputs)
        {
            _inputs = new Queue<string?>(inputs);
        }

        public string? Output => string.Join("\n", _output);

        public void WriteLine(string? value = null)
        {
            _output.Add(value ?? string.Empty);
        }

        public void Write(string value)
        {
            _output.Add(value);
        }

        public string? ReadLine()
        {
            return _inputs.Count > 0 ? _inputs.Dequeue() : null;
        }
    }
}
