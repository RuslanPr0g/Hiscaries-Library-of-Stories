# PowerShell script to add user secrets to HC.AppHost.csproj

# Ensure user secrets are initialized
$projectPath = "HC.AppHost/HC.AppHost.csproj"
dotnet user-secrets init --project $projectPath

# Generate random values
$jwtKey = -join ((65..90) + (97..122) + (48..57) | Get-Random -Count 32 | % {[char]$_})
$salt = '$2a$12$m5lW8F/uqqVKO2qTkBxZSe'

# Set user secrets
dotnet user-secrets set "JwtSettings:Key" $jwtKey --project $projectPath
dotnet user-secrets set "JwtSettings:Issuer" "hiscary" --project $projectPath
dotnet user-secrets set "JwtSettings:Audience" "users" --project $projectPath
dotnet user-secrets set "JwtSettings:TokenLifeTime" "30.00:00:00" --project $projectPath
dotnet user-secrets set "SaltSettings:StoredSalt" $salt --project $projectPath