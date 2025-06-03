#!/bin/bash

set -e  # Exit on any error

# Update and restore workloads
dotnet workload update
dotnet workload restore

# Ensure user secrets are initialized
projectPath="HC.AppHost/HC.AppHost.csproj"
dotnet user-secrets init --project "$projectPath"

# Generate a 32-character alphanumeric JWT key
jwtKey=$(cat /dev/urandom | tr -dc 'A-Za-z0-9' | head -c 32)
salt='$2a$12$m5lW8F/uqqVKO2qTkBxZSe'

# Set user secrets
dotnet user-secrets set "JwtSettings:Key" "$jwtKey" --project "$projectPath"
dotnet user-secrets set "JwtSettings:Issuer" "hiscary" --project "$projectPath"
dotnet user-secrets set "JwtSettings:Audience" "users" --project "$projectPath"
dotnet user-secrets set "JwtSettings:TokenLifeTime" "30.00:00:00" --project "$projectPath"
dotnet user-secrets set "SaltSettings:StoredSalt" "$salt" --project "$projectPath"

# Resolve absolute path to the wwwroot of the Media API
mediaProjectPath="HC.Media.Api.Rest/HC.Media.Api.Rest.csproj"
mediaRoot=$(dirname "$(realpath "$mediaProjectPath")")
wwwrootPath="$mediaRoot/wwwroot"

# Add StorageUrl to user secrets
dotnet user-secrets set "ResourceSettings:StorageUrl" "$wwwrootPath" --project "$projectPath"

