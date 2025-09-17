using DefaultCorsPolicyNugetPackage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Data;
using System.Text.Json;
using TeknikServis.Application;
using TeknikServis.Infrastructure;
using TeknikServis.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SqlServer");
var columnOptions = new ColumnOptions();


Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.MSSqlServer(
        connectionString,
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "logs",           
            AutoCreateSqlTable = true   
        },
        columnOptions: columnOptions,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
    )
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDefaultCors();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});

var healthChecksBuilder = builder.Services.AddHealthChecks();
if (!string.IsNullOrWhiteSpace(connectionString))
{
    healthChecksBuilder.AddSqlServer(connectionString, tags: new[] { "db" });
}
else
{
    healthChecksBuilder.AddCheck("self", () => HealthCheckResult.Healthy("db configure edilmedi."));
}

builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseExceptionHandler();

app.Use(async (context, next) =>
{
    const int LIMIT = 100;
    var cache = context.RequestServices.GetRequiredService<IMemoryCache>();
    var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    var key = $"rate_{ip}";

    if (!cache.TryGetValue<int>(key, out var counter))
    {
        counter = 0;
    }

    if (counter >= LIMIT)
    {
        context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await context.Response.WriteAsync("Too Many Requests");
        return;
    }

    cache.Set(key, counter + 1, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) });
    await next();
});

app.MapControllers();

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags != null && check.Tags.Contains("db"),
    ResponseWriter = async (httpContext, report) =>
    {
        httpContext.Response.ContentType = "application/json";
        var result = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new { name = e.Key, status = e.Value.Status.ToString(), description = e.Value.Description })
        };
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(result));
    }
});

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false,
    ResponseWriter = async (httpContext, report) =>
    {
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new { status = "Live" }));
    }
});

ExtensionsMiddleware.CreateFirstUser(app);

app.Run();
