# Базовый образ для ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем nuget.config и локальные пакеты в контейнер
COPY ../nuget.config ./
COPY ../LocalNuget/ ./LocalNuget/

# Копируем всю папку NotificationService в контейнер
COPY ../NotificationService ./NotificationService

# Устанавливаем рабочую директорию для восстановления зависимостей
WORKDIR "/src/NotificationService/NotificationService.Api"

RUN dotnet restore "NotificationService.Api.csproj"

# Копируем остальные файлы проекта NotificationService.Api и собираем приложение

COPY NotificationService/NotificationService.Api/. ./
RUN dotnet build "NotificationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NotificationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationService.Api.dll"]