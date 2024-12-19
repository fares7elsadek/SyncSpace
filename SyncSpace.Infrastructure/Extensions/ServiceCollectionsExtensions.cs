using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SyncSpace.Domain.Repositories;
using SyncSpace.Infrastructure.Data;
using SyncSpace.Infrastructure.Repositories;


namespace SyncSpace.Infrastructure.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cs"));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
