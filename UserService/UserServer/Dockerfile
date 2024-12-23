# Базовый образ для ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем всю папку UserServer в контейнер
COPY ../UserService ./UserService

# Устанавливаем рабочую директорию для восстановления зависимостей
WORKDIR "/src/UserService/UserServer"

RUN dotnet restore "UserServer.WebHost.csproj"

# Собираем приложение
RUN dotnet build "UserServer.WebHost.csproj" -c Release -o /app/build

# Этап публикации
FROM build AS publish
RUN dotnet publish "UserServer.WebHost.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserServer.WebHost.dll"]
