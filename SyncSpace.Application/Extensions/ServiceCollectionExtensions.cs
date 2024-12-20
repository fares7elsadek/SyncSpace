﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SyncSpace.Application.ApplicationUser;
using SyncSpace.Application.Services;

namespace SyncSpace.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
            policy =>
            {
                policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
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
    }
    public static void AddSerilog(this IHostBuilder host)
    {
        host.UseSerilog((context, loggerConfig) =>
        {
            loggerConfig.ReadFrom.Configuration(context.Configuration);
        });
    }
}