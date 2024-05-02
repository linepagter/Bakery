FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /source

COPY Bakery/*.csproj .
RUN dotnet restore

WORKDIR /source/Bakery
COPY Bakery/. .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Bakery.dll"]  


