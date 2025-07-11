FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /app

COPY ReWear.DeathClothe.API/ ./ReWear.DeathClothe.API/
WORKDIR /app/ReWear.DeathClothe.API

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS runtime
WORKDIR /app
COPY --from=build /app/ReWear.DeathClothe.API/out ./

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ReWear.DeathClothe.API.dll"]
