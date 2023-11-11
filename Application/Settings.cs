namespace Application;

public class Settings
{
    public static string SectionName = "Application";
    public string PasswordHashSalt { get; init; } = null!;
}