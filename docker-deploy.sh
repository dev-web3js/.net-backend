#!/bin/bash

# Docker Deployment Script for Houseiana Backend
# Builds and deploys the full stack using Docker Compose

set -e

# Default values
ENVIRONMENT="prod"
BUILD=false
FRESH=false
LOGS=false

# Parse command line arguments
while [[ $# -gt 0 ]]; do
  case $1 in
    --env|--environment)
      ENVIRONMENT="$2"
      shift 2
      ;;
    --build)
      BUILD=true
      shift
      ;;
    --fresh)
      FRESH=true
      shift
      ;;
    --logs)
      LOGS=true
      shift
      ;;
    *)
      echo "Unknown option $1"
      exit 1
      ;;
  esac
done

echo "🐳 Houseiana Docker Deployment Script"
echo "Environment: $ENVIRONMENT"

# Determine compose file
if [ "$ENVIRONMENT" = "dev" ]; then
    COMPOSE_FILE="docker-compose.dev.yml"
    echo "📦 Using development configuration"
else
    COMPOSE_FILE="docker-compose.yml"
    echo "🚀 Using production configuration"
fi

# Fresh start - remove all containers and volumes
if [ "$FRESH" = true ]; then
    echo "🧹 Cleaning up existing containers and volumes..."
    docker-compose -f $COMPOSE_FILE down -v --remove-orphans
    docker system prune -f
fi

# Build images if requested
if [ "$BUILD" = true ]; then
    echo "🔨 Building Docker images..."
    docker-compose -f $COMPOSE_FILE build --no-cache
fi

# Start services
echo "🚀 Starting Houseiana services..."
docker-compose -f $COMPOSE_FILE up -d

# Wait for services to be healthy
echo "⏳ Waiting for services to be ready..."
sleep 10

# Check service status
echo "📊 Service Status:"
docker-compose -f $COMPOSE_FILE ps

# Run database migrations
echo "🗄️ Running database migrations..."
docker-compose -f $COMPOSE_FILE exec -T houseiana-api dotnet ef database update

# Show logs if requested
if [ "$LOGS" = true ]; then
    echo "📜 Showing application logs..."
    docker-compose -f $COMPOSE_FILE logs -f houseiana-api
fi

# Show access information
cat << EOF

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
   View logs: docker-compose -f $COMPOSE_FILE logs -f
   Stop services: docker-compose -f $COMPOSE_FILE down
   Restart: docker-compose -f $COMPOSE_FILE restart
   Shell access: docker-compose -f $COMPOSE_FILE exec houseiana-api bash

📊 Monitor:
   docker-compose -f $COMPOSE_FILE ps
   docker stats

EOF

echo "🎉 Deployment completed!"