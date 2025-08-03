using eCommerce.Core.ServiceContracts;
using eCommerce.Core.Services;
using eCommerce.Core.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using FluentValidation;

namespace eCommerce.Core;
public static class DependencyInjection
{
    // add IServiceCollection services
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        // Register your core services here
        services.AddTransient<IUsersService, UsersService>();

        // Fix for CS1061: Ensure FluentValidation's extension method is available
        services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

        return services;
    }
}
       