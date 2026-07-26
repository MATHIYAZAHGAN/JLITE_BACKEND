# Deploy .NET Backend to Railway.app

## Step-by-Step Guide

### Step 1: Create Account
Go to: https://railway.app and sign up

### Step 2: Install Railway CLI

```bash
npm install -g @railway/cli
```

### Step 3: Login

```bash
railway login
```

### Step 4: Initialize Project

```bash
cd Backend\JLITE.API
railway init
```

### Step 5: Deploy

```bash
railway up
```

### Step 6: Add Environment Variables

```bash
railway variables set ASPNETCORE_ENVIRONMENT=Production
railway variables set Email__SmtpHost=smtp.gmail.com
railway variables set Email__SmtpPort=587
railway variables set Email__SmtpUser=your-email@gmail.com
railway variables set Email__SmtpPassword=your-app-password
railway variables set Email__ToAddress=jlite2025@gmail.com
railway variables set Email__QuoteAddress=jlite@jliteengineers.com
```

### Step 7: Get Your URL

```bash
railway domain
```

Your backend will be at: `https://your-app.up.railway.app`

## Pricing

- **$5 free credit per month**
- Pay-as-you-go after that
- Usually $3-5/month for small apps
- No credit card required to start
