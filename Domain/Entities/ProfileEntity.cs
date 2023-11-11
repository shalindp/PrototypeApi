using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrototypeBackend.Json;

namespace PrototypeBackend.Entities;

public sealed class ProfileEntity : BaseEntity
{
    [Key] public int ProfileId { get; set; }
    public string DisplayName { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    [Column(TypeName = "jsonb")] public GenderIdentityJson GenderIdentity { get; set; }
    public string PrimaryImageUrl { get; set; } = null!;
    [Column(TypeName = "jsonb")] public IEnumerable<string> ImageUrls = new List<string>();
    public byte Age { get; set; }
    [Column(TypeName = "jsonb")] public GenderIdentityJson PreferredGenderIdentity { get; set; }
    public string City { get; set; } = null!;
    public int MaximumAcceptedDistance { get; set; }
    public int PreferredMinimumAge { get; set; }
    public int PreferredMaximumAge { get; set; }
    [Column(TypeName = "jsonb")] public IEnumerable<InterestJson> Interests { get; set; } = new List<InterestJson>();
    [Column(TypeName = "jsonb")] public OccupationJson? Occupation { get; set; }
    public UserEntity? User { get; set; }
}