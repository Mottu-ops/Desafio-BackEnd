#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/01 - MotorcycleManagementService/MotorcycleRental.AdminManagementService.Api/MotorcycleRental.AdminManagementService.Api.csproj", "src/01 - MotorcycleManagementService/MotorcycleRental.AdminManagementService.Api/"]
COPY ["src/01 - MotorcycleManagementService/MotorcycleRental.AdminManagementService.Service/MotorcycleRental.AdminManagementService.Service.csproj", "src/01 - MotorcycleManagementService/MotorcycleRental.AdminManagementService.Service/"]
COPY ["src/03 - Domain/MotorcycleRental.Domain/MotorcycleRental.Domain.csproj", "src/03 - Domain/MotorcycleRental.Domain/"]
RUN dotnet restore "./src/01 - MotorcycleManagementService/MotorcycleRental.AdminManagementService.Api/./MotorcycleRental.AdminManagementService.Api.csproj"
COPY . .
WORKDIR "/src/src/01 - MotorcycleManagementService/MotorcycleRental.AdminManagementService.Api"
RUN dotnet build "./MotorcycleRental.AdminManagementService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MotorcycleRental.AdminManagementService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MotorcycleRental.AdminManagementService.Api.dll"]