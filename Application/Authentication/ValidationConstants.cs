namespace Application.Authentication;

public static class ExceptionsConstants
{
    public const string SignInInvalidCredentials = "Sorry, invalid credentials. Please try again.";
    public const string SignUpDuplicate = "Sorry, account already exits. Please sign in.";
    public const string InvalidRefreshToken = "Invalid refresh token.";
}