# PowerShell script to add EF Core migration to a selected persistence project

# Define project mappings (persistence -> startup)
$projectMappings = @{
    "HC.UserAccounts.Persistence.Context/HC.UserAccounts.Persistence.Context.csproj" = "HC.UserAccounts.Api.Rest/HC.UserAccounts.Api.Rest.csproj"
    "HC.Notifications.Persistence.Context/HC.Notifications.Persistence.Context.csproj" = "HC.Notifications.Api.Rest/HC.Notifications.Api.Rest.csproj"
    "HC.Stories.Persistence.Context/HC.Stories.Persistence.Context.csproj" = "HC.Stories.Api.Rest/HC.Stories.Api.Rest.csproj"
    "HC.PlatformUsers.Persistence.Context/HC.PlatformUsers.Persistence.Context.csproj" = "HC.PlatformUsers.Api.Rest/HC.PlatformUsers.Api.Rest.csproj"
}

# Prompt user to select a persistence project
Write-Host "Select a project to add a migration to:"
$index = 1
$projectMappings.Keys | ForEach-Object { Write-Host "$index. $_"; $index++ }
$choice = Read-Host "Enter the number (1-$($projectMappings.Count))"
$selectedProject = ($projectMappings.Keys | Select-Object -Index ($choice - 1))

if (-not $selectedProject) {
    Write-Host "Invalid selection. Exiting."
    exit
}

# Get corresponding startup project
$startupProject = $projectMappings[$selectedProject]

# Prompt for migration name
$migrationName = Read-Host "Enter migration name"

# Run dotnet ef migrations add
Write-Host "Adding migration '$migrationName' to $selectedProject with startup project $startupProject..."
dotnet ef migrations add $migrationName `
    --project $selectedProject `
    --startup-project $startupProject `
    --output-dir Migrations

Write-Host "Migration '$migrationName' added successfully."