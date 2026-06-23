using Grpc.Core;
using InvestmentCalculator.Grpc;
using DomainService = InvestmentCalculator.Services.InvestmentService;

namespace InvestmentCalculator.Grpc;

public class InvestmentGrpcService(DomainService investmentService) : InvestmentCalculatorService.InvestmentCalculatorServiceBase
{
    public override Task<CalculateResponse> Calculate(CalculateRequest request, ServerCallContext context)
    {
        decimal endAmount = investmentService.Calculate(
            (decimal)request.InitialAmount,
            request.Years,
            (decimal)request.MonthlyContribution,
            (decimal)request.AnnualReturnRate);

        return Task.FromResult(new CalculateResponse
        {
            InitialAmount = request.InitialAmount,
            Years = request.Years,
            MonthlyContribution = request.MonthlyContribution,
            AnnualReturnRate = request.AnnualReturnRate,
            EndAmount = (double)endAmount
        });
    }
}
