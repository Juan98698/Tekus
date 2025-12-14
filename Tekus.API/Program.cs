using Microsoft.AspNetCore.Authentication;
using Tekus.API.Middlewares;
using Tekus.Application;
using Tekus.Infrastructure.DependencyInjection;
using Tekus.API.Security;
using Tekus.Application.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Application & Infrastructure
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Simple Authentication (fake user)
builder.Services.AddAuthentication("Default")
    .AddScheme<AuthenticationSchemeOptions, SimpleAuthHandler>(
        "Default", null);

builder.Services.AddAuthorization();

var app = builder.Build();

// Middlewares
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();