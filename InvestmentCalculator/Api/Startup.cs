namespace InvestmentCalculator.Api;

public static class Startup
{
    public static WebApplication AddApi(this WebApplication app)
    {
        app.MapGet("/calculate", (decimal initialAmount, int years, decimal monthlyContribution, decimal annualReturnRate) =>
        {
            decimal rate = annualReturnRate / 100;
            decimal monthlyRate = rate / 12;
            int totalMonths = years * 12;

            decimal futureValueInitial = initialAmount * (decimal)Math.Pow((double)(1 + rate), years);

            decimal futureValueContributions = monthlyContribution == 0 || monthlyRate == 0
                ? monthlyContribution * totalMonths
                : monthlyContribution * ((decimal)Math.Pow((double)(1 + monthlyRate), totalMonths) - 1) / monthlyRate;

            decimal endAmount = futureValueInitial + futureValueContributions;

            return Results.Ok(new
            {
                InitialAmount = initialAmount,
                Years = years,
                MonthlyContribution = monthlyContribution,
                AnnualReturnRate = annualReturnRate,
                EndAmount = Math.Round(endAmount, 2)
            });
        });

        return app;
    }
}
