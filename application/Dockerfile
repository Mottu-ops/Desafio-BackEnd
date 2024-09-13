# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar arquivos de projeto e restaurar dependências
COPY ["DesafioBackend.sln", "./"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Presentation/Presentation.csproj", "src/Presentation/"]
RUN dotnet restore

# Copiar todo o código e publicar a aplicação
COPY . .
RUN dotnet publish "src/Presentation/Presentation.csproj" -c Release -o /app/out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "Presentation.dll"]
