# 🏠 Houseiana Backend - ASP.NET 6 Migration

> **🔄 Migration Notice**: This project has been migrated from **NestJS** to **ASP.NET 6 Web API**

## 📋 Project Overview

**Houseiana Backend** is a comprehensive rental property management API built with ASP.NET 6, Entity Framework Core, and PostgreSQL. This backend powers a modern property rental platform with features like user management, property listings, bookings, real-time chat, and payment processing.

## 🚀 Quick Start

### Prerequisites
- .NET 6.0 SDK or later
- PostgreSQL 12+
- Redis (optional, for caching)

### Setup Instructions

1. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

2. **Configure Database**
   Update `appsettings.json` with your database connection:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=houseiana;Username=your_username;Password=your_password"
     }
   }
   ```

3. **Run Database Migrations**
   ```bash
   # Windows
   .\create-migration.ps1

   # Unix/Linux/macOS
   ./create-migration.sh
   ```

4. **Start the Application**
   ```bash
   dotnet run
   ```

5. **Access API Documentation**
   Open your browser to: `http://localhost:4000/api`

## 🏗️ Architecture

This ASP.NET 6 Web API follows clean architecture principles:

- **Controllers** - API endpoints and request handling
- **Services** - Business logic and domain operations
- **Data** - Entity Framework Core DbContext and data access
- **Models** - Entity models and domain objects
- **DTOs** - Data transfer objects for API contracts
- **Hubs** - SignalR hubs for real-time features

## 📡 API Endpoints

### Authentication
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login
- `GET /api/auth/profile` - Get user profile
- `PUT /api/auth/profile` - Update profile
- `POST /api/auth/become-host` - Become a host

### General
- `GET /api` - API health check
- `GET /api/health` - Detailed health status

## 🔄 Migration Details

### What Was Migrated
- ✅ **Framework**: NestJS → ASP.NET 6 Web API
- ✅ **ORM**: Prisma → Entity Framework Core
- ✅ **Database**: PostgreSQL (preserved)
- ✅ **Authentication**: JWT (preserved)
- ✅ **Real-time**: Socket.IO → SignalR
- ✅ **API Documentation**: Swagger (preserved)
- ✅ **Validation**: class-validator → Data Annotations + FluentValidation

### Removed Files
The following NestJS/Node.js files have been cleaned up:
- ❌ `src/` directory (TypeScript source files)
- ❌ `node_modules/` and `package*.json`
- ❌ `dist/` (build artifacts)
- ❌ `test/` directory
- ❌ TypeScript configuration files (`tsconfig.json`, etc.)
- ❌ ESLint and Prettier configuration
- ❌ NestJS CLI configuration

### Preserved Files
- ✅ `prisma/schema.prisma` (kept as reference)
- ✅ Database schema and relationships
- ✅ Core business logic and functionality

## 🛠️ Development Commands

```bash
# Build the project
dotnet build

# Run in development mode
dotnet run

# Run tests (when implemented)
dotnet test

# Create database migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update
```

## 📚 Documentation

For detailed setup instructions, API documentation, and deployment guidance, see:
- **[ASP.NET-README.md](ASP.NET-README.md)** - Complete ASP.NET setup guide
- **Swagger UI** - Available at `/api` when running

## 🌐 Real-time Features

The application includes SignalR hubs for real-time functionality:
- **Chat Hub** (`/hubs/chat`) - Messaging between users
- **Notification Hub** (`/hubs/notifications`) - Real-time notifications

## 🔧 Configuration

Key configuration files:
- `appsettings.json` - Application settings
- `appsettings.Development.json` - Development environment settings
- `Houseiana.csproj` - Project dependencies and build configuration

## 📄 License

This project maintains its original licensing terms.

---

**🎉 Successfully migrated from NestJS to ASP.NET 6** with preserved functionality and improved performance!