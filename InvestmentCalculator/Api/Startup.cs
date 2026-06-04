using InvestmentCalculator.Services;

namespace InvestmentCalculator.Api;

public static class Startup
{
    public static WebApplication AddApi(this WebApplication app)
    {
        app.MapGet("/calculate", (decimal initialAmount, int years, decimal monthlyContribution, decimal annualReturnRate, InvestmentService service) =>
        {
            decimal endAmount = service.Calculate(initialAmount, years, monthlyContribution, annualReturnRate);

            return Results.Ok(new
            {
                InitialAmount = initialAmount,
                Years = years,
                MonthlyContribution = monthlyContribution,
                AnnualReturnRate = annualReturnRate,
                EndAmount = endAmount
            });
        });

        return app;
    }
}
