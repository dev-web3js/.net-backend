#!/bin/bash

# Azure Deployment Script for Houseiana Backend
# This script deploys the ASP.NET 8 application to Azure App Service

# Configuration
RESOURCE_GROUP_NAME="Houseiana_group"
APP_SERVICE_NAME="Houseiana"
SUBSCRIPTION_ID="aa935f6b-dd8b-4746-98e0-b60637c451a1"

echo "🚀 Starting deployment to Azure App Service..."

# Check if Azure CLI is installed
if ! command -v az &> /dev/null; then
    echo "❌ Azure CLI is not installed. Please install it first."
    echo "Visit: https://docs.microsoft.com/en-us/cli/azure/install-azure-cli"
    exit 1
fi

# Login to Azure (if not already logged in)
echo "🔐 Checking Azure login status..."
if ! az account show &> /dev/null; then
    echo "Please login to Azure..."
    az login
fi

# Set subscription
echo "📋 Setting subscription to: $SUBSCRIPTION_ID"
az account set --subscription $SUBSCRIPTION_ID

# Build and publish the application
echo "🔨 Building and publishing the application..."
dotnet publish -c Release -o ./publish

if [ $? -ne 0 ]; then
    echo "❌ Build failed. Please fix the errors and try again."
    exit 1
fi

# Create deployment package
echo "📦 Creating deployment package..."
cd publish
zip -r ../houseiana-backend.zip .
cd ..

# Deploy to Azure App Service
echo "🚀 Deploying to Azure App Service: $APP_SERVICE_NAME"
az webapp deployment source config-zip \
    --resource-group $RESOURCE_GROUP_NAME \
    --name $APP_SERVICE_NAME \
    --src ./houseiana-backend.zip

if [ $? -ne 0 ]; then
    echo "❌ Deployment failed. Please check the error messages above."
    exit 1
fi

# Configure App Settings
echo "⚙️ Configuring application settings..."
az webapp config appsettings set \
    --resource-group $RESOURCE_GROUP_NAME \
    --name $APP_SERVICE_NAME \
    --settings \
        ASPNETCORE_ENVIRONMENT=Production \
        WEBSITES_ENABLE_APP_SERVICE_STORAGE=false \
        WEBSITES_PORT=4000

echo "✅ Deployment completed!"
echo "🌐 Your app is available at: https://$APP_SERVICE_NAME.azurewebsites.net"
echo "📚 API Documentation: https://$APP_SERVICE_NAME.azurewebsites.net/api"

# Clean up
echo "🧹 Cleaning up temporary files..."
rm -rf ./publish
rm -f ./houseiana-backend.zip

echo "🎉 Deployment script completed!"

cat << EOF

📋 Next Steps:
1. Configure your database connection strings in Azure Portal:
   - Go to App Service > Configuration > Application Settings
   - Add your PostgreSQL connection string

2. Set required environment variables:
   - AZURE_POSTGRES_HOST
   - AZURE_POSTGRES_USER
   - AZURE_POSTGRES_PASSWORD
   - JWT_SECRET_KEY

3. Test your API at: https://$APP_SERVICE_NAME.azurewebsites.net/api

4. View logs: az webapp log tail --name $APP_SERVICE_NAME --resource-group $RESOURCE_GROUP_NAME

EOF