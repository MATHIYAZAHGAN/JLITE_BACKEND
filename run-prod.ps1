# PowerShell script to run backend in Production mode (with real email)
Write-Host "Starting JLITE Backend API in Production Mode..." -ForegroundColor Green
Write-Host "Real Email Service will be used (Gmail SMTP)" -ForegroundColor Cyan
Write-Host ""
Write-Host "WARNING: Make sure you have configured appsettings.json with Gmail credentials!" -ForegroundColor Yellow
Write-Host ""

$env:ASPNETCORE_ENVIRONMENT="Production"
dotnet run
