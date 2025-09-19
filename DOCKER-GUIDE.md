# 🐳 Docker Deployment Guide - Houseiana Backend

## 🎯 Overview

Your Houseiana backend is now fully containerized with Docker! This provides:

- ✅ **Consistent environment** across development and production
- ✅ **Easy deployment** with single command
- ✅ **Complete stack** (API + PostgreSQL + Redis + Nginx)
- ✅ **Scalability** and orchestration ready
- ✅ **Development** and **Production** configurations

## 🚀 Quick Start

### Option 1: One-Command Deployment

```bash
# Production deployment
./docker-deploy.sh

# Development deployment
./docker-deploy.sh --env dev --build --logs
```

### Option 2: Manual Docker Compose

```bash
# Production
docker-compose up -d

# Development
docker-compose -f docker-compose.dev.yml up -d
```

## 📦 What's Included

### 🔧 **Services**

1. **houseiana-api** - Your ASP.NET 8 backend
   - Port: 4000
   - Health checks enabled
   - Multi-stage optimized build

2. **postgres** - PostgreSQL 15 database
   - Port: 5432
   - Persistent data storage
   - Health checks enabled

3. **redis** - Redis cache
   - Port: 6379
   - Persistent data storage

4. **nginx** - Reverse proxy (optional)
   - Port: 80/443
   - Load balancing
   - SSL termination ready

### 📁 **Docker Files**

- `Dockerfile` - Multi-stage .NET 8 build
- `docker-compose.yml` - Production stack
- `docker-compose.dev.yml` - Development stack
- `.dockerignore` - Build optimization
- `nginx/nginx.conf` - Reverse proxy config
- `init-scripts/` - Database initialization

## 🔧 Usage Commands

### Deployment Scripts

```bash
# PowerShell (Windows)
./docker-deploy.ps1 -Environment prod -Build -Fresh

# Bash (Linux/Mac)
./docker-deploy.sh --env prod --build --fresh --logs
```

**Script Options:**
- `--env dev|prod` - Environment configuration
- `--build` - Rebuild Docker images
- `--fresh` - Clean start (removes volumes)
- `--logs` - Show application logs

### Docker Compose Commands

```bash
# Start services
docker-compose up -d

# Stop services
docker-compose down

# View logs
docker-compose logs -f houseiana-api

# Restart single service
docker-compose restart houseiana-api

# Scale API instances
docker-compose up -d --scale houseiana-api=3

# Execute commands in container
docker-compose exec houseiana-api bash
```

### Database Operations

```bash
# Run migrations
docker-compose exec houseiana-api dotnet ef database update

# Create new migration
docker-compose exec houseiana-api dotnet ef migrations add MigrationName

# Database shell
docker-compose exec postgres psql -U postgres -d houseiana
```

## 🌐 Access Your Application

After deployment, your services are available at:

- **🔗 API**: http://localhost:4000
- **📚 API Docs**: http://localhost:4000/api
- **💚 Health**: http://localhost:4000/api/health
- **🗄️ Database**: localhost:5432
- **🔄 Redis**: localhost:6379
- **🌐 Nginx**: http://localhost (if enabled)

## ⚙️ Environment Variables

### Production (docker-compose.yml)

```yaml
environment:
  - ASPNETCORE_ENVIRONMENT=Production
  - ConnectionStrings__DefaultConnection=Host=postgres;Port=5432;Database=houseiana;Username=postgres;Password=houseiana123;
  - ConnectionStrings__Redis=redis:6379
  - JwtSettings__Secret=your-super-secret-jwt-key-for-production
```

### Development (docker-compose.dev.yml)

```yaml
environment:
  - ASPNETCORE_ENVIRONMENT=Development
  - # Same as above but with development values
```

## 🔒 Security Configuration

### For Production:

1. **Change default passwords**:
   ```yaml
   environment:
     POSTGRES_PASSWORD: your-secure-password
   ```

2. **Use Docker secrets**:
   ```yaml
   secrets:
     - postgres_password
     - jwt_secret
   ```

3. **Enable SSL in Nginx**:
   - Uncomment SSL section in `nginx/nginx.conf`
   - Add SSL certificates to `nginx/ssl/`

## 📊 Monitoring & Logging

### View Service Status
```bash
docker-compose ps
docker stats
```

### Application Logs
```bash
# All services
docker-compose logs -f

# Specific service
docker-compose logs -f houseiana-api

# Last 100 lines
docker-compose logs --tail=100 houseiana-api
```

### Health Checks
```bash
# Check health endpoints
curl http://localhost:4000/api/health

# Docker health status
docker inspect houseiana-backend --format='{{.State.Health.Status}}'
```

## 🔄 CI/CD Integration

### Azure DevOps Pipeline

Add to your `azure-pipelines.yml`:

```yaml
- task: Docker@2
  displayName: 'Build Docker Image'
  inputs:
    command: 'buildAndPush'
    repository: 'houseiana/backend'
    dockerfile: '**/Dockerfile'
    tags: '$(Build.BuildId)'

- task: DockerCompose@0
  displayName: 'Deploy with Docker Compose'
  inputs:
    dockerComposeFile: 'docker-compose.yml'
    action: 'Run services'
```

### GitHub Actions

```yaml
- name: Build and Deploy
  run: |
    docker-compose build
    docker-compose up -d
```

## 🚀 Production Deployment Options

### Option 1: Azure Container Instances

```bash
# Create container group
az container create \
  --resource-group houseiana-rg \
  --name houseiana-backend \
  --image your-registry/houseiana:latest \
  --ports 4000
```

### Option 2: Azure Container Apps

```bash
# Deploy to Container Apps
az containerapp create \
  --name houseiana-backend \
  --resource-group houseiana-rg \
  --image your-registry/houseiana:latest
```

### Option 3: Kubernetes

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: houseiana-backend
spec:
  replicas: 3
  selector:
    matchLabels:
      app: houseiana-backend
  template:
    spec:
      containers:
      - name: houseiana-backend
        image: houseiana/backend:latest
        ports:
        - containerPort: 4000
```

## 🔧 Troubleshooting

### Common Issues

1. **Port already in use**:
   ```bash
   # Check what's using the port
   netstat -tulpn | grep :4000

   # Stop conflicting services
   docker-compose down
   ```

2. **Database connection issues**:
   ```bash
   # Check PostgreSQL logs
   docker-compose logs postgres

   # Test connection
   docker-compose exec postgres pg_isready -U postgres
   ```

3. **Build failures**:
   ```bash
   # Clear Docker cache
   docker system prune -a

   # Rebuild without cache
   docker-compose build --no-cache
   ```

### Performance Tuning

```yaml
# Add resource limits
services:
  houseiana-api:
    deploy:
      resources:
        limits:
          memory: 512M
        reservations:
          memory: 256M
```

## ✅ Benefits of Docker Deployment

- 🔄 **Consistent environments** (dev = prod)
- 📦 **Easy deployment** (single command)
- 🔧 **Service isolation** and security
- 📈 **Horizontal scaling** ready
- 🚀 **Fast startup** times
- 🔄 **Easy rollbacks** and updates
- 📊 **Built-in monitoring** and health checks
- 🌐 **Cloud deployment** ready

## 🎉 Next Steps

1. **Test locally**: Run `./docker-deploy.sh --env dev --build`
2. **Configure production**: Update environment variables
3. **Setup CI/CD**: Integrate with your pipeline
4. **Deploy to cloud**: Choose Azure Container Apps/ACI
5. **Monitor**: Setup logging and alerting

Your Houseiana backend is now fully containerized and ready for modern deployment! 🚀