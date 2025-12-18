using Microsoft.AspNetCore.Authentication;
using Tekus.API.Middlewares;
using Tekus.API.Security;
using Tekus.Application;
using Tekus.Application.DependencyInjection;
using Tekus.Application.Interfaces.Services;
using Tekus.Infrastructure.DependencyInjection;
using Tekus.Infrastructure.ExternalServices;
using Microsoft.EntityFrameworkCore;
using Tekus.Infrastructure.Persistence.Context;





var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowDev", policy =>
    {
        policy.WithOrigins("https://localhost:7200") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Application & Infrastructure
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

//  External Services (Countries API)
builder.Services.AddHttpClient<ICountryProvider, RestCountriesProvider>();

// Simple Authentication (fake user)
builder.Services.AddAuthentication("Default")
    .AddScheme<AuthenticationSchemeOptions, SimpleAuthHandler>(
        "Default", null);

builder.Services.AddAuthorization();

builder.Services.AddDbContext<TekusDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")
    )
);

var app = builder.Build();
app.UseCors("AllowDev");

app.UseDeveloperExceptionPage();


 //Middlewares
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