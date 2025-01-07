using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SyncSpace.Application.ApplicationUser;
using SyncSpace.Application.Services;
using SyncSpace.Application.Services.EmailService;
using SyncSpace.Domain.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using SyncSpace.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace SyncSpace.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("SignalRPolicy", policy =>
            {
                policy.AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials()
                      .SetIsOriginAllowed(origin => true);
            });
        });


        services.AddHttpContextAccessor();
        var assemply = typeof(ServiceCollectionExtensions).Assembly;
        services.AddAutoMapper(assemply);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemply));
        services.AddValidatorsFromAssembly(assemply)
            .AddFluentValidationAutoValidation();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserContext, UserContext>();
        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 5 * 1024 * 1024;
        });
        services.AddScoped<IFileService, FileService>();
        services.Configure<EmailProvider>(configuration.GetSection("email"));
        services.AddScoped<EmailService>();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddScoped<IUrlHelper>(sp =>
        {
            var actionContext = sp.GetRequiredService<IActionContextAccessor>().ActionContext;
            return new UrlHelper(actionContext);
        });
        services.AddScoped<IEmailSender<User>, EmailService>();
    }
    public static void AddSerilog(this IHostBuilder host)
    {
        host.UseSerilog((context, loggerConfig) =>
        {
            loggerConfig.ReadFrom.Configuration(context.Configuration);
        });
    }
}
