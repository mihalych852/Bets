using NotificationService.Services.Helpers;
using NotificationService.MailServices.Helper;
using NotificationService.Api.Helpers;
using NotificationService.DataAccess;
using Microsoft.EntityFrameworkCore;
using NotificationService.DataAccess.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var config = builder.Configuration;

builder.Services
    .AddNotificationServices()
    .AddNotificationSendingServices()
    .AddMailServices(config)
    .AddServices(config);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    await db.Database.MigrateAsync();

    var dbSeedHelper = new DbSeedHelper(config, db);
    await dbSeedHelper.CreateDefaultMessengerAsync();
}

app.Run();
