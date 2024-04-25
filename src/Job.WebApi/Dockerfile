FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID

WORKDIR /app
EXPOSE 8080
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Job.WebApi/Job.WebApi.csproj", "src/Job.WebApi/"]
COPY ["src/Job.Domain/Job.Domain.csproj", "src/Job.Domain/"]
COPY ["src/Job.Infrastructure/Job.Infrastructure.csproj", "src/Job.Infrastructure/"]
RUN dotnet restore "src/Job.WebApi/Job.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Job.WebApi"
RUN dotnet build "Job.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Job.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Job.WebApi.dll"]
