# Stage 1: Build all dependencies
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["Payment-Service.Api/Payment-Service.Api.csproj", "Payment-Service.Api/"]
COPY ["Payment-Service.Application/Payment-Service.Application.csproj", "Payment-Service.Application/"]
COPY ["Payment-Service.Core/Payment-Service.Core.csproj", "Payment-Service.Core/"]
COPY ["Payment-Service.Infrastructure/Payment-Service.Infrastructure.csproj", "Payment-Service.Infrastructure/"]

RUN dotnet restore "Payment-Service.Api/Payment-Service.Api.csproj"

# Copy the full solution
COPY . .

# Build all projects
RUN dotnet build "Payment-Service.Api/Payment-Service.Api.csproj" -c Release

# Stage 2: Run tests
#FROM build AS test
#WORKDIR /src/WebShopTests

# Run tests and ensure they pass
#RUN dotnet test "WebShopTests.csproj" --no-build --logger:trx --results-directory /src/TestResults

# Stage 3: Publish WebShop (only proceeds if tests pass)
FROM build AS publish
WORKDIR /src/Payment-Service.Api
RUN dotnet publish "Payment-Service.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final runtime image for WebShop
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment-Service.Api.dll"]
