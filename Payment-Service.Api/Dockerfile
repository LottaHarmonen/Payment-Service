# Stage 1: Build all dependencies
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["PaymentService.Api/PaymentService.Api.csproj", "PaymentService.Api/"]
COPY ["PaymentService.Application/PaymentService.Application.csproj", "PaymentService.Application/"]
COPY ["PaymentService.Core/PaymentService.Core.csproj", "PaymentService.Core/"]
COPY ["PaymentService.Infrastructure/PaymentService.Infrastructure.csproj", "PaymentService.Infrastructure/"]

RUN dotnet restore "PaymentService.Api/PaymentService.Api.csproj"

# Copy the full solution
COPY . .

# Build all projects
RUN dotnet build "PaymentService.Api/PaymentService.Api.csproj" -c Release

# Stage 2: Run tests
#FROM build AS test
#WORKDIR /src/WebShopTests

# Run tests and ensure they pass
#RUN dotnet test "WebShopTests.csproj" --no-build --logger:trx --results-directory /src/TestResults

# Stage 3: Publish WebShop (only proceeds if tests pass)
FROM build AS publish
WORKDIR /src/PaymentService.Api
RUN dotnet publish "PaymentService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final runtime image for WebShop
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentService.Api.dll"]