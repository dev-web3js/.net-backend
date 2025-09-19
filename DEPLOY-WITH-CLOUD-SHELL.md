# 🚀 Deploy Houseiana Backend Using Azure Cloud Shell

## Method 1: Azure Cloud Shell Deployment (FASTEST - No Installation Required)

### Step 1: Access Azure Cloud Shell
1. Go to **Azure Portal**: https://portal.azure.com
2. Click the **Cloud Shell** icon (>_) in the top toolbar
3. Choose **PowerShell** when prompted
4. Wait for Cloud Shell to initialize

### Step 2: Upload Deployment Package
1. In Cloud Shell, click the **Upload/Download** icon
2. Select **Upload**
3. Choose your `houseiana-deployment.zip` file
4. Wait for upload to complete

### Step 3: Deploy to Your App Service
Copy and paste these commands one by one in Cloud Shell:

```bash
# Extract the deployment package
unzip houseiana-deployment.zip -d houseiana-app

# Deploy to your App Service
az webapp deployment source config-zip \
    --resource-group Houseiana_group \
    --name Houseiana \
    --src houseiana-deployment.zip
```

### Step 4: Configure Environment Variables
Run this command in Cloud Shell:

```bash
az webapp config appsettings set \
    --resource-group Houseiana_group \
    --name Houseiana \
    --settings \
        ASPNETCORE_ENVIRONMENT=Production \
        WEBSITES_ENABLE_APP_SERVICE_STORAGE=false \
        WEBSITES_PORT=4000 \
        "ConnectionStrings__DefaultConnection=Host=YOUR_POSTGRES_HOST.postgres.database.azure.com;Port=5432;Database=houseiana;Username=YOUR_POSTGRES_USER;Password=YOUR_POSTGRES_PASSWORD;SSL Mode=Require;Trust Server Certificate=true" \
        "JwtSettings__Secret=your-super-secret-jwt-key-make-it-very-long-and-secure-at-least-64-characters-long" \
        JwtSettings__Issuer=houseiana-api \
        JwtSettings__Audience=houseiana-client \
        JwtSettings__ExpiryInHours=24
```

**Replace these values:**
- `YOUR_POSTGRES_HOST` - Your Azure PostgreSQL server name
- `YOUR_POSTGRES_USER` - Your PostgreSQL admin username
- `YOUR_POSTGRES_PASSWORD` - Your PostgreSQL admin password

### Step 5: Restart the App Service
```bash
az webapp restart --resource-group Houseiana_group --name Houseiana
```

### Step 6: Verify Deployment
```bash
# Check app status
az webapp show --resource-group Houseiana_group --name Houseiana --query state

# Test the API
curl https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net/api/health
```

---

## Method 2: Manual Azure Portal Deployment

### Step 1: Upload via Azure Portal
1. Go to **Azure Portal** → Search "Houseiana" → Click your App Service
2. Go to **Deployment Center** → **ZIP Deploy**
3. Upload `houseiana-deployment.zip`
4. Click **Deploy** and wait 2-3 minutes

### Step 2: Configure App Settings
1. Go to **Configuration** → **Application settings**
2. Add these settings:

| Name | Value |
|------|-------|
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `WEBSITES_ENABLE_APP_SERVICE_STORAGE` | `false` |
| `WEBSITES_PORT` | `4000` |
| `ConnectionStrings__DefaultConnection` | `Host=YOUR_POSTGRES_HOST.postgres.database.azure.com;Port=5432;Database=houseiana;Username=YOUR_POSTGRES_USER;Password=YOUR_POSTGRES_PASSWORD;SSL Mode=Require;Trust Server Certificate=true` |
| `JwtSettings__Secret` | `your-super-secret-jwt-key-make-it-very-long-and-secure` |
| `JwtSettings__Issuer` | `houseiana-api` |
| `JwtSettings__Audience` | `houseiana-client` |
| `JwtSettings__ExpiryInHours` | `24` |

3. Click **Save** → **Continue**

### Step 3: Restart App Service
- Go to **Overview** → Click **Restart**

---

## 🎯 After Deployment - Test Your API

### Health Check
Visit: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net/api/health

Expected response:
```json
{
  "status": "healthy",
  "timestamp": "2024-01-20T10:30:00Z",
  "uptime": 12345
}
```

### API Documentation
Visit: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net/api

You should see the Swagger UI with all your API endpoints.

### Main API Endpoint
Visit: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net

Expected response:
```json
{
  "message": "🚀 Houseiana API is running!",
  "version": "1.0.0",
  "timestamp": "2024-01-20T10:30:00Z",
  "environment": "Production"
}
```

---

## 🗄️ Database Setup (After App is Running)

### Create Database Tables
Use Azure Cloud Shell:

```bash
# SSH into your app container
az webapp ssh --resource-group Houseiana_group --name Houseiana

# Once inside the container:
cd site/wwwroot
dotnet ef database update
```

OR use local connection:
```bash
dotnet ef database update --connection "Host=your-postgres-server.postgres.database.azure.com;Port=5432;Database=houseiana;Username=your-admin;Password=your-password;SSL Mode=Require"
```

---

## 🔧 Troubleshooting

### App Not Starting
```bash
# Check logs in Cloud Shell
az webapp log tail --resource-group Houseiana_group --name Houseiana
```

### Common Issues
1. **Wrong environment variables** - Double-check all values
2. **Database connection** - Verify PostgreSQL firewall allows Azure
3. **Port issues** - Ensure WEBSITES_PORT=4000

### Quick Fixes
```bash
# Restart app
az webapp restart --resource-group Houseiana_group --name Houseiana

# Check app settings
az webapp config appsettings list --resource-group Houseiana_group --name Houseiana
```

---

## ✅ Success Checklist

- ✅ App deployed to Azure
- ✅ Environment variables configured
- ✅ App is running (health check passes)
- ✅ API documentation accessible
- ✅ Database tables created

## 🌐 Your Live URLs

- **Main API**: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net
- **API Docs**: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net/api
- **Health Check**: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net/api/health

🎉 **Your Houseiana backend is now live on Azure!**