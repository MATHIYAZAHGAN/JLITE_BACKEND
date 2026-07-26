# Google Cloud Run Deployment Script for .NET Backend
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "  JLITE Backend - Cloud Run Deployment" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# Check if gcloud is installed
Write-Host "[1/6] Checking Google Cloud CLI..." -ForegroundColor Yellow
if (!(Get-Command gcloud -ErrorAction SilentlyContinue)) {
    Write-Host "ERROR: Google Cloud CLI not found!" -ForegroundColor Red
    Write-Host "Please install from: https://cloud.google.com/sdk/docs/install" -ForegroundColor Yellow
    exit 1
}
Write-Host "Google Cloud CLI found!" -ForegroundColor Green
Write-Host ""

# Check if logged in
Write-Host "[2/6] Checking authentication..." -ForegroundColor Yellow
$account = gcloud auth list --filter=status:ACTIVE --format="value(account)"
if ([string]::IsNullOrEmpty($account)) {
    Write-Host "Not logged in. Please login..." -ForegroundColor Yellow
    gcloud auth login
}
Write-Host "Logged in as: $account" -ForegroundColor Green
Write-Host ""

# Get project ID
Write-Host "[3/6] Getting project information..." -ForegroundColor Yellow
$projectId = gcloud config get-value project
if ([string]::IsNullOrEmpty($projectId)) {
    Write-Host "No project selected. Please set project:" -ForegroundColor Red
    Write-Host "  gcloud config set project YOUR_PROJECT_ID" -ForegroundColor Yellow
    exit 1
}
Write-Host "Project: $projectId" -ForegroundColor Green
Write-Host ""

# Enable required APIs
Write-Host "[4/6] Enabling required APIs (this may take a minute)..." -ForegroundColor Yellow
gcloud services enable run.googleapis.com --quiet
gcloud services enable cloudbuild.googleapis.com --quiet
Write-Host "APIs enabled!" -ForegroundColor Green
Write-Host ""

# Deploy to Cloud Run
Write-Host "[5/6] Deploying to Cloud Run..." -ForegroundColor Yellow
Write-Host "This will take a few minutes..." -ForegroundColor Gray
Write-Host ""

gcloud run deploy jlite-backend `
  --source . `
  --platform managed `
  --region us-central1 `
  --allow-unauthenticated `
  --set-env-vars ASPNETCORE_ENVIRONMENT=Production

if ($LASTEXITCODE -ne 0) {
    Write-Host "Deployment failed!" -ForegroundColor Red
    exit 1
}
Write-Host ""

# Get service URL
Write-Host "[6/6] Getting service URL..." -ForegroundColor Yellow
$serviceUrl = gcloud run services describe jlite-backend --region us-central1 --format="value(status.url)"
Write-Host ""
Write-Host "============================================" -ForegroundColor Green
Write-Host "  DEPLOYMENT SUCCESSFUL!" -ForegroundColor Green
Write-Host "============================================" -ForegroundColor Green
Write-Host ""
Write-Host "Your backend API is live at:" -ForegroundColor Cyan
Write-Host $serviceUrl -ForegroundColor White
Write-Host ""
Write-Host "API Endpoints:" -ForegroundColor Yellow
Write-Host "  Contact: $serviceUrl/api/contact" -ForegroundColor White
Write-Host "  Quote:   $serviceUrl/api/quote" -ForegroundColor White
Write-Host ""
Write-Host "NEXT STEPS:" -ForegroundColor Yellow
Write-Host ""
Write-Host "1. Configure Email Settings:" -ForegroundColor White
Write-Host "   gcloud run services update jlite-backend --region us-central1 --update-env-vars Email__SmtpUser=your-email@gmail.com,Email__SmtpPassword=your-app-password" -ForegroundColor Gray
Write-Host ""
Write-Host "2. Update Frontend (src/environments/environment.prod.ts):" -ForegroundColor White
Write-Host "   apiUrl: '$serviceUrl/api'" -ForegroundColor Gray
Write-Host ""
Write-Host "3. Update Backend CORS (Program.cs):" -ForegroundColor White
Write-Host "   Add your Firebase URL to WithOrigins()" -ForegroundColor Gray
Write-Host ""
Write-Host "4. Redeploy both frontend and backend after changes" -ForegroundColor White
Write-Host ""
Write-Host "View logs: gcloud run logs read jlite-backend --region us-central1" -ForegroundColor Cyan
Write-Host ""
