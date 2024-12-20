
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
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
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

            builder.Services.AddControllers();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddPresentation();
            builder.Services.AddApplication();
            builder.Services.AddSignalR();
            builder.Services.AddEndpointsApiExplorer();
            builder.Host.AddSerilog();
            
            var app = builder.Build();
            SyncSpaceSeeder(app);
            string uploadsPath = Path.Combine(builder.Environment.ContentRootPath, "Uploads");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadsPath),
                RequestPath = "/Resources"
            });
            app.UseSerilogRequestLogging();
            app.UseAuthorization();
            app.MapHub<StreamingHub>("/streamingHub");
            app.MapControllers();
            app.Run();
        }
        public static void SyncSpaceSeeder(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<ISyncSpaceSeeder>();
            seeder.Seed();
        }
    }
}
