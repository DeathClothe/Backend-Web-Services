using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ReWear.DeathClothe.API.Clothes.Application.Internal.CommandServices;
using ReWear.DeathClothe.API.Clothes.Application.Internal.QueryServices;
using ReWear.DeathClothe.API.Clothes.Domain.Repositories;
using ReWear.DeathClothe.API.Clothes.Domain.Services;
using ReWear.DeathClothe.API.Clothes.Infrastructure.Persistence.EFC.Repositories;
using ReWear.DeathClothe.API.Shared.Domain.Repositories;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy =>
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());
});

// Database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString is null)
    throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Clothes API",
        Version = "v1",
        Description = "Clothes Bounded Context API for DeathClothe",
        Contact = new OpenApiContact
        {
            Name = "DeathClothe Team",
            Email = "contact@deathclothe.com"
        }
    });
    options.EnableAnnotations();
});

// Shared
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Clothes Bounded Context
builder.Services.AddScoped<IClotheRepository, ClotheRepository>();
builder.Services.AddScoped<IClotheCommandService, ClotheCommandService>();
builder.Services.AddScoped<IClotheQueryService, ClotheQueryService>();

var app = builder.Build();

// DB Verification
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Clothes API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
