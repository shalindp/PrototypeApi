namespace Contracts.Authentication.Responses;

public struct RefreshTokenResponse
{
    public string Email { get; set; }

    public string Password { get; set; }
}