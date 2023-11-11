namespace Application.External.Interfaces.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}