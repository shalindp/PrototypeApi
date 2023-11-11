using System.Text;
using Application.External.Interfaces.Authentication;
using Application.External.Interfaces.Services;
using Infrastructure.Authentication;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PrototypeBackend.Constants;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<Settings>(configuration.GetSection(Settings.SectionName));

        //jwt:auth
        var key = Encoding.ASCII.GetBytes(configuration.GetSection(
                $"{Settings.SectionName}:Secret").Value ?? throw new KeyNotFoundException()
        );
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            //@todo change this to true prod
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateLifetime = true,
        };

        services.AddAuthentication(c =>
        {
            c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            c.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(c =>
        {
            c.SaveToken = true;
            c.TokenValidationParameters = tokenValidationParameters;
        });

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton(tokenValidationParameters);
        services.AddTransient<IUserProvider, UserProvider>();

        services.AddAuthorization(c =>
        {
            c.AddPolicy(IdentityConstants.AdminUserClaimPolicy,
                o => o.RequireClaim(IdentityConstants.AdminUserClaimName, "true"));
        });

        return services;
    }
}