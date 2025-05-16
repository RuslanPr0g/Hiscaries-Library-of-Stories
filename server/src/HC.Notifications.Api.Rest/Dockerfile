#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HC.Notifications.Api.Rest/HC.Notifications.Api.Rest.csproj", "HC.Notifications.Api.Rest/"]
COPY ["Enterprise.Api.Rest/Enterprise.Api.Rest.csproj", "Enterprise.Api.Rest/"]
COPY ["Enterprise.Application/Enterprise.Application.csproj", "Enterprise.Application/"]
COPY ["Enterprise.Domain/Enterprise.Domain.csproj", "Enterprise.Domain/"]
COPY ["Enterprise.FileStorage/Enterprise.FileStorage.csproj", "Enterprise.FileStorage/"]
COPY ["Enterprise.Generators/Enterprise.Generators.csproj", "Enterprise.Generators/"]
COPY ["Enterprise.Images/Enterprise.Images.csproj", "Enterprise.Images/"]
COPY ["Enterprise.Jwt/Enterprise.Jwt.csproj", "Enterprise.Jwt/"]
COPY ["Enterprise.Outbox/Enterprise.Outbox.csproj", "Enterprise.Outbox/"]
COPY ["Enterprise.Persistence.Context/Enterprise.Persistence.Context.csproj", "Enterprise.Persistence.Context/"]
COPY ["Enterprise.Persistence/Enterprise.Persistence.csproj", "Enterprise.Persistence/"]
COPY ["HC.Notifications.Application.Read/HC.Notifications.Application.Read.csproj", "HC.Notifications.Application.Read/"]
COPY ["HC.Notifications.Domain/HC.Notifications.Domain.csproj", "HC.Notifications.Domain/"]
COPY ["HC.Notifications.Domain.Events/HC.Notifications.Domain.Events.csproj", "HC.Notifications.Domain.Events/"]
COPY ["HC.Notifications.Application.Write/HC.Notifications.Application.Write.csproj", "HC.Notifications.Application.Write/"]
COPY ["HC.Notifications.EventHandlers/HC.Notifications.EventHandlers.csproj", "HC.Notifications.EventHandlers/"]
COPY ["Enterprise.EventHandlers/Enterprise.EventHandlers.csproj", "Enterprise.EventHandlers/"]
COPY ["HC.Notifications.SignalR/HC.Notifications.SignalR.csproj", "HC.Notifications.SignalR/"]
COPY ["HC.PlatformUsers.Domain.Events/HC.PlatformUsers.Domain.Events.csproj", "HC.PlatformUsers.Domain.Events/"]
COPY ["HC.Notifications.Jobs/HC.Notifications.Jobs.csproj", "HC.Notifications.Jobs/"]
COPY ["HC.Notifications.Persistence.Context/HC.Notifications.Persistence.Context.csproj", "HC.Notifications.Persistence.Context/"]
COPY ["HC.Notifications.Persistence.Read/HC.Notifications.Persistence.Read.csproj", "HC.Notifications.Persistence.Read/"]
COPY ["HC.Notifications.Persistence.Write/HC.Notifications.Persistence.Write.csproj", "HC.Notifications.Persistence.Write/"]
RUN dotnet restore "./HC.Notifications.Api.Rest/HC.Notifications.Api.Rest.csproj"
COPY . .
WORKDIR "/src/HC.Notifications.Api.Rest"
RUN dotnet build "./HC.Notifications.Api.Rest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HC.Notifications.Api.Rest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HC.Notifications.Api.Rest.dll"]