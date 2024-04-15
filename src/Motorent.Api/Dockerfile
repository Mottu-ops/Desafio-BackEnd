FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Motorent.Api/Motorent.Api.csproj", "src/Motorent.Api/"]
COPY ["src/Motorent.Infrastructure/Motorent.Infrastructure.csproj", "src/Motorent.Infrastructure/"]
COPY ["src/Motorent.Application/Motorent.Application.csproj", "src/Motorent.Application/"]
COPY ["src/Motorent.Contracts/Motorent.Contracts.csproj", "src/Motorent.Contracts/"]
COPY ["src/Motorent.Domain/Motorent.Domain.csproj", "src/Motorent.Domain/"]
COPY ["src/Motorent.Presentation/Motorent.Presentation.csproj", "src/Motorent.Presentation/"]
RUN dotnet restore "src/Motorent.Api/Motorent.Api.csproj"
COPY . .
WORKDIR "/src/src/Motorent.Api"
RUN dotnet build "Motorent.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Motorent.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Motorent.Api.dll"]
