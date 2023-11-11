using System.Security.Claims;

namespace Application.External.Interfaces.Authentication;

public interface IUserProvider
{
    public int UserId { get; }
    public string Email { get; }
}