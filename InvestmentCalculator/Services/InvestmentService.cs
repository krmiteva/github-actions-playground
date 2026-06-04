namespace InvestmentCalculator.Services;

public class InvestmentService
{
    public decimal Calculate(decimal initialAmount, int years, decimal monthlyContribution, decimal annualReturnRate)
    {
        decimal rate = annualReturnRate / 100;
        decimal monthlyRate = rate / 12;
        int totalMonths = years * 12;

        decimal futureValueInitial = initialAmount * (decimal)Math.Pow((double)(1 + rate), years);

        decimal futureValueContributions = monthlyContribution == 0 || monthlyRate == 0
            ? monthlyContribution * totalMonths
            : monthlyContribution * ((decimal)Math.Pow((double)(1 + monthlyRate), totalMonths) - 1) / monthlyRate;

        return Math.Round(futureValueInitial + futureValueContributions, 2);
    }
}
