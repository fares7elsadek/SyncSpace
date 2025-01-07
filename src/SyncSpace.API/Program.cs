using Serilog;
using SyncSpace.API.Extensions;
using SyncSpace.API.Middlewares;
using SyncSpace.API.SignalR.Hubs;
using SyncSpace.Application.Extensions;
using SyncSpace.Infrastructure.Extensions;
using SyncSpace.Infrastructure.Seeder;

namespace SyncSpace.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Services.AddSignalR();
                builder.Services.AddControllers();
                builder.Services.AddInfrastructure(builder.Configuration);
                builder.Services.AddPresentation();
                builder.Services.AddApplication(builder.Configuration);

                builder.Services.AddEndpointsApiExplorer();
                builder.Host.AddSerilog();


                var app = builder.Build();
                await SyncSpaceSeeder(app);

                if (true)
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseMiddleware<ErrorHandlingMiddleware>();
                app.UseSerilogRequestLogging();
                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseRouting();
                app.UseCors("SignalRPolicy");
                app.UseAuthentication();
                app.UseAuthorization();
                app.MapHub<StreamingHub>("/streaminghub");
                app.MapControllers();
                app.Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application startup failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        public static async Task SyncSpaceSeeder(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<ISyncSpaceSeeder>();
            await seeder.Seed();
        }
    }
}
