cd ../src
dotnet ef migrations add %1 -p HC.Infrastructure/HC.Infrastructure.csproj -s HC.API/HC.API.csproj -c HiscaryContext
cd ../scripts