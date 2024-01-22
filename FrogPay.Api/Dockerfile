FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:latest AS runtime
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "FrogPay.Api.dll"]