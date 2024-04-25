FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /src
EXPOSE 80

#copy all .csproj files and restore as distinct layers.
COPY Challenge.sln Challenge.sln

#user
COPY src/services/user/User.API/User.API.csproj src/services/user/User.API/User.API.csproj
COPY src/services/user/User.Domain/User.Domain.csproj src/services/user/User.Domain/User.Domain.csproj
COPY src/services/user/User.Core/User.Core.csproj src/services/user/User.Core/User.Core.csproj
COPY src/services/user/User.Infra/User.Infra.csproj src/services/user/User.Infra/User.Infra.csproj
COPY src/services/user/User.Service/User.Service.csproj src/services/user/User.Service/User.Service.csproj

#Motorcycle
COPY src/services/motorcycle/Motorcycle.API/Motorcycle.API.csproj src/services/motorcycle/Motorcycle.API/Motorcycle.API.csproj
COPY src/services/motorcycle/Motorcycle.Domain/Motorcycle.Domain.csproj src/services/motorcycle/Motorcycle.Domain/Motorcycle.Domain.csproj
COPY src/services/motorcycle/Motorcycle.Core/Motorcycle.Core.csproj src/services/motorcycle/Motorcycle.Core/Motorcycle.Core.csproj
COPY src/services/motorcycle/Motorcycle.Infra/Motorcycle.Infra.csproj src/services/motorcycle/Motorcycle.Infra/Motorcycle.Infra.csproj
COPY src/services/motorcycle/Motorcycle.Service/Motorcycle.Service.csproj src/services/motorcycle/Motorcycle.Service/Motorcycle.Service.csproj

#Plan
COPY src/services/plan/Plan.API/Plan.API.csproj src/services/plan/Plan.API/Plan.API.csproj
COPY src/services/plan/Plan.Domain/Plan.Domain.csproj src/services/plan/Plan.Domain/Plan.Domain.csproj
COPY src/services/plan/Plan.Core/Plan.Core.csproj src/services/plan/Plan.Core/Plan.Core.csproj
COPY src/services/plan/Plan.Infra/Plan.Infra.csproj src/services/plan/Plan.Infra/Plan.Infra.csproj
COPY src/services/plan/Plan.Service/Plan.Service.csproj src/services/plan/Plan.Service/Plan.Service.csproj

#Rent
COPY src/services/rent/Rent.API/Rent.API.csproj src/services/rent/Rent.API/Rent.API.csproj
COPY src/services/rent/Rent.Domain/Rent.Domain.csproj src/services/rent/Rent.Domain/Rent.Domain.csproj
COPY src/services/rent/Rent.Core/Rent.Core.csproj src/services/rent/Rent.Core/Rent.Core.csproj
COPY src/services/rent/Rent.Infra/Rent.Infra.csproj src/services/rent/Rent.Infra/Rent.Infra.csproj
COPY src/services/rent/Rent.Service/Rent.Service.csproj src/services/rent/Rent.Service/Rent.Service.csproj

#Order
COPY src/services/order/Order.API/Order.API.csproj src/services/order/Order.API/Order.API.csproj
COPY src/services/order/Order.Domain/Order.Domain.csproj src/services/order/Order.Domain/Order.Domain.csproj
COPY src/services/order/Order.Core/Order.Core.csproj src/services/order/Order.Core/Order.Core.csproj
COPY src/services/order/Order.Infra/Order.Infra.csproj src/services/order/Order.Infra/Order.Infra.csproj
COPY src/services/order/Order.Service/Order.Service.csproj src/services/order/Order.Service/Order.Service.csproj

#Logger
COPY src/services/logger/Logger.API/Logger.API.csproj src/services/logger/Logger.API/Logger.API.csproj

#BusConnection
COPY src/BusConnection/BusConnection.Core/BusConnections.Core.csproj src/BusConnection/BusConnection.Core/BusConnections.Core.csproj
COPY src/BusConnection/BusConnection.Events/BusConnections.Events.csproj src/BusConnection/BusConnection.Events/BusConnections.Events.csproj

#Unit tests
COPY src/tests/Order.Tests/Order.Tests.csproj src/tests/Order.Tests/Order.Tests.csproj

#Restore package deps
RUN dotnet restore Challenge.sln
COPY src/services/order/Order.API src/services/order/Order.API
COPY src/services/order/Order.Domain src/services/order/Order.Domain
COPY src/services/order/Order.Core src/services/order/Order.Core
COPY src/services/order/Order.Infra src/services/order/Order.Infra
COPY src/services/order/Order.Service src/services/order/Order.Service
COPY src/BusConnection/BusConnection.Core src/BusConnection/BusConnection.Core
COPY src/BusConnection/BusConnection.Events src/BusConnection/BusConnection.Events
WORKDIR "/src/src/services/order/Order.API"
RUN dotnet build "./Order.API.csproj" -c Release -o /app/src/out

#Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet",  "Order.API.dll"]
