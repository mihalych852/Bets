using UserServer.WebHost.Extensions;
using UserServer.DataAccess.Extensions;

namespace UserServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;

            builder.Services.AddServices(configuration);

            builder.AddLogger(configuration);

            var app = builder.Build();

            app.AddHttpLogging<Program>();
            
            await app.AddMigrationAndSeedDataAsync();

            if (app.Environment.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Service API V1"); // ��������� �������� ����� ��� Swagger
                    c.RoutePrefix = string.Empty; // ������������� Swagger UI �� ������ (http://localhost:5000/)
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseIdentityServer();

            await app.AddCustomMiddleware();

            app.UseAuthorization();

            // ��������� �������� �����
            app.MapControllers();

            await app.RunAsync();
        }
    }
}
