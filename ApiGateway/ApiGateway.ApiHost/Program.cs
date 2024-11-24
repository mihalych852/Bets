using ApiGateway.ApiHost.Service;
using ApiGateway_Core.Configurations;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Serilog;

namespace ApiGateway.ApiHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<MicroservicesConfig>(builder.Configuration.GetSection("Microservices"));

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("app_name", "ApiGateway")
                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.Request | HttpLoggingFields.Response | HttpLoggingFields.RequestBody | HttpLoggingFields.ResponseBody;
            });

            builder.Services.AddHttpClient<HttpRequestHandler>();
            builder.Services.AddControllers();
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gateway", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter your token in the format **Bearer {your token}**",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            var app = builder.Build();
            app.UseHttpLogging();

            // ������ ������������� �����������
            app.UseSerilogRequestLogging();
            var log = app.Services.GetService<ILogger<Program>>();
            log?.LogDebug("debug");
            log?.LogInformation("info");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiGateway API V1"); // ��������� �������� ����� ��� Swagger
                    c.DocumentTitle = "ApiGateway";
                    c.RoutePrefix = string.Empty; // ������������� Swagger UI �� ������ (http://localhost:5000/)
                });
            }

            app.UseHttpsRedirection();

            //app.UseAuthentication(); // �������� ��������������
            //app.UseAuthorization();  // �������� �����������

            app.MapControllers();

            app.Run();
        }
    }
}
