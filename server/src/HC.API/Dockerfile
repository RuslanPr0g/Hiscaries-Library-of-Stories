#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HC.API/HC.API.csproj", "HC.API/"]
COPY ["HC.Application.Common/HC.Application.Common.csproj", "HC.Application.Common/"]
COPY ["HC.Domain/HC.Domain.csproj", "HC.Domain/"]
COPY ["HC.Application.Read/HC.Application.Read.csproj", "HC.Application.Read/"]
COPY ["HC.Application.Write/HC.Application.Write.csproj", "HC.Application.Write/"]
COPY ["HC.Infrastructure.EventHandlers/HC.Infrastructure.EventHandlers.csproj", "HC.Infrastructure.EventHandlers/"]
COPY ["HC.Infrastructure.Jobs/HC.Infrastructure.Jobs.csproj", "HC.Infrastructure.Jobs/"]
COPY ["HC.Persistence.Context/HC.Persistence.Context.csproj", "HC.Persistence.Context/"]
COPY ["HC.Persistence.Write/HC.Persistence.Write.csproj", "HC.Persistence.Write/"]
COPY ["HC.Persistence.Read/HC.Persistence.Read.csproj", "HC.Persistence.Read/"]
RUN dotnet restore "./HC.API/HC.API.csproj"
COPY . .
WORKDIR "/src/HC.API"
RUN dotnet build "./HC.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HC.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HC.API.dll"]