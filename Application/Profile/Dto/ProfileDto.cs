using PrototypeBackend.Entities;

namespace Application.Profile.Dto;

public struct ProfileDto
{
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public GenderIdentityDto GenderIdentity { get; set; }
    public string PrimaryImageUrl { get; set; }
    public IEnumerable<string> ImageUrls { get; set; }
    public byte Age { get; set; }
    public GenderIdentityDto PreferredGenderIdentity { get; set; }
    public string City { get; set; }
    public IEnumerable<InterestDto> Interests { get; set; }
    public OccupationDto? Occupation { get; set; }
    public int MaximumAcceptedDistance { get; set; }
    public int PreferredMinimumAge { get; set; }
    public int PreferredMaximumAge { get; set; }
}