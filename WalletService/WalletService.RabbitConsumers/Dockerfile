#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# �������� nuget.config � ��������� ������ � ���������
COPY ../nuget.config ./
COPY ../LocalNuget/ ./LocalNuget/

# �������� ��� ����� NotificationService � ���������
COPY ../WalletService ./WalletService

WORKDIR "/src/WalletService/WalletService.RabbitConsumers"

RUN dotnet restore "WalletService.RabbitConsumers.csproj"

# �������� ��������� ����� ������� WalletService.RabbitConsumers � �������� ����������

COPY WalletService/WalletService.RabbitConsumers/. ./
RUN dotnet build "WalletService.RabbitConsumers.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WalletService.RabbitConsumers.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WalletService.RabbitConsumers.dll"]