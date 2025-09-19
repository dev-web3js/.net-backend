# 🐧 WSL + Docker Setup for Houseiana Development

## 🎯 Quick WSL Installation

### Step 1: Install WSL
Run this command in **PowerShell as Administrator**:

```powershell
wsl --install
```

This will:
- Enable WSL feature
- Install Ubuntu (default)
- Set WSL 2 as default
- Restart required

### Step 2: After Restart
1. **Open Ubuntu** from Start Menu
2. **Create username/password** when prompted
3. **Update packages**:
   ```bash
   sudo apt update && sudo apt upgrade -y
   ```

## 🐳 Docker Desktop Setup

### Install Docker Desktop
1. **Download**: https://www.docker.com/products/docker-desktop/
2. **Install** with WSL 2 backend enabled
3. **Restart** when prompted
4. **Enable WSL integration**: Settings → Resources → WSL Integration

## 🚀 Test Your Houseiana Docker Setup

### Option 1: From WSL Ubuntu
```bash
# Navigate to your project
cd /mnt/c/Users/mosai/OneDrive/Desktop/next\ JS\ project/Houseiana\ by\ java\ script/houseiana-backend/

# Build and run
./docker-deploy.sh --env dev --build --logs
```

### Option 2: From Windows Terminal
```bash
# In your project directory
docker-compose up -d --build

# Check status
docker-compose ps

# View logs
docker-compose logs -f houseiana-api
```

## ✅ Verify Installation

### Check WSL
```powershell
wsl --list --verbose
```

### Check Docker
```bash
docker --version
docker-compose --version
```

### Test Houseiana
```bash
# Start the stack
docker-compose up -d

# Test API
curl http://localhost:4000/api/health
```

## 🌐 Access Your Containerized Houseiana

Once running, your services will be available at:

- **🔗 API**: http://localhost:4000
- **📚 API Docs**: http://localhost:4000/api
- **💚 Health**: http://localhost:4000/api/health
- **🗄️ PostgreSQL**: localhost:5432
- **🔄 Redis**: localhost:6379

## 🔧 Development Workflow

### Start Development Environment
```bash
# Hot reload enabled
docker-compose -f docker-compose.dev.yml up -d

# View live logs
docker-compose -f docker-compose.dev.yml logs -f houseiana-api
```

### Make Code Changes
- Edit files in VS Code
- Docker will automatically rebuild and restart
- See changes instantly at http://localhost:4000

### Database Operations
```bash
# Run migrations
docker-compose exec houseiana-api dotnet ef database update

# Access database
docker-compose exec postgres psql -U postgres -d houseiana
```

## 🎯 Benefits of WSL + Docker

- ✅ **Native Linux environment** for Docker
- ✅ **Better performance** than Docker for Windows
- ✅ **Consistent development** environment
- ✅ **Easy file sharing** between Windows and Linux
- ✅ **Full Linux tools** available

## 🔧 Troubleshooting

### WSL Issues
```powershell
# Check WSL status
wsl --status

# Restart WSL
wsl --shutdown
wsl
```

### Docker Issues
```bash
# Restart Docker Desktop
# Or restart WSL integration in Docker Desktop settings

# Clear Docker cache
docker system prune -a
```

### Port Conflicts
```bash
# Check what's using port 4000
netstat -tulpn | grep :4000

# Stop Houseiana stack
docker-compose down
```

## 🚀 Quick Commands Reference

```bash
# Start everything
docker-compose up -d

# Stop everything
docker-compose down

# Rebuild and start
docker-compose up -d --build

# View all logs
docker-compose logs -f

# Shell into API container
docker-compose exec houseiana-api bash

# Run migrations
docker-compose exec houseiana-api dotnet ef database update
```

## ⚡ Next Steps

1. **Install WSL**: `wsl --install` (as Administrator)
2. **Install Docker Desktop** with WSL backend
3. **Test setup**: `docker-compose up -d`
4. **Access API**: http://localhost:4000
5. **Start developing!**

Your Houseiana backend will run perfectly in containerized environment! 🎉