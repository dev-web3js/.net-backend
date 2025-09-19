# Database Setup Guide

## Current Configuration
This ASP.NET 8 project uses **PostgreSQL** with Entity Framework Core.

## 🔵 Azure PostgreSQL Integration (Your Setup)

Based on your Azure PostgreSQL Flexible Server template, here's how to connect:

### 1. Get Your Azure PostgreSQL Details
After deploying your Azure template, you'll have:
- **Server Name**: `{your-server-name}.postgres.database.azure.com`
- **Admin Username**: From `administratorLogin` parameter
- **Admin Password**: From `administratorLoginPassword` parameter
- **Database**: `houseiana` (created automatically)

### 2. Quick Setup (Choose One):

**Option A: PowerShell (Windows)**
```powershell
# Edit azure-setup.ps1 with your details, then run:
./azure-setup.ps1
```

**Option B: Bash (Linux/Mac)**
```bash
# Edit azure-setup.sh with your details, then run:
chmod +x azure-setup.sh && ./azure-setup.sh
```

**Option C: Manual Setup**
1. Update `appsettings.json` with your connection details:
```json
"DefaultConnection": "Host=your-server.postgres.database.azure.com;Port=5432;Database=houseiana;Username=your-admin;Password=your-password;SSL Mode=Require;Trust Server Certificate=true"
```

2. Run migrations:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Setup Options

### Option 1: Azure Database for PostgreSQL (Recommended for Production)

1. **Create Azure Database:**
   - Go to [Azure Portal](https://portal.azure.com)
   - Search "Azure Database for PostgreSQL"
   - Create "Flexible Server"
   - Choose your region (Australia East to match your cluster)
   - Select appropriate pricing tier

2. **Configure Connection:**
   - Update `appsettings.json` to use "AzurePostgreSQL" connection string
   - Replace placeholders with your actual values:
   ```json
   "DefaultConnection": "Host=your-server.postgres.database.azure.com;Port=5432;Database=houseiana;Username=your-username;Password=your-password;SSL Mode=Require"
   ```

3. **Update Program.cs** (if using Azure connection):
   ```csharp
   builder.Services.AddDbContext<HouseianaDbContext>(options =>
       options.UseNpgsql(builder.Configuration.GetConnectionString("AzurePostgreSQL")));
   ```

### Option 2: Local PostgreSQL Development

1. **Install PostgreSQL:**
   - Download from [postgresql.org](https://www.postgresql.org/download/)
   - Install with default settings
   - Remember your postgres user password

2. **Create Database:**
   ```sql
   CREATE DATABASE houseiana;
   ```

3. **Update Connection String:**
   - Modify the "DefaultConnection" in `appsettings.json`
   - Use your actual PostgreSQL credentials

### Option 3: Docker PostgreSQL

1. **Run PostgreSQL Container:**
   ```bash
   docker run --name houseiana-postgres -e POSTGRES_DB=houseiana -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres:15
   ```

2. **Connection string remains the same** (localhost configuration)

## Database Migration

After setting up your database, run migrations:

```bash
# Create initial migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

## Environment Variables (Recommended for Production)

Instead of storing credentials in appsettings.json, use environment variables:

```bash
export ConnectionStrings__DefaultConnection="Host=your-server;Database=houseiana;Username=user;Password=pass"
export JwtSettings__Secret="your-secure-jwt-secret"
```

## Security Notes

- Never commit real database credentials to Git
- Use Azure Key Vault for production secrets
- Enable SSL/TLS for database connections
- Use managed identities when possible

## Troubleshooting

1. **Connection Issues:**
   - Verify firewall rules (Azure)
   - Check network connectivity
   - Validate connection string format

2. **Migration Errors:**
   - Ensure database exists
   - Check user permissions
   - Verify Entity Framework tools are installed

## Azure Data Explorer Note

The Azure Data Explorer cluster you mentioned is for analytics, not transactional data. This ASP.NET project requires a relational database (PostgreSQL/SQL Server) for CRUD operations.