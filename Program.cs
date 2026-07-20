using ChangeCalculator.Services;

var app = new ChangeCalculatorApp(
    new ChangeCalculatorService(),
    new ChangeCalculatorValidator());

return app.Run();