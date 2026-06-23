using InvestmentCalculator.Api;
using InvestmentCalculator.Grpc;
using InvestmentCalculator.Services;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<InvestmentService>();
builder.Services.AddOpenApi();
builder.Services.AddGrpc();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.AddApi();
app.MapGrpcService<InvestmentGrpcService>();

app.Run();
