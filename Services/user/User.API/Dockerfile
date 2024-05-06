#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY DeliverySolution.sln


#user
COPY src/services/user/User.API/User.API.csproj src/services/user/User.API/User.API.csproj
COPY src/services/user/User.Domain/User.Domain.csproj src/services/user/User.Domain/User.Domain.csproj
COPY src/services/user/User.Infra/User.Infra.csproj src/services/user/User.Infra/User.Infra.csproj
COPY src/services/user/User.Service/User.Service.csproj src/services/user/User.Service/User.Service.csproj
COPY src/services/user/User.Service/User.Service.csproj src/services/user/User.Service/User.S3.Lib.csproj


#Restore package deps
RUN dotnet restore DeliverySolution.sln
COPY src/services/user/User.API src/services/user/User.API
COPY src/services/user/User.Domain src/services/user/User.Domain
COPY src/services/user/User.Infra src/services/user/User.Infra
COPY src/services/user/User.Service src/services/user/User.Service
COPY src/services/user/User.Service src/services/user/User.S3.Lib

WORKDIR "/src/src/services/user/User.API"
RUN dotnet build "./User.API.csproj" -c Release -o /app/src/out

#Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet",  "User.API.dll"]