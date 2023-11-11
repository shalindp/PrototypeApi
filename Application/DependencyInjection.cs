
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(c=>c.RegisterServicesFromAssembly(assembly));
        services.Configure<Settings>(configuration.GetSection(Settings.SectionName));

        return services;
    }
}