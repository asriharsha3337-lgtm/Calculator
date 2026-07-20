using ChangeCalculator.Services;
using System;
using System.IO;
using Xunit;

namespace ChangeCalculator.Tests;

public class ChangeCalculatorAppTests
{
    private static readonly object ConsoleLock = new();

    [Fact]
    public void Run_WithValidInput_PrintsChangeReport()
    {
        lock (ConsoleLock)
        {
            string output = CaptureConsoleOutput("20\r\n5.50\r\n", () =>
            {
                var app = new ChangeCalculatorApp(new ChangeCalculatorService(), new ChangeCalculatorValidator());
                int exitCode = app.Run();
                Assert.Equal(0, exitCode);
            });

            Assert.Contains("Your change is:", output);
            Assert.Contains("1 x £10", output);
            Assert.Contains("2 x £2", output);
            Assert.Contains("1 x 50p", output);
        }
    }

    [Fact]
    public void Run_WithInvalidAmount_PrintsErrorAndReturnsNonZero()
    {
        lock (ConsoleLock)
        {
            string output = CaptureConsoleOutput("abc\r\n5.50\r\n", () =>
            {
                var app = new ChangeCalculatorApp(new ChangeCalculatorService(), new ChangeCalculatorValidator());
                int exitCode = app.Run();
                Assert.Equal(1, exitCode);
            });

            Assert.Contains("Invalid amount.", output);
        }
    }

    private static string CaptureConsoleOutput(string input, Action action)
    {
        using var inputReader = new StringReader(input);
        using var outputWriter = new StringWriter();

        TextReader originalIn = Console.In;
        TextWriter originalOut = Console.Out;

        try
        {
            Console.SetIn(inputReader);
            Console.SetOut(outputWriter);
            action();
            return outputWriter.ToString();
        }
        finally
        {
            Console.SetIn(originalIn);
            Console.SetOut(originalOut);
        }
    }
}
