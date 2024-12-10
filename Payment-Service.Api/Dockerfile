# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["Payment-Service.Api/Payment-Service.Api.csproj", "Payment-Service.Api/"]
COPY ["Payment-Service.Application/Payment-Service.Application.csproj", "Payment-Service.Application/"]
COPY ["Payment-Service.Core/Payment-Service.Core.csproj", "Payment-Service.Core/"]
COPY ["Payment-Service.Infrastructure/Payment-Service.Infrastructure.csproj", "Payment-Service.Infrastructure/"]

RUN dotnet restore "Payment-Service.Api/Payment-Service.Api.csproj"

# Copy the entire source and build
COPY . .
RUN dotnet build "Payment-Service.Api/Payment-Service.Api.csproj" -c Release

# Stage 2: Publish
FROM build AS publish
WORKDIR /src/Payment-Service.Api
RUN dotnet publish "Payment-Service.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment-Service.Api.dll"]