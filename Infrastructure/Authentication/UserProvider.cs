using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.External.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication;

public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _accessor;

    public UserProvider(IHttpContextAccessor accessor)
    {
        _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
    }

    public int UserId
    {
        get
        {
            var principal = _accessor.HttpContext?.User;
            int.TryParse(principal?.FindFirstValue(JwtRegisteredClaimNames.Jti), out var userId);
            return userId;
        }
    }
    public string Email
    {
        get
        {
            var principal = _accessor.HttpContext?.User;
            return principal?.FindFirstValue(JwtRegisteredClaimNames.Email) ?? string.Empty;
        }
    }
}