FROM mcr.microsoft.com/dotnet/sdk:6.0  AS publish
WORKDIR /src
EXPOSE 80
EXPOSE 443

COPY . .

COPY *.sln ./
RUN dotnet restore

COPY ../ ./

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final

COPY --from=publish /app/publish .
