using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Identity;
using ServiceLog.Data;
using Microsoft.AspNetCore.Mvc;
using ServiceLog.Models.Domain;
using MongoDB.Driver;
using System;
using Microsoft.IdentityModel.Tokens;
using ServiceLog.Services.interfaces;
using ServiceLog.Services;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/ServiceLog_Log.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Error()
    .CreateLogger();

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("ServiceLog")
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString")));

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.MapGet("/seed-category", async ([FromServices] MongoDbContext db) =>
{
    try
    {
        var newCategory = new Category
        {
            Name = "Laptop",
            ServiceOptions = new List<string>
    {
        "Screen Replacement2",
        "Battery Replacement2"
    }
        };

        await db.Categories.InsertOneAsync(newCategory);

        return Results.Ok(new
        {
            message = "Category added successfully",
            category = newCategory
        });
    }
    catch (MongoDB.Driver.MongoConnectionException ex)
    {
        return Results.Problem($"MongoDB connection failed: {ex.Message}");
    }
    catch (System.TimeoutException)
    {
        return Results.Problem($"MongoDB timeout: Make sure MongoDB is running on localhost:27017");
    }
}).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" }); 



app.Run();