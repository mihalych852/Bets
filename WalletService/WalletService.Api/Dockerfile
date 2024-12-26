#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем всю папку UserServer в контейнер
COPY ../WalletService ./WalletService

# Копируем nuget.config и локальные пакеты в контейнер
COPY ../nuget.config ./
COPY ../LocalNuget/ ./LocalNuget/

# Устанавливаем рабочую директорию для восстановления зависимостей
WORKDIR "/src/WalletService/WalletService.Api"

RUN dotnet restore "./WalletService.Api.csproj"

# Собираем приложение
RUN dotnet build "WalletService.Api.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WalletService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WalletService.Api.dll"]