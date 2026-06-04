FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY InvestmentCalculator/InvestmentCalculator.csproj InvestmentCalculator/
COPY InvestmentCalculator.Tests/InvestmentCalculator.Tests.csproj InvestmentCalculator.Tests/
RUN dotnet restore InvestmentCalculator/InvestmentCalculator.csproj

COPY . .
RUN dotnet publish InvestmentCalculator/InvestmentCalculator.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "InvestmentCalculator.dll"]
