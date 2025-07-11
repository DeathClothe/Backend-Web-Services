using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ReWear.DeathClothe.API.Categories.Application.Internal.CommandServices;
using ReWear.DeathClothe.API.Categories.Application.Internal.QueryServices;
using ReWear.DeathClothe.API.Categories.Domain.Repositories;
using ReWear.DeathClothe.API.Categories.Domain.Services;
using ReWear.DeathClothe.API.Categories.Infrastructure.Persistence.EFC.Repositories;
using ReWear.DeathClothe.API.Clothes.Application.Internal.CommandServices;
using ReWear.DeathClothe.API.Clothes.Application.Internal.QueryServices;
using ReWear.DeathClothe.API.Clothes.Domain.Repositories;
using ReWear.DeathClothe.API.Clothes.Domain.Services;
using ReWear.DeathClothe.API.Clothes.Infrastructure.Persistence.EFC.Repositories;
using ReWear.DeathClothe.API.IAM.Application.Internal.CommandServices;
using ReWear.DeathClothe.API.IAM.Application.Internal.OutboundServices;
using ReWear.DeathClothe.API.IAM.Application.Internal.QueryServices;
using ReWear.DeathClothe.API.IAM.Domain.Repositories;
using ReWear.DeathClothe.API.IAM.Domain.Services;
using ReWear.DeathClothe.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using ReWear.DeathClothe.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using ReWear.DeathClothe.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using ReWear.DeathClothe.API.IAM.Infrastructure.Tokens.JWT.Services;
using ReWear.DeathClothe.API.Shared.Domain.Repositories;
using ReWear.DeathClothe.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using ReWear.DeathClothe.API.Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy =>
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
});

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString is null) throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

// Add Documentation
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "DeathClothe API",
            Version = "v1",
            Description = "DeathClothe API Documentation",
            TermsOfService = new Uri("https://deathclothe.github.io/DEATHCLOTHELandingPage/"),
            Contact = new OpenApiContact
            {
                Name = "ReWear",
                Email = "contact@rewear.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.EnableAnnotations();
});

// Dependency Injection

// Shared Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IAM Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();


// Clothes Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IClotheRepository, ClotheRepository>();
builder.Services.AddScoped<IClotheCommandService, ClotheCommandService>();
builder.Services.AddScoped<IClotheQueryService, ClotheQueryService>();


// Categories Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryCommandService, CategoryCommandService>();
builder.Services.AddScoped<ICategoryQueryService, CategoryQueryService>();


var app = builder.Build();

// Verify Database Objects are Created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

//Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "DeathClothe API v1");
        options.RoutePrefix = "swagger";
    });


app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
