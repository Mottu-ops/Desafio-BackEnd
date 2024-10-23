# Dockerfile

# Imagem base para ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Imagem para build e restore
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar os arquivos de projeto
COPY ["MT.Backend.Challenge/src/MT.Backend.Challenge.Api/MT.Backend.Challenge.Api.csproj", "MT.Backend.Challenge/src/MT.Backend.Challenge.Api/"]
COPY ["MT.Backend.Challenge/src/MT.Backend.Challenge.Application/MT.Backend.Challenge.Application.csproj", "MT.Backend.Challenge/src/MT.Backend.Challenge.Application/"]
COPY ["MT.Backend.Challenge/src/MT.Backend.Challenge.Domain/MT.Backend.Challenge.Domain.csproj", "MT.Backend.Challenge/src/MT.Backend.Challenge.Domain/"]
COPY ["MT.Backend.Challenge/src/MT.Backend.Challenge.CrossCutting/MT.Backend.Challenge.CrossCutting.csproj", "MT.Backend.Challenge/src/MT.Backend.Challenge.CrossCutting/"]
COPY ["MT.Backend.Challenge/src/MT.Backend.Challenge.Infrastructure/MT.Backend.Challenge.Infrastructure.csproj", "MT.Backend.Challenge/src/MT.Backend.Challenge.Infrastructure/"]

# Restaurar as dependências
RUN dotnet restore "./MT.Backend.Challenge/src/MT.Backend.Challenge.Api/MT.Backend.Challenge.Api.csproj"

# Copiar o restante do código e construir a aplicação
COPY . .
WORKDIR "/src/MT.Backend.Challenge/src/MT.Backend.Challenge.Api"
RUN dotnet build "./MT.Backend.Challenge.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicar a aplicação para produção
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MT.Backend.Challenge.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MT.Backend.Challenge.Api.dll"]
