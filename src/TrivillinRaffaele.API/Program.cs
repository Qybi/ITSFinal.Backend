using TrivillinRaffaele.DataAccess.Abstractions.Contexts;
using TrivillinRaffaele.DataAccess.Contexts;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using TrivillinRaffaele.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("azuresql") ?? throw new InvalidOperationException("Connection string 'azuresql' not found."));
});

builder.Services.AddUnitOfWork();

builder.Services.AddOpenApi();
builder.Services.AddCors(c =>
{
    c.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// ignore possible reference loops in json serialization
builder.Services.Configure<JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "API V1");
    });
}

if (app.Environment.IsProduction())
    app.UseHttpsRedirection();

app.MapSensorDataEndPoints();
app.MapSensorsEndPoints();

app.Run();
