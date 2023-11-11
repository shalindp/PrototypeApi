namespace Contracts.Authentication.Responses;

public struct AuthenticationResponse
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}