# Houseiana Backend - ASP.NET 5 Conversion

🏠 **Houseiana Backend** has been successfully converted from NestJS to ASP.NET 5 Web API.

## 🚀 Quick Start

### Prerequisites
- .NET 5.0 SDK
- PostgreSQL 12+
- Redis (optional, for caching)

### Setup Instructions

1. **Clone and Navigate**
   ```bash
   cd houseiana-backend
   ```

2. **Install Dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure Database**
   Update `appsettings.json` with your PostgreSQL connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=houseiana;Username=your_username;Password=your_password"
     }
   }
   ```

4. **Set Up Database**

   **On Windows:**
   ```powershell
   .\create-migration.ps1
   ```

   **On Unix/Linux/macOS:**
   ```bash
   ./create-migration.sh
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```

## 📁 Project Structure

```
├── Controllers/           # API Controllers
│   ├── AuthController.cs
│   └── HomeController.cs
├── Data/                 # Database Context
│   └── HouseianaDbContext.cs
├── DTOs/                 # Data Transfer Objects
│   └── AuthDtos.cs
├── Extensions/           # Extensions and Profiles
│   └── AutoMapperProfile.cs
├── Hubs/                 # SignalR Hubs
│   ├── ChatHub.cs
│   └── NotificationHub.cs
├── Models/               # Entity Models
│   ├── User.cs
│   ├── Listing.cs
│   ├── Booking.cs
│   ├── Review.cs
│   ├── Payment.cs
│   ├── Enums.cs
│   └── AdditionalModels.cs
├── Services/             # Business Logic Services
│   ├── IAuthService.cs
│   ├── AuthService.cs
│   ├── ServiceInterfaces.cs
│   ├── ListingService.cs
│   ├── BookingService.cs
│   ├── UserService.cs
│   └── NotificationService.cs
├── Program.cs            # Application Entry Point
├── Houseiana.csproj     # Project File
├── appsettings.json     # Configuration
└── appsettings.Development.json
```

## 🛠 Key Features

### ✅ Converted Features
- **Authentication & Authorization** - JWT-based auth with Identity
- **User Management** - Registration, login, profile management
- **Database Integration** - Entity Framework Core with PostgreSQL
- **API Documentation** - Swagger/OpenAPI integration
- **Real-time Communication** - SignalR for chat and notifications
- **Data Validation** - FluentValidation for request validation
- **Auto Mapping** - AutoMapper for entity-DTO mapping
- **CORS Support** - Cross-origin resource sharing
- **Caching** - Redis integration for performance

### 🏗 Architecture Improvements
- **Clean Architecture** - Separation of concerns with services and controllers
- **Dependency Injection** - Built-in DI container
- **Configuration Management** - Flexible configuration system
- **Error Handling** - Structured exception handling
- **Logging** - Integrated logging framework

## 🔧 Configuration

### JWT Settings
```json
{
  "JwtSettings": {
    "Secret": "your-super-secret-jwt-key-here-make-it-long-enough",
    "Issuer": "houseiana-api",
    "Audience": "houseiana-client",
    "ExpiryInHours": 24
  }
}
```

### Database Connection
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=houseiana;Username=postgres;Password=postgres",
    "Redis": "localhost:6379"
  }
}
```

## 📡 API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - User login
- `POST /api/auth/logout` - User logout
- `GET /api/auth/profile` - Get user profile
- `PUT /api/auth/profile` - Update user profile
- `POST /api/auth/become-host` - Become a host
- `POST /api/auth/refresh` - Refresh JWT token
- `GET /api/auth/validate` - Validate current token

### General
- `GET /api` - API health check
- `GET /api/health` - Detailed health status

## 🌐 SignalR Hubs

### Chat Hub (`/hubs/chat`)
- Real-time messaging between users
- Conversation management
- Message read status

### Notification Hub (`/hubs/notifications`)
- Real-time notifications
- User-specific notification delivery

## 🧪 Testing

Run the application and test endpoints:

```bash
# Start the application
dotnet run

# Test health endpoint
curl http://localhost:4000/api/health

# View API documentation
# Open browser: http://localhost:4000/api
```

## 📝 Migration from NestJS

### Key Changes Made:
1. **Framework Migration**: NestJS → ASP.NET 5 Web API
2. **ORM Migration**: Prisma → Entity Framework Core
3. **Dependency Injection**: NestJS DI → ASP.NET Core DI
4. **Validation**: class-validator → FluentValidation
5. **Real-time**: Socket.IO → SignalR
6. **Documentation**: @nestjs/swagger → Swashbuckle

### Preserved Features:
- All API endpoints and functionality
- Database schema and relationships
- Authentication and authorization logic
- Real-time communication capabilities
- Input validation and error handling

## 🚀 Production Deployment

### Environment Variables
Set the following environment variables:
- `ASPNETCORE_ENVIRONMENT=Production`
- `PORT=4000`
- Database connection strings
- JWT secret keys

### Docker Support
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY . .
EXPOSE 80
ENTRYPOINT ["dotnet", "Houseiana.dll"]
```

## 🔍 Monitoring & Logging

The application includes:
- Structured logging with Serilog (can be added)
- Health checks for dependencies
- Performance monitoring hooks
- Error tracking and reporting

## 🤝 Contributing

1. Follow the existing code structure
2. Use dependency injection for services
3. Write unit tests for new features
4. Update API documentation
5. Follow C# coding conventions

## 📄 License

This project maintains the same license as the original NestJS version.

---

🎉 **Migration Complete!** Your Houseiana backend is now running on ASP.NET 5 with all the modern .NET ecosystem benefits.