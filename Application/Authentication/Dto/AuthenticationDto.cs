namespace Application.Authentication.Dto;

public struct AuthenticationDto 
{
    public int UserId;
    public string Email;
    public string Token;
    public string RefreshToken;
}