# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /app
COPY . ./
WORKDIR /app/ReWear.DeathClothe.API
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/runtime-deps:8.0 AS runtime

# Instala dependencias y runtime 9 preview
RUN apt-get update && apt-get install -y wget tar gzip libicu72 libssl3 ca-certificates && \
    wget -O dotnet.tar.gz https://aka.ms/dotnet-runtime-9.0.0-preview-linux-x64 && \
    mkdir -p /usr/share/dotnet && \
    tar -zxf dotnet.tar.gz -C /usr/share/dotnet && \
    rm dotnet.tar.gz

ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH=$PATH:/usr/share/dotnet

WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ReWear.DeathClothe.API.dll"]
