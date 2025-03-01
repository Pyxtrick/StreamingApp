using Microsoft.EntityFrameworkCore;
using NSwag.Generation.Processors.Security;
using StreamingApp.API;
using StreamingApp.Core;
using StreamingApp.API.SignalRHub;
using StreamingApp.DB;
using StreamingApp.Core.Commands;
using StreamingApp.API.BetterTV_7TV;

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

// SignalR
builder.Services.AddSignalR(cfg => cfg.EnableDetailedErrors = true);

// DB Services
bool enableDataLogging = builder.Configuration.GetValue("Logging:EnableDataLogging", false);
builder.Services.AddDataBaseFeature(builder.Configuration["ConnectionString"]!);

builder.Services.AddSwaggerDocument(swagger =>
{
    swagger.Title = "Steaming API";
    swagger.Version = "v1";
    swagger.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", builder => { builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200"); });
});

//builder.Services.AddHostedService<ActivityScheduler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseOpenApi();
    app.UseSwaggerUi();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = (UnitOfWorkContext)services.GetRequiredService<UnitOfWorkContext>();
    context.Database.Migrate();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<ChatHub>("/chathub");
app.MapHub<ClientHub>("/clienthub");

app.MapControllers();

// Start Connection To Twitch at Startup
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<IStartTwitchApi>().Execute();
    Task task = scope.ServiceProvider.GetRequiredService<IEmotesApiRequest>().GetTVEmoteSet();
}

app.Run();