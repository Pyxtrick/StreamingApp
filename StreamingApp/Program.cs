using Microsoft.EntityFrameworkCore;
using NSwag.Generation.Processors.Security;
using StreamingApp.API;
using StreamingApp.Core;
using StreamingApp.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// API Services
builder.Services.AddApiOptions();

// Core Services
builder.Services.AddCoreOptions();

// DB Services
bool enableDataLogging = builder.Configuration.GetValue("Logging:EnableDataLogging", false);
builder.Services.AddDataBaseFeature(builder.Configuration["ConnectionString"]!);

builder.Services.AddSwaggerDocument(swagger =>
{
    swagger.Title = "Steaming API";
    swagger.Version = "v1";
    swagger.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = (UnitOfWorkContext)services.GetRequiredService<UnitOfWorkContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
