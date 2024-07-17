#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HC.API/HC.API.csproj", "HC.API/"]
COPY ["HC.Domain/HC.Domain.csproj", "HC.Domain/"]
COPY ["HC.Infrastructure/HC.Infrastructure.csproj", "HC.Infrastructure/"]
COPY ["HC.Application/HC.Application.csproj", "HC.Application/"]
RUN dotnet restore "HC.API/HC.API.csproj"
COPY . .
WORKDIR "/src/HC.API"
RUN dotnet build "HC.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HC.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HC.API.dll"]