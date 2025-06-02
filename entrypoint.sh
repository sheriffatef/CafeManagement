#!/bin/bash
set -e

# Run migrations
echo "Running database migrations..."
# Use the EF migrations bundle that was created during build
./efbundle --connection "${ConnectionStrings__DefaultConnection}"
echo "Migrations completed successfully!"

# Start the application
exec dotnet CafeManagement.dll
