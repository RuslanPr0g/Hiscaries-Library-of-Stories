#!/bin/bash

set -e

SOLUTION_FILE=$(find . -maxdepth 1 -name "*.sln" | head -n 1)

if [[ -z "$SOLUTION_FILE" ]]; then
  echo "❌ No solution (.sln) file found in current directory."
  exit 1
fi

SOLUTION_FILE="${SOLUTION_FILE#./}"

read -p "Enter the library name: " LIBRARY_NAME

if [[ -z "$LIBRARY_NAME" ]]; then
  echo "❌ Library name cannot be empty."
  exit 1
fi

echo "📦 Creating class library: $LIBRARY_NAME"
dotnet new classlib -n "$LIBRARY_NAME"

echo "🔗 Adding $LIBRARY_NAME to $SOLUTION_FILE"
dotnet sln "$SOLUTION_FILE" add "$LIBRARY_NAME/$LIBRARY_NAME.csproj"

echo "✅ Project '$LIBRARY_NAME' added to solution '$SOLUTION_FILE'."
