FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HC.UserAccounts.Api.Rest/HC.UserAccounts.Api.Rest.csproj", "HC.UserAccounts.Api.Rest/"]
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
COPY ["HC.UserAccounts.Application.Write/HC.UserAccounts.Application.Write.csproj", "HC.UserAccounts.Application.Write/"]
COPY ["HC.UserAccounts.Domain/HC.UserAccounts.Domain.csproj", "HC.UserAccounts.Domain/"]
COPY ["HC.UserAccounts.Domain.Events/HC.UserAccounts.Domain.Events.csproj", "HC.UserAccounts.Domain.Events/"]
COPY ["HC.UserAccounts.EventHandlers/HC.UserAccounts.EventHandlers.csproj", "HC.UserAccounts.EventHandlers/"]
COPY ["Enterprise.EventHandlers/Enterprise.EventHandlers.csproj", "Enterprise.EventHandlers/"]
COPY ["HC.UserAccounts.Jobs/HC.UserAccounts.Jobs.csproj", "HC.UserAccounts.Jobs/"]
COPY ["HC.UserAccounts.Persistence.Context/HC.UserAccounts.Persistence.Context.csproj", "HC.UserAccounts.Persistence.Context/"]
COPY ["HC.UserAccounts.Persistence.Write/HC.UserAccounts.Persistence.Write.csproj", "HC.UserAccounts.Persistence.Write/"]
RUN dotnet restore "HC.UserAccounts.Api.Rest/HC.UserAccounts.Api.Rest.csproj"
COPY . .
WORKDIR "/src/HC.UserAccounts.Api.Rest"
RUN dotnet publish "HC.UserAccounts.Api.Rest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
USER app
ENTRYPOINT ["dotnet", "HC.UserAccounts.Api.Rest.dll"]