FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /app

# Copia todo el contenido del backend (la solución completa)
COPY . .

# Ve a la carpeta donde está el proyecto principal
WORKDIR /app/ReWear.DeathClothe.API

# Restaura y publica
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS runtime
WORKDIR /app
COPY --from=build /app/ReWear.DeathClothe.API/out ./

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ReWear.DeathClothe.API.dll"]
