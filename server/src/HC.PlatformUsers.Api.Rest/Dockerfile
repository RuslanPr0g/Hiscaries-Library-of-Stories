#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HC.PlatformUsers.Api.Rest/HC.PlatformUsers.Api.Rest.csproj", "HC.PlatformUsers.Api.Rest/"]
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
COPY ["HC.PlatformUsers.Application.Read/HC.PlatformUsers.Application.Read.csproj", "HC.PlatformUsers.Application.Read/"]
COPY ["HC.PlatformUsers.Domain/HC.PlatformUsers.Domain.csproj", "HC.PlatformUsers.Domain/"]
COPY ["HC.PlatformUsers.Domain.Events/HC.PlatformUsers.Domain.Events.csproj", "HC.PlatformUsers.Domain.Events/"]
COPY ["HC.UserAccounts.Domain/HC.UserAccounts.Domain.csproj", "HC.UserAccounts.Domain/"]
COPY ["HC.UserAccounts.Domain.Events/HC.UserAccounts.Domain.Events.csproj", "HC.UserAccounts.Domain.Events/"]
COPY ["HC.PlatformUsers.Application.Write/HC.PlatformUsers.Application.Write.csproj", "HC.PlatformUsers.Application.Write/"]
COPY ["HC.PlatformUsers.EventHandlers/HC.PlatformUsers.EventHandlers.csproj", "HC.PlatformUsers.EventHandlers/"]
COPY ["Enterprise.EventHandlers/Enterprise.EventHandlers.csproj", "Enterprise.EventHandlers/"]
COPY ["HC.Stories.Domain.Events/HC.Stories.Domain.Events.csproj", "HC.Stories.Domain.Events/"]
COPY ["HC.PlatformUsers.Jobs/HC.PlatformUsers.Jobs.csproj", "HC.PlatformUsers.Jobs/"]
COPY ["HC.PlatformUsers.Persistence.Context/HC.PlatformUsers.Persistence.Context.csproj", "HC.PlatformUsers.Persistence.Context/"]
COPY ["HC.PlatformUsers.Persistence.Read/HC.PlatformUsers.Persistence.Read.csproj", "HC.PlatformUsers.Persistence.Read/"]
COPY ["HC.PlatformUsers.Persistence.Write/HC.PlatformUsers.Persistence.Write.csproj", "HC.PlatformUsers.Persistence.Write/"]
RUN dotnet restore "./HC.PlatformUsers.Api.Rest/HC.PlatformUsers.Api.Rest.csproj"
COPY . .
WORKDIR "/src/HC.PlatformUsers.Api.Rest"
RUN dotnet build "./HC.PlatformUsers.Api.Rest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HC.PlatformUsers.Api.Rest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HC.PlatformUsers.Api.Rest.dll"]