# Base utilisée pour l'exécution (définie deux fois dans ton original, on garde une seule)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# 🔽 Ajout pour corriger le problème de certificat
RUN apt-get update && \
    apt-get install -y ca-certificates && \
    update-ca-certificates

# Phase de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["api/api.csproj", "api/"]
RUN dotnet restore "./api/api.csproj"
COPY . .
WORKDIR "/src/api"
RUN dotnet build "./api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Phase de publication
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Phase finale (exécution)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api.dll"]
