using BetsService.Services.Helpers;
using BetsService.Api.Helpers;
using BetsService.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var config = builder.Configuration;

builder.Services
    .AddBetsServices()
    .AddRedis(config)
    .AddServices(config);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    var frontendUrl = builder.Configuration.GetValue<string>("frontend-url");
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(frontendUrl).AllowAnyHeader().AllowAnyMethod();
    });
});
builder.AddLogger(config);

var app = builder.Build();

app.AddHttpLogging<Program>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    db.Database.Migrate();
}

app.Run();
