# PowerShell script to run backend in Development mode
Write-Host "Starting JLITE Backend API in Development Mode..." -ForegroundColor Green
Write-Host "Mock Email Service will be used (no real emails sent)" -ForegroundColor Yellow
Write-Host ""

$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet run
