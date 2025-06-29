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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/ServiceLog_Log.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Warning()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString")));

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton<MongoDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/seed-category", async ([FromServices] MongoDbContext db) =>
{
    try
    {
        var newCategory = new Category
        {
            // Id = nie ustawiaj tutaj!
            Name = "Laptop",
            ServiceOptions = new List<string>
    {
        "Screen Replacement",
        "Battery Replacement"
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
});



app.Run();