cd ..
dotnet ef migrations add %1 --startup-project HC.API/HC.API.csproj --project HC.Persistence.Context/HC.Persistence.Context.csproj -c HiscaryContext
cd ../scripts