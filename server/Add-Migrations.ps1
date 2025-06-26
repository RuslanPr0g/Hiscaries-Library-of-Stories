# PowerShell script to add EF Core migration to a selected persistence project

# Define project mappings (persistence -> startup)
$projectMappings = @{
    "Hiscary.UserAccounts.Persistence.Context/Hiscary.UserAccounts.Persistence.Context.csproj" = "Hiscary.UserAccounts.Api.Rest/Hiscary.UserAccounts.Api.Rest.csproj"
    "Hiscary.Notifications.Persistence.Context/Hiscary.Notifications.Persistence.Context.csproj" = "Hiscary.Notifications.Api.Rest/Hiscary.Notifications.Api.Rest.csproj"
    "Hiscary.Stories.Persistence.Context/Hiscary.Stories.Persistence.Context.csproj" = "Hiscary.Stories.Api.Rest/Hiscary.Stories.Api.Rest.csproj"
    "Hiscary.PlatformUsers.Persistence.Context/Hiscary.PlatformUsers.Persistence.Context.csproj" = "Hiscary.PlatformUsers.Api.Rest/Hiscary.PlatformUsers.Api.Rest.csproj"
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