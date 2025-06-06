# PowerShell script to delete all 'bin' and 'obj' folders recursively with confirmation and logging

# Prompt for confirmation
$confirmation = Read-Host "Are you sure you want to delete all 'bin' and 'obj' folders under $(Get-Location)? (y/n)"
if ($confirmation -ne 'y') {
    Write-Host "Operation cancelled."
    exit
}

# Find and delete 'bin' and 'obj' directories
Get-ChildItem -Path . -Recurse -Directory -Include bin,obj | ForEach-Object {
    try {
        Write-Host "Deleting: $($_.FullName)"
        Remove-Item -Recurse -Force -LiteralPath $_.FullName
        Write-Host "Deleted: $($_.FullName)"
    } catch {
        Write-Error "Failed to delete: $($_.FullName) - $_"
    }
}

Write-Host "All 'bin' and 'obj' folders have been processed."
