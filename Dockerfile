FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /app

COPY . ./
WORKDIR /app/ReWear.DeathClothe.API

RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# ⚠️ CAMBIO: usamos SDK también como runtime temporalmente
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ReWear.DeathClothe.API.dll"]
