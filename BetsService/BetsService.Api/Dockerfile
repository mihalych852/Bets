# Базовый образ для ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем nuget.config и локальные пакеты в контейнер
COPY ../nuget.config ./
COPY ../LocalNuget/ ./LocalNuget/

# Копируем всю папку BetsService в контейнер
COPY ../BetsService ./BetsService

# Устанавливаем рабочую директорию для восстановления зависимостей
WORKDIR "/src/BetsService/BetsService.Api"

# Восстанавливаем зависимости с использованием nuget.config
RUN dotnet restore "BetsService.Api.csproj"

# Копируем остальные файлы проекта BetsService.Api и собираем приложение
COPY BetsService/BetsService.Api/. ./
RUN dotnet build "BetsService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этап публикации
FROM build AS publish
RUN dotnet publish "BetsService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BetsService.Api.dll"]