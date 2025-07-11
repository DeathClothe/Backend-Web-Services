# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /app

COPY . ./
WORKDIR /app/ReWear.DeathClothe.API

RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# Etapa de runtime: instalamos manualmente el runtime 9.0-preview
FROM mcr.microsoft.com/dotnet/runtime-deps:8.0 AS runtime

# Instala dependencias necesarias y el runtime 9 preview manualmente
RUN apt-get update && \
    apt-get install -y wget tar gzip libicu72 libssl3 && \
    wget https://download.visualstudio.microsoft.com/download/pr/34b8f4fa-b6f3-43a7-b21d-85cf1ea62bdf/607fe6d9bc6e0a2bbf80e74cb656c914/dotnet-runtime-9.0.0-preview.7.23375.6-linux-x64.tar.gz && \
    mkdir -p /usr/share/dotnet && \
    tar -zxf dotnet-runtime-9.0.0-preview.7.23375.6-linux-x64.tar.gz -C /usr/share/dotnet && \
    rm dotnet-runtime-9.0.0-preview.7.23375.6-linux-x64.tar.gz

ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH=$PATH:/usr/share/dotnet

WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ReWear.DeathClothe.API.dll"]
