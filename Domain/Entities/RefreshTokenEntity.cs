using System.ComponentModel.DataAnnotations;

namespace PrototypeBackend.Entities;

public class RefreshToken : BaseEntity
{
    [Key]
    public int RefreshTokenId { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = null!;
    public DateTime ExpireDate { get; set; }
}