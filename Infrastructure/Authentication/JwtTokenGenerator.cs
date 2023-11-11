using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.External.Interfaces.Authentication;
using Application.External.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.PostgresSql;
using PrototypeBackend.Entities;

namespace Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly Settings _settings;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<Settings> settings)
    {
        _dateTimeProvider = dateTimeProvider;
        _settings = settings.Value;
    }

    public async Task<GenerateTokenResult> GenerateToken(PostgresDbContext postgresDbContext, int userId, string email)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var tokenDescription = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
            }),
            Expires = _dateTimeProvider.UtcNow.Add(_settings.ExpiryTimeFrame ?? throw new KeyNotFoundException()),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret ?? throw new KeyNotFoundException())),
                SecurityAlgorithms.HmacSha256)
        };

        var securityToken = jwtTokenHandler.CreateToken(tokenDescription);

        var refreshToken = new RefreshToken
        {
            UserId = userId,
            Token = Guid.NewGuid().ToString(),
            Created = _dateTimeProvider.UtcNow,
            ExpireDate = _dateTimeProvider.UtcNow.Add(_settings.ExpiryTimeFrame ?? throw new KeyNotFoundException()),
        };

        postgresDbContext.RefreshTokens.Add(refreshToken);
        await postgresDbContext.SaveChangesAsync();

        var token = jwtTokenHandler.WriteToken(securityToken);

        return new GenerateTokenResult
        {
            Token = token,
            RefreshToken = refreshToken.Token
        };
    }
}