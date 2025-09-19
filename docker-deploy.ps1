# Docker Deployment Script for Houseiana Backend
# Builds and deploys the full stack using Docker Compose

param(
    [Parameter(Mandatory=$false)]
    [ValidateSet("dev", "prod", "production")]
    [string]$Environment = "prod",

    [Parameter(Mandatory=$false)]
    [switch]$Build = $false,

    [Parameter(Mandatory=$false)]
    [switch]$Fresh = $false,

    [Parameter(Mandatory=$false)]
    [switch]$Logs = $false
)

Write-Host "🐳 Houseiana Docker Deployment Script" -ForegroundColor Green
Write-Host "Environment: $Environment" -ForegroundColor Yellow

# Determine compose file
if ($Environment -eq "dev") {
    $ComposeFile = "docker-compose.dev.yml"
    Write-Host "📦 Using development configuration" -ForegroundColor Cyan
} else {
    $ComposeFile = "docker-compose.yml"
    Write-Host "🚀 Using production configuration" -ForegroundColor Cyan
}

# Fresh start - remove all containers and volumes
if ($Fresh) {
    Write-Host "🧹 Cleaning up existing containers and volumes..." -ForegroundColor Yellow
    docker-compose -f $ComposeFile down -v --remove-orphans
    docker system prune -f
}

# Build images if requested
if ($Build) {
    Write-Host "🔨 Building Docker images..." -ForegroundColor Yellow
    docker-compose -f $ComposeFile build --no-cache
}

# Start services
Write-Host "🚀 Starting Houseiana services..." -ForegroundColor Green
docker-compose -f $ComposeFile up -d

# Wait for services to be healthy
Write-Host "⏳ Waiting for services to be ready..." -ForegroundColor Yellow
Start-Sleep -Seconds 10

# Check service status
Write-Host "📊 Service Status:" -ForegroundColor Green
docker-compose -f $ComposeFile ps

# Run database migrations
Write-Host "🗄️ Running database migrations..." -ForegroundColor Yellow
docker-compose -f $ComposeFile exec -T houseiana-api dotnet ef database update

# Show logs if requested
if ($Logs) {
    Write-Host "📜 Showing application logs..." -ForegroundColor Cyan
    docker-compose -f $ComposeFile logs -f houseiana-api
}

# Show access information
Write-Host @"

✅ Houseiana Backend Deployed Successfully!

🌐 API Endpoints:
   Main API: http://localhost:4000
   Health Check: http://localhost:4000/api/health
   API Documentation: http://localhost:4000/api

🗄️ Database:
   PostgreSQL: localhost:5432
   Database: houseiana
   Username: postgres
   Password: houseiana123

🔄 Redis Cache:
   Redis: localhost:6379

🐳 Docker Commands:
   View logs: docker-compose -f $ComposeFile logs -f
   Stop services: docker-compose -f $ComposeFile down
   Restart: docker-compose -f $ComposeFile restart
   Shell access: docker-compose -f $ComposeFile exec houseiana-api bash

📊 Monitor:
   docker-compose -f $ComposeFile ps
   docker stats

"@ -ForegroundColor Cyan

Write-Host "🎉 Deployment completed!" -ForegroundColor Green