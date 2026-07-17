using ChangeCalculator.Services;

var app = new ChangeCalculatorApp(
    new ChangeCalculatorService(),
    new TextConsole());

return app.Run();