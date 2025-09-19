# 🚀 Push Your Code to Azure DevOps Repos RIGHT NOW

## Quick Setup (5 Minutes)

### Step 1: Create Azure DevOps Project
1. **Go to**: https://dev.azure.com
2. **Sign in** with your Microsoft account
3. **Create new project**:
   - Name: `Houseiana-Backend`
   - Visibility: Private
   - Click **Create**

### Step 2: Get Your Azure Repos URL
After creating the project, you'll see:
```
https://dev.azure.com/YOUR_ORGANIZATION_NAME/Houseiana-Backend/_git/Houseiana-Backend
```

### Step 3: Push Code to Azure DevOps
Run these commands in your project directory:

```bash
# Add Azure DevOps as remote
git remote add azure https://dev.azure.com/YOUR_ORGANIZATION_NAME/Houseiana-Backend/_git/Houseiana-Backend

# Push to Azure DevOps
git push azure master
```

**Replace `YOUR_ORGANIZATION_NAME`** with your actual Azure DevOps organization name.

### Step 4: Setup Automatic Deployment
1. **Go to Pipelines** in Azure DevOps
2. **Create Pipeline** → **Azure Repos Git**
3. **Select your repo** → **Existing Azure Pipelines YAML file**
4. **Choose** `/azure-pipelines.yml`
5. **Run the pipeline**

## Alternative: Import from GitHub

If you want to keep GitHub as primary and sync to Azure DevOps:

1. **Azure DevOps** → **Repos** → **Import repository**
2. **Source URL**: `https://github.com/dev-web3js/.net-backend.git`
3. **Click Import**

## What Happens Next

✅ **Code pushed** to Azure DevOps
✅ **Pipeline automatically runs**
✅ **Builds your .NET 8 app**
✅ **Deploys to your Azure App Service**
✅ **Your API goes live**

## Your URLs After Setup

- **Azure DevOps Project**: https://dev.azure.com/YOUR_ORG/Houseiana-Backend
- **Source Code**: https://dev.azure.com/YOUR_ORG/Houseiana-Backend/_git/Houseiana-Backend
- **Pipeline**: https://dev.azure.com/YOUR_ORG/Houseiana-Backend/_build
- **Live API**: https://houseiana-aga7fncfejgtdxgn.southafricanorth-01.azurewebsites.net

## Benefits of Azure DevOps Integration

🔄 **Automatic deployment** when you push code
🔒 **Secure environment variables** management
📊 **Build and deployment history**
🧪 **Automated testing** on every commit
🚀 **One-click rollbacks** if needed
📈 **Performance monitoring** integration

## Need Help?

Check `AZURE-DEVOPS-SETUP.md` for detailed step-by-step instructions with screenshots and troubleshooting.

🎉 **Ready to push your code to Azure DevOps and get automatic deployments!**