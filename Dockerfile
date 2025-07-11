FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /app

# Copia todo el código fuente
COPY . .

# Restaurar la solución completa
RUN dotnet restore DeathClothe-Backend.sln

# Publicar el proyecto principal desde la solución
RUN dotnet publish ReWear.DeathClothe.API/ReWear.DeathClothe.API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ReWear.DeathClothe.API.dll"]
