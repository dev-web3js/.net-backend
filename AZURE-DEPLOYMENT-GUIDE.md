# Azure Deployment Guide - Houseiana Backend

## 🚀 Your Azure App Service Details
- **App Name**: Houseiana
- **URL**: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net
- **Resource Group**: Houseiana_group
- **Location**: South Africa North
- **Runtime**: .NET 8 on Linux
- **SKU**: Premium0V3 (P0v3)

## 📦 Deployment Options

### Option 1: Manual Deployment via Azure Portal

1. **Create Deployment Package**
   ```bash
   # Already created in ./publish folder
   # Zip the contents for upload
   ```

2. **Deploy via Azure Portal**
   - Go to [Azure Portal](https://portal.azure.com)
   - Navigate to your App Service "Houseiana"
   - Go to "Deployment Center"
   - Choose "ZIP Deploy"
   - Upload the publish folder as a ZIP file

### Option 2: Using Azure CLI

```bash
# Install Azure CLI if not installed
# Login to Azure
az login

# Set subscription
az account set --subscription aa935f6b-dd8b-4746-98e0-b60637c451a1

# Create deployment package
cd publish
zip -r ../houseiana-backend.zip .
cd ..

# Deploy to Azure
az webapp deployment source config-zip \
    --resource-group Houseiana_group \
    --name Houseiana \
    --src ./houseiana-backend.zip
```

### Option 3: GitHub Actions (Automated)

The `.github/workflows/azure-deploy.yml` file is ready for CI/CD:

1. **Setup GitHub Secrets**
   - Go to your GitHub repository settings
   - Add secret: `AZUREAPPSERVICE_PUBLISHPROFILE`
   - Get publish profile from Azure Portal > App Service > Get publish profile

2. **Push to GitHub**
   ```bash
   git add .
   git commit -m "Add Azure deployment configuration"
   git push origin master
   ```

## ⚙️ Required Configuration

### 1. Environment Variables in Azure

Go to Azure Portal > App Service > Configuration > Application Settings and add:

```json
{
  "ASPNETCORE_ENVIRONMENT": "Production",
  "WEBSITES_ENABLE_APP_SERVICE_STORAGE": "false",
  "WEBSITES_PORT": "4000",
  "ConnectionStrings__DefaultConnection": "Host={YOUR_POSTGRES_HOST};Port=5432;Database=houseiana;Username={YOUR_POSTGRES_USER};Password={YOUR_POSTGRES_PASSWORD};SSL Mode=Require;Trust Server Certificate=true",
  "JwtSettings__Secret": "{GENERATE_SECURE_JWT_SECRET_KEY}",
  "JwtSettings__Issuer": "houseiana-api",
  "JwtSettings__Audience": "houseiana-client",
  "JwtSettings__ExpiryInHours": "24"
}
```

### 2. Database Configuration

Connect your Azure PostgreSQL:
- Update the ConnectionString with your actual PostgreSQL details
- Ensure firewall rules allow Azure services
- Run database migrations (see below)

### 3. Database Migrations

After deployment, run migrations:

```bash
# Option A: Using Azure Cloud Shell
az webapp ssh --name Houseiana --resource-group Houseiana_group
cd site/wwwroot
dotnet ef database update

# Option B: Local connection (with Azure PostgreSQL connection string)
dotnet ef database update --connection "Host=your-postgres.postgres.database.azure.com;Port=5432;Database=houseiana;Username=user;Password=pass;SSL Mode=Require"
```

## 🔧 Post-Deployment Setup

### 1. Verify Deployment
- Visit: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net
- Check health: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net/api/health
- API docs: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net/api

### 2. Configure Custom Domain (Optional)
- Go to Azure Portal > App Service > Custom domains
- Add your custom domain
- Configure SSL certificate

### 3. Enable Application Insights
Already configured for monitoring:
- Application Insights: "Houseiana"
- Region: South Africa North

## 🔒 Security Checklist

- ✅ HTTPS Only enabled
- ✅ FTP disabled
- ✅ SCM basic auth disabled
- ⚠️ Set strong JWT secret key
- ⚠️ Configure database firewall rules
- ⚠️ Set up proper CORS origins

## 📊 Monitoring & Logs

### View Logs
```bash
# Real-time logs
az webapp log tail --name Houseiana --resource-group Houseiana_group

# Download logs
az webapp log download --name Houseiana --resource-group Houseiana_group
```

### Application Insights
- Monitor performance and errors
- View at: Azure Portal > Application Insights > Houseiana

## 🚨 Troubleshooting

### Common Issues

1. **App won't start**
   - Check application logs
   - Verify environment variables
   - Ensure correct runtime version

2. **Database connection errors**
   - Verify connection string
   - Check PostgreSQL firewall rules
   - Ensure SSL is configured

3. **502 Bad Gateway**
   - Check if app is listening on correct port (4000)
   - Verify WEBSITES_PORT environment variable

### Quick Diagnostics
```bash
# Check app status
az webapp show --name Houseiana --resource-group Houseiana_group --query state

# Restart app
az webapp restart --name Houseiana --resource-group Houseiana_group
```

## 📞 Support

If you encounter issues:
1. Check Azure Portal logs
2. Verify all environment variables
3. Test database connectivity
4. Review Application Insights for errors

## 🎉 Success!

Once deployed successfully, your Houseiana API will be available at:
**https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net**