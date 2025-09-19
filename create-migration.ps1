# PowerShell script to create and apply EF Core migrations

Write-Host "🔧 Setting up Entity Framework migrations..." -ForegroundColor Cyan

# Install EF Core tools if not already installed
Write-Host "Installing EF Core tools..." -ForegroundColor Yellow
dotnet tool install --global dotnet-ef --version 5.0.17

# Add initial migration
Write-Host "Creating initial migration..." -ForegroundColor Yellow
dotnet ef migrations add InitialCreate

# Update database
Write-Host "Updating database..." -ForegroundColor Yellow
dotnet ef database update

Write-Host "✅ Database setup completed!" -ForegroundColor Green
Write-Host "📚 API Documentation will be available at: http://localhost:4000/api" -ForegroundColor Cyan