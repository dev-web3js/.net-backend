#!/bin/bash

# Shell script to create and apply EF Core migrations

echo "🔧 Setting up Entity Framework migrations..."

# Install EF Core tools if not already installed
echo "Installing EF Core tools..."
dotnet tool install --global dotnet-ef --version 5.0.17

# Add initial migration
echo "Creating initial migration..."
dotnet ef migrations add InitialCreate

# Update database
echo "Updating database..."
dotnet ef database update

echo "✅ Database setup completed!"
echo "📚 API Documentation will be available at: http://localhost:4000/api"