#!/bin/bash

# Azure PostgreSQL Setup Script for Houseiana Backend
# Run this script after deploying your Azure PostgreSQL Flexible Server

# Set your Azure PostgreSQL details here
AZURE_POSTGRES_HOST="your-server-name.postgres.database.azure.com"
AZURE_POSTGRES_USER="your-admin-username"
AZURE_POSTGRES_PASSWORD="your-admin-password"
DATABASE_NAME="houseiana"

echo "🚀 Setting up Azure PostgreSQL connection for Houseiana..."

# Create connection string
CONNECTION_STRING="Host=$AZURE_POSTGRES_HOST;Port=5432;Database=$DATABASE_NAME;Username=$AZURE_POSTGRES_USER;Password=$AZURE_POSTGRES_PASSWORD;SSL Mode=Require;Trust Server Certificate=true;Timeout=30;Command Timeout=30"

echo "📝 Connection String: $CONNECTION_STRING"

# Set environment variables
export AZURE_POSTGRES_HOST="$AZURE_POSTGRES_HOST"
export AZURE_POSTGRES_USER="$AZURE_POSTGRES_USER"
export AZURE_POSTGRES_PASSWORD="$AZURE_POSTGRES_PASSWORD"

echo "✅ Environment variables set for current session"

# Test connection and setup database
echo "🔧 Testing connection to Azure PostgreSQL..."

# Create initial migration if it doesn't exist
if [ ! -d "Migrations" ]; then
    echo "📊 Creating initial database migration..."
    dotnet ef migrations add InitialCreate
fi

# Update database schema
echo "🗄️ Updating database schema..."
if dotnet ef database update; then
    echo "✅ Database setup completed successfully!"
else
    echo "❌ Error setting up database. Please check your connection details."
    exit 1
fi

echo "
📋 Next Steps:
1. Update the variables at the top of this script with your actual Azure PostgreSQL details
2. Run: chmod +x azure-setup.sh && ./azure-setup.sh
3. For production deployment, set these environment variables in your Azure App Service:
   - AZURE_POSTGRES_HOST
   - AZURE_POSTGRES_USER
   - AZURE_POSTGRES_PASSWORD
   - JWT_SECRET_KEY

4. Test your connection with: dotnet run
"