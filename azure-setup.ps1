# Azure PostgreSQL Setup Script for Houseiana Backend
# Run this script after deploying your Azure PostgreSQL Flexible Server

# Set your Azure PostgreSQL details here
$AZURE_POSTGRES_HOST = "your-server-name.postgres.database.azure.com"
$AZURE_POSTGRES_USER = "your-admin-username"
$AZURE_POSTGRES_PASSWORD = "your-admin-password"
$DATABASE_NAME = "houseiana"

Write-Host "Setting up Azure PostgreSQL connection for Houseiana..." -ForegroundColor Green

# Update appsettings.Production.json with actual values
$productionSettings = Get-Content "appsettings.Production.json" | ConvertFrom-Json
$connectionString = "Host=$AZURE_POSTGRES_HOST;Port=5432;Database=$DATABASE_NAME;Username=$AZURE_POSTGRES_USER;Password=$AZURE_POSTGRES_PASSWORD;SSL Mode=Require;Trust Server Certificate=true;Timeout=30;Command Timeout=30"

Write-Host "Connection String: $connectionString" -ForegroundColor Yellow

# Set environment variables for local testing
[Environment]::SetEnvironmentVariable("AZURE_POSTGRES_HOST", $AZURE_POSTGRES_HOST, "User")
[Environment]::SetEnvironmentVariable("AZURE_POSTGRES_USER", $AZURE_POSTGRES_USER, "User")
[Environment]::SetEnvironmentVariable("AZURE_POSTGRES_PASSWORD", $AZURE_POSTGRES_PASSWORD, "User")

Write-Host "Environment variables set. Please restart your terminal/IDE." -ForegroundColor Green

# Test connection (optional)
Write-Host "Testing connection to Azure PostgreSQL..." -ForegroundColor Yellow

try {
    # Create initial migration if it doesn't exist
    if (!(Test-Path "Migrations")) {
        Write-Host "Creating initial database migration..." -ForegroundColor Yellow
        dotnet ef migrations add InitialCreate
    }

    # Update database schema
    Write-Host "Updating database schema..." -ForegroundColor Yellow
    dotnet ef database update

    Write-Host "Database setup completed successfully!" -ForegroundColor Green
}
catch {
    Write-Host "Error setting up database: $_" -ForegroundColor Red
    Write-Host "Please check your connection details and try again." -ForegroundColor Red
}

Write-Host @"

Next Steps:
1. Update the variables at the top of this script with your actual Azure PostgreSQL details
2. Run: ./azure-setup.ps1
3. For production deployment, set these environment variables in your Azure App Service:
   - AZURE_POSTGRES_HOST
   - AZURE_POSTGRES_USER
   - AZURE_POSTGRES_PASSWORD
   - JWT_SECRET_KEY

4. Test your connection with: dotnet run

"@ -ForegroundColor Cyan