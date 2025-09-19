# 🚀 Push Code to Azure DevOps and Setup CI/CD

## Step 1: Create Azure DevOps Project

1. **Go to Azure DevOps**: https://dev.azure.com
2. **Sign in** with your Microsoft/Azure account
3. **Create a new project**:
   - Project name: `Houseiana-Backend`
   - Visibility: Private
   - Version control: Git
   - Work item process: Agile

## Step 2: Push Code to Azure Repos

### Option A: Using Azure DevOps Web Interface

1. **Go to Repos** in your Azure DevOps project
2. **Click "Import"** at the bottom
3. **Clone URL**: `https://github.com/dev-web3js/.net-backend.git`
4. **Click Import**

### Option B: Using Git Commands

```bash
# Add Azure DevOps as a remote
git remote add azure https://dev.azure.com/YOUR_ORG/Houseiana-Backend/_git/Houseiana-Backend

# Push to Azure DevOps
git push azure master
```

Replace `YOUR_ORG` with your Azure DevOps organization name.

## Step 3: Setup Service Connection

1. **Go to Project Settings** (bottom left)
2. **Service connections** → **New service connection**
3. **Choose Azure Resource Manager** → **Service principal (automatic)**
4. **Scope level**: Subscription
5. **Subscription**: Select your Azure subscription
6. **Resource group**: `Houseiana_group`
7. **Service connection name**: `Azure-ServiceConnection`
8. **Click Save**

## Step 4: Create Build Pipeline

1. **Go to Pipelines** → **Create Pipeline**
2. **Choose "Azure Repos Git"**
3. **Select your repository**: `Houseiana-Backend`
4. **Choose "Existing Azure Pipelines YAML file"**
5. **Path**: `/azure-pipelines.yml`
6. **Click Continue** → **Run**

## Step 5: Configure Pipeline Variables (Security)

1. **Go to Pipelines** → **Your pipeline** → **Edit**
2. **Variables** tab → **New variable**

Add these **secret variables**:

| Name | Value | Keep this value secret |
|------|-------|------------------------|
| `ConnectionStrings.DefaultConnection` | `Host=YOUR_POSTGRES_HOST.postgres.database.azure.com;Port=5432;Database=houseiana;Username=YOUR_POSTGRES_USER;Password=YOUR_POSTGRES_PASSWORD;SSL Mode=Require;Trust Server Certificate=true` | ✅ |
| `JwtSettings.Secret` | `your-super-secret-jwt-key-at-least-64-characters-long-for-production-security` | ✅ |

## Step 6: Update Pipeline for Environment Variables

Edit your `azure-pipelines.yml` to include the secret variables:

```yaml
- task: AzureWebApp@1
  displayName: 'Deploy to Azure App Service'
  inputs:
    azureSubscription: $(azureServiceConnection)
    appType: 'webAppLinux'
    appName: $(webAppName)
    resourceGroupName: $(resourceGroupName)
    package: '$(Pipeline.Workspace)/drop/*.zip'
    runtimeStack: 'DOTNETCORE|8.0'
    appSettings: |
      -ASPNETCORE_ENVIRONMENT Production
      -WEBSITES_ENABLE_APP_SERVICE_STORAGE false
      -WEBSITES_PORT 4000
      -ConnectionStrings__DefaultConnection "$(ConnectionStrings.DefaultConnection)"
      -JwtSettings__Secret "$(JwtSettings.Secret)"
      -JwtSettings__Issuer houseiana-api
      -JwtSettings__Audience houseiana-client
      -JwtSettings__ExpiryInHours 24
```

## Step 7: Enable Continuous Integration

1. **Go to Pipelines** → **Your pipeline** → **Edit**
2. **Triggers** tab
3. **Enable continuous integration**
4. **Branch filters**: `master` and `main`
5. **Save**

## Step 8: Create Release Pipeline (Optional - for Advanced Deployment)

1. **Go to Pipelines** → **Releases** → **New pipeline**
2. **Choose "Azure App Service deployment"**
3. **Artifact source**: Build pipeline
4. **Enable continuous deployment trigger**
5. **Configure deployment to your App Service**

## Step 9: Setup Branch Policies (Recommended)

1. **Go to Repos** → **Branches**
2. **Click "..." on master branch** → **Branch policies**
3. **Enable**:
   - Require minimum number of reviewers
   - Check for linked work items
   - Build validation (select your pipeline)

## 🔄 Continuous Deployment Workflow

Once setup is complete:

1. **Push code** to master/main branch
2. **Pipeline automatically triggers**
3. **Build** → **Test** → **Deploy** to Azure App Service
4. **Your API is live** at: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net

## 📊 Monitor Your Deployments

### Pipeline Status
- **Azure DevOps** → **Pipelines** → View build/deploy status

### Application Logs
- **Azure Portal** → **App Service** → **Log stream**

### Application Insights
- **Azure Portal** → **Application Insights** → **Houseiana**

## 🔧 Troubleshooting

### Common Issues

1. **Service Connection Permissions**
   ```
   Error: Insufficient permissions
   Solution: Ensure service principal has Contributor role on resource group
   ```

2. **Build Failures**
   ```
   Error: Package restore failed
   Solution: Check .NET version compatibility in pipeline
   ```

3. **Deployment Failures**
   ```
   Error: App Service deployment failed
   Solution: Check app settings and connection strings
   ```

### Quick Fixes

1. **Re-run Pipeline**: Pipelines → Your pipeline → Run pipeline
2. **Check Logs**: Pipeline run → View detailed logs
3. **Verify Service Connection**: Project Settings → Service connections → Test connection

## ✅ Success Checklist

- ✅ Azure DevOps project created
- ✅ Code pushed to Azure Repos
- ✅ Service connection configured
- ✅ Build pipeline working
- ✅ Deployment pipeline working
- ✅ Environment variables secured
- ✅ CI/CD triggered on code push
- ✅ App successfully deployed

## 🌐 Your Azure DevOps URLs

- **Project**: https://dev.azure.com/YOUR_ORG/Houseiana-Backend
- **Repos**: https://dev.azure.com/YOUR_ORG/Houseiana-Backend/_git/Houseiana-Backend
- **Pipelines**: https://dev.azure.com/YOUR_ORG/Houseiana-Backend/_build
- **Live API**: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net

## 🎉 Benefits of Azure DevOps CI/CD

- ✅ **Automatic deployments** on code push
- ✅ **Build validation** before deployment
- ✅ **Environment management** (Dev, Staging, Prod)
- ✅ **Rollback capabilities**
- ✅ **Deployment history tracking**
- ✅ **Integrated testing**
- ✅ **Security scanning**

Your Houseiana backend now has enterprise-grade CI/CD! 🚀