using Persistence.PostgresSql;

namespace Application.External.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    Task<GenerateTokenResult> GenerateToken(PostgresDbContext postgresDbContext, int userId, string email);
}

public struct GenerateTokenResult
{
    public readonly string Token { get; init; }
    public readonly string RefreshToken { get; init; }
}