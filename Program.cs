using JLITE.API.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Use Mock Email Service in Development, Real Email Service in Production
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped<IEmailService, MockEmailService>();
    Console.WriteLine("⚠️  Using MOCK Email Service - emails won't actually send");
}
else
{
    builder.Services.AddScoped<IEmailService, EmailService>();
}

// Configure CORS for Angular frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
policy.WithOrigins(
    "http://localhost:4200",
    "https://your-firebase-project.web.app",
    "https://your-firebase-project.firebaseapp.com"
)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
