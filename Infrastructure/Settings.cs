namespace Infrastructure;

public class Settings
{
    public const string SectionName = "Infrastructure";
    public string Secret { get; init; } = null!;
    public TimeSpan? ExpiryTimeFrame { get; set; }
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
}

