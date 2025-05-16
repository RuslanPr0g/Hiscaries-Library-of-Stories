#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HC.Stories.Api.Rest/HC.Stories.Api.Rest.csproj", "HC.Stories.Api.Rest/"]
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
COPY ["HC.Stories.Application.Read/HC.Stories.Application.Read.csproj", "HC.Stories.Application.Read/"]
COPY ["HC.Stories.Domain/HC.Stories.Domain.csproj", "HC.Stories.Domain/"]
COPY ["HC.Stories.Domain.Events/HC.Stories.Domain.Events.csproj", "HC.Stories.Domain.Events/"]
COPY ["HC.Stories.Application.Write/HC.Stories.Application.Write.csproj", "HC.Stories.Application.Write/"]
COPY ["HC.Stories.EventHandlers/HC.Stories.EventHandlers.csproj", "HC.Stories.EventHandlers/"]
COPY ["Enterprise.EventHandlers/Enterprise.EventHandlers.csproj", "Enterprise.EventHandlers/"]
COPY ["HC.Stories.Jobs/HC.Stories.Jobs.csproj", "HC.Stories.Jobs/"]
COPY ["HC.Stories.Persistence.Context/HC.Stories.Persistence.Context.csproj", "HC.Stories.Persistence.Context/"]
COPY ["HC.Stories.Persistence.Read/HC.Stories.Persistence.Read.csproj", "HC.Stories.Persistence.Read/"]
COPY ["HC.Stories.Persistence.Write/HC.Stories.Persistence.Write.csproj", "HC.Stories.Persistence.Write/"]
RUN dotnet restore "./HC.Stories.Api.Rest/HC.Stories.Api.Rest.csproj"
COPY . .
WORKDIR "/src/HC.Stories.Api.Rest"
RUN dotnet build "./HC.Stories.Api.Rest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HC.Stories.Api.Rest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HC.Stories.Api.Rest.dll"]