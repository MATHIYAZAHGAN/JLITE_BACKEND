# Deploy .NET Backend to Render.com (100% FREE)

## Step-by-Step Guide

### Step 1: Create Account
Go to: https://render.com and sign up (free)

### Step 2: Push Code to GitHub
Your backend code needs to be on GitHub.

```bash
cd Backend/JLITE.API
git init
git add .
git commit -m "Initial backend commit"
git remote add origin https://github.com/yourusername/jlite-backend.git
git push -u origin main
```

### Step 3: Create New Web Service on Render

1. Go to Render Dashboard
2. Click **New +** → **Web Service**
3. Connect your GitHub repository
4. Configure:
   - **Name**: `jlite-backend`
   - **Environment**: `Docker`
   - **Branch**: `main`
   - **Plan**: **Free**

### Step 4: Add Environment Variables

In Render dashboard, add these environment variables:
- `ASPNETCORE_ENVIRONMENT` = `Production`
- `Email__SmtpHost` = `smtp.gmail.com`
- `Email__SmtpPort` = `587`
- `Email__SmtpUser` = `your-email@gmail.com`
- `Email__SmtpPassword` = `your-app-password`
- `Email__ToAddress` = `jlite2025@gmail.com`
- `Email__QuoteAddress` = `jlite@jliteengineers.com`

### Step 5: Deploy

Click **Create Web Service** - Render will automatically build and deploy!

### Step 6: Get Your URL

Your backend will be at: `https://jlite-backend.onrender.com`

## Important Notes

⚠️ **Free tier sleeps after 15 minutes of inactivity**
- First request after sleep takes ~30 seconds
- Good for low-traffic sites
- Upgrade to paid ($7/month) for always-on

✅ **Stays free forever** (no credit card required)
