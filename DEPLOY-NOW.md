# 🚀 Deploy Houseiana Backend to Azure NOW

## Step 1: Deploy the Application

### Option A: Azure Portal (Recommended - Easiest)

1. **Go to Azure Portal**
   - Visit: https://portal.azure.com
   - Sign in with your Azure account

2. **Navigate to Your App Service**
   - Search for "Houseiana" in the top search bar
   - Click on your App Service "Houseiana"

3. **Deploy via ZIP**
   - In the left menu, click "Deployment Center"
   - Click "ZIP Deploy" tab
   - Click "Browse" and select the file: `houseiana-deployment.zip`
   - Click "Deploy"
   - Wait for deployment to complete (2-3 minutes)

### Option B: Using Azure CLI (If you have it installed)

```bash
# Install Azure CLI first if not installed
# Then run these commands:

# Login to Azure
az login

# Set subscription
az account set --subscription aa935f6b-dd8b-4746-98e0-b60637c451a1

# Deploy the ZIP file
az webapp deployment source config-zip \
    --resource-group Houseiana_group \
    --name Houseiana \
    --src ./houseiana-deployment.zip
```

## Step 2: Configure Environment Variables

1. **Go to App Service Configuration**
   - In Azure Portal, go to your App Service "Houseiana"
   - Click "Configuration" in the left menu
   - Click "Application settings" tab

2. **Add These Settings** (Click "New application setting" for each):

   ```
   Name: ASPNETCORE_ENVIRONMENT
   Value: Production

   Name: WEBSITES_ENABLE_APP_SERVICE_STORAGE
   Value: false

   Name: WEBSITES_PORT
   Value: 4000

   Name: ConnectionStrings__DefaultConnection
   Value: Host=YOUR_POSTGRES_HOST;Port=5432;Database=houseiana;Username=YOUR_POSTGRES_USER;Password=YOUR_POSTGRES_PASSWORD;SSL Mode=Require;Trust Server Certificate=true

   Name: JwtSettings__Secret
   Value: your-super-secret-jwt-key-make-it-very-long-and-secure-at-least-64-characters-long

   Name: JwtSettings__Issuer
   Value: houseiana-api

   Name: JwtSettings__Audience
   Value: houseiana-client

   Name: JwtSettings__ExpiryInHours
   Value: 24
   ```

3. **Save Configuration**
   - Click "Save" at the top
   - Click "Continue" when prompted
   - Wait for restart (30 seconds)

## Step 3: Set Up Database Connection

Replace the PostgreSQL connection values with your actual Azure PostgreSQL details:

```
YOUR_POSTGRES_HOST = your-postgres-server.postgres.database.azure.com
YOUR_POSTGRES_USER = your-admin-username
YOUR_POSTGRES_PASSWORD = your-admin-password
```

## Step 4: Verify Deployment

1. **Check if App is Running**
   - Visit: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net
   - You should see: `{"message":"🚀 Houseiana API is running!","version":"1.0.0"}`

2. **Check API Documentation**
   - Visit: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net/api
   - You should see Swagger UI with all API endpoints

3. **Test Health Endpoint**
   - Visit: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net/api/health
   - You should see: `{"status":"healthy","timestamp":"...","uptime":...}`

## Step 5: Run Database Migrations

After deployment, you need to create the database tables:

### Option A: Use Azure Cloud Shell
1. Go to Azure Portal
2. Click the Cloud Shell icon (>_) at the top
3. Choose "PowerShell"
4. Run these commands:

```bash
# Navigate to your app
az webapp ssh --name Houseiana --resource-group Houseiana_group

# Once connected to the app container:
cd site/wwwroot
dotnet ef database update
```

### Option B: Use Local EF Tools (with Azure PostgreSQL connection)
```bash
# Run this from your local machine with your Azure PostgreSQL connection string
dotnet ef database update --connection "Host=your-postgres-server.postgres.database.azure.com;Port=5432;Database=houseiana;Username=your-admin;Password=your-password;SSL Mode=Require"
```

## 🎉 Success Checklist

- ✅ App deployed to Azure App Service
- ✅ Environment variables configured
- ✅ Database connection configured
- ✅ Database migrations run
- ✅ API is accessible and responding
- ✅ Swagger documentation is available

## 🔧 Troubleshooting

### If the app doesn't start:

1. **Check Logs**
   - Azure Portal > App Service > Log stream
   - Look for error messages

2. **Common Issues:**
   - Missing environment variables
   - Wrong database connection string
   - Port configuration (should be 4000)

3. **Quick Fixes:**
   - Restart the app: Azure Portal > App Service > Restart
   - Check all environment variables are set correctly
   - Verify PostgreSQL firewall allows Azure connections

## 📞 Need Help?

If you encounter issues:
1. Check the app logs in Azure Portal
2. Verify all environment variables are set
3. Test database connectivity
4. Ensure PostgreSQL allows connections from Azure

## 🌐 Your Live URLs

Once deployed successfully:

- **Main API**: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net
- **API Docs**: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net/api
- **Health Check**: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net/api/health

Your Houseiana backend will be live and ready to use! 🎉