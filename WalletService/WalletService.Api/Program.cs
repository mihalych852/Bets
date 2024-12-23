using WalletService.Service.Helpers;
using WalletService.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var config = builder.Configuration;

builder.Services
    .AddWalletServices()
    .AddServices(config)
    .AddRabitSetvices(config);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddLogger(config);
builder.Services.AddCors(options =>
{
    var frontendUrl = "http://localhost:3000";
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(frontendUrl).AllowAnyHeader().AllowAnyMethod();
    });
});
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

app.Run();
