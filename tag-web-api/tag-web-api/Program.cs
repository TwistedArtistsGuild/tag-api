// <copyright file="Program.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TAGWEBAPI.Data;
using TAGWEBAPI.Models;

Console.WriteLine("========================================");
Console.WriteLine("Setting Up TAG WEB API...");
Console.WriteLine("========================================");

var textprefix = "==--->>>|";

var builder = WebApplication.CreateBuilder(args);

// Retrieve the Application Insights connection string from the environment variable
var appInsightsConnectionString = builder.Configuration.GetConnectionString("appinsights");
if (!string.IsNullOrEmpty(appInsightsConnectionString))
{
    // Register Application Insights carefully - failures during design-time (dotnet-ef)
    // or mismatched package/tooling versions can throw when listeners are attached.
    try
    {
        builder.Services.AddApplicationInsightsTelemetry(appInsightsConnectionString);
        Console.WriteLine(textprefix + "Application Insights telemetry enabled.");
    }
    catch (Exception aiEx)
    {
        // Don't allow telemetry registration to abort the host build, especially during design-time operations.
        Console.WriteLine(textprefix + "Could not enable Application Insights telemetry. Continuing without it.");
        Console.WriteLine("Telemetry registration error: " + aiEx.Message);
    }
}
else
{
    Console.WriteLine("Application Insights connection string not found. Telemetry is disabled.");
}

// Validate the database connection string
var dbConnectionString = builder.Configuration.GetConnectionString("tag_web_db");
if (string.IsNullOrEmpty(dbConnectionString))
{
    Console.WriteLine("========================================");
    Console.WriteLine("ERROR: Database connection string 'tag_web_db' is not initialized.");
    Console.WriteLine("========================================");
    throw new InvalidOperationException("Database connection string 'tag_web_db' is not configured.");
}
else
{
    Console.WriteLine(textprefix + "Database connection string 'tag_web_db' is initialized.");
}

// Add services to the container.
builder.Services.AddControllers();
Console.WriteLine(textprefix + "Controllers added to the service container.");

builder.Services.AddDbContext<TAGDBContext>(options =>
    options.UseNpgsql(dbConnectionString));
Console.WriteLine(textprefix + "Database context configured with PostgreSQL.");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Define a named CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder
            .WithOrigins(
                "https://dev.twistedartistsguild.com",
                "https://twistedartistsguild.com",
                "https://www.twistedartistsguild.com", // Note for the future that CORS can keep dev, staging, and prod responding to only their peer group
                "https://staging.twistedartistsguild.com",
                "http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Allow credentials for cross-origin requests
    });
});
Console.WriteLine(textprefix + "CORS policy 'AllowSpecificOrigins' defined.");

try
{
    var app = builder.Build();
#pragma warning disable CA1303 // Do not pass literals as localized parameters
    Console.WriteLine(textprefix + "Application built successfully.");
#pragma warning restore CA1303 // Do not pass literals as localized parameters

    // Apply the CORS policy globally
    app.UseCors("AllowSpecificOrigins");
    Console.WriteLine(textprefix + "CORS policy applied globally.");

    if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        Console.WriteLine(textprefix + "Swagger enabled for API documentation.");
    }

    if (app.Environment.IsProduction()) // Temporary for testing, remove in future
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        Console.WriteLine(textprefix + "Swagger enabled for API documentation.");
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    if (app.Environment.IsProduction() || app.Environment.IsStaging())
    {
        app.UseHttpsRedirection();
    }

    app.UseAuthorization();

    // Map the default API call to return "Hello World!"
    app.MapGet("/api/", () => "Hello World!");

    app.MapControllers();

    Console.WriteLine("========================================");
    Console.WriteLine("TAG WEB API is starting: app.run()");
    Console.WriteLine("========================================");
    app.Run();
}
catch (Exception ex)
{
    // Always log to console first for immediate visibility
    Console.WriteLine("========================================");
    Console.WriteLine("Exception caught in Program.cs:");
    // Print the full exception (includes inner exceptions) to help debugging during design-time operations
    Console.WriteLine(ex.ToString());
    Console.WriteLine("========================================");

    // Attempt to log to database - with smarter exception handling
    // If the host was aborted by a diagnostic listener (HostAbortedException) it's often
    // informational during design-time runs; skip trying to resolve services in that case
    if (ex is HostAbortedException)
    {
        Console.WriteLine("Host aborted during build; skipping database logging to avoid cascading failures.");
    }
    else
    {
        try
        {
            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TAGDBContext>();

                // Simple database connection check
                if (db.Database.CanConnect())
                {
                    var log = new Log
                    {
                        Tags = "Program.cs",
                        ShortText = "ERROR",
                        Critical = true,
                        LoggedData = $"Message: {ex.Message} \r\n Stack: {ex.StackTrace}",
                        LogTimestamp = DateTime.UtcNow, // Always use UTC time
                    };
                    db.Logs.Add(log);
                    db.SaveChanges();
                    Console.WriteLine("Error successfully logged to database.");
                }
                else
                {
                    Console.WriteLine("Database connection failed - error logged to console only.");
                }
            }
        }
        catch (Exception dbEx)
        {
            // If database logging fails, just note it and continue
            // This handles DB doesn't exist, permissions issues, etc. 
            Console.WriteLine("Could not log to database:");
            Console.WriteLine($"Reason: {dbEx.Message}");

            // No need to re-log the original exception - it's already in the console output
        }
    }
}
