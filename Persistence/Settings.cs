namespace Persistence;

public class Settings
{
    public const string SectionName = "Psql";
    public string ConnectionString { get; init; } = null!;
}

