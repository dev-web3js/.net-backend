# Azure Deployment Script for Houseiana Backend
# This script deploys the ASP.NET 8 application to Azure App Service

param(
    [Parameter(Mandatory=$true)]
    [string]$ResourceGroupName = "Houseiana_group",

    [Parameter(Mandatory=$true)]
    [string]$AppServiceName = "Houseiana",

    [Parameter(Mandatory=$false)]
    [string]$SubscriptionId = "aa935f6b-dd8b-4746-98e0-b60637c451a1"
)

Write-Host "🚀 Starting deployment to Azure App Service..." -ForegroundColor Green

# Login to Azure (if not already logged in)
Write-Host "Checking Azure login status..." -ForegroundColor Yellow
$context = az account show 2>$null | ConvertFrom-Json
if (-not $context) {
    Write-Host "Please login to Azure..." -ForegroundColor Yellow
    az login
}

# Set subscription
Write-Host "Setting subscription to: $SubscriptionId" -ForegroundColor Yellow
az account set --subscription $SubscriptionId

# Build and publish the application
Write-Host "Building and publishing the application..." -ForegroundColor Yellow
dotnet publish -c Release -o ./publish

# Create deployment package
Write-Host "Creating deployment package..." -ForegroundColor Yellow
Compress-Archive -Path "./publish/*" -DestinationPath "./houseiana-backend.zip" -Force

# Deploy to Azure App Service
Write-Host "Deploying to Azure App Service: $AppServiceName" -ForegroundColor Yellow
az webapp deployment source config-zip --resource-group $ResourceGroupName --name $AppServiceName --src "./houseiana-backend.zip"

# Configure App Settings
Write-Host "Configuring application settings..." -ForegroundColor Yellow

# Set environment to Production
az webapp config appsettings set --resource-group $ResourceGroupName --name $AppServiceName --settings @"
[
  {
    "name": "ASPNETCORE_ENVIRONMENT",
    "value": "Production"
  },
  {
    "name": "WEBSITES_ENABLE_APP_SERVICE_STORAGE",
    "value": "false"
  },
  {
    "name": "WEBSITES_PORT",
    "value": "4000"
  }
]
"@

Write-Host "✅ Deployment completed!" -ForegroundColor Green
Write-Host "🌐 Your app is available at: https://$AppServiceName.azurewebsites.net" -ForegroundColor Cyan
Write-Host "📚 API Documentation: https://$AppServiceName.azurewebsites.net/api" -ForegroundColor Cyan

# Clean up
Write-Host "Cleaning up temporary files..." -ForegroundColor Yellow
Remove-Item "./publish" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item "./houseiana-backend.zip" -Force -ErrorAction SilentlyContinue

Write-Host "🎉 Deployment script completed!" -ForegroundColor Green

Write-Host @"

Next Steps:
1. Configure your database connection strings in Azure Portal:
   - Go to App Service > Configuration > Application Settings
   - Add your PostgreSQL connection string

2. Set required environment variables:
   - AZURE_POSTGRES_HOST
   - AZURE_POSTGRES_USER
   - AZURE_POSTGRES_PASSWORD
   - JWT_SECRET_KEY

3. Test your API at: https://$AppServiceName.azurewebsites.net/api
"@ -ForegroundColor Cyan