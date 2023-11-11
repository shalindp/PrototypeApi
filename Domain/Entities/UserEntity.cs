using System.ComponentModel.DataAnnotations;
namespace PrototypeBackend.Entities;

public sealed class UserEntity : BaseEntity
{
    [Key]
    public int UserId { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public ProfileEntity? Profile { get; set; }
}