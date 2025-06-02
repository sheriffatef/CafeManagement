#!/bin/bash
set -e

# Run migrations
echo "Running database migrations..."
dotnet ef database update --project /app/Persistence.dll --startup-project /app/CafeManagement.dll
echo "Migrations completed successfully!"

# Start the application
exec dotnet CafeManagement.dll
