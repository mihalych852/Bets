#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем nuget.config и локальные пакеты в контейнер
COPY ../nuget.config ./
COPY ../LocalNuget/ ./LocalNuget/

# Копируем всю папку NotificationService в контейнер
COPY ../NotificationService ./NotificationService

# Устанавливаем рабочую директорию для восстановления зависимостей
WORKDIR "/src/NotificationService/NotificationService.RabbitConsumers"

RUN dotnet restore "NotificationService.RabbitConsumers.csproj"


# Копируем остальные файлы проекта NotificationService.RabbitConsumers и собираем приложение

COPY NotificationService/NotificationService.RabbitConsumers/. ./
RUN dotnet build "NotificationService.RabbitConsumers.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NotificationService.RabbitConsumers.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationService.RabbitConsumers.dll"]