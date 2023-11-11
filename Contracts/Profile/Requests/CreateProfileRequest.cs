using System.ComponentModel.DataAnnotations;
using Application.Profile.Dto;
using PrototypeBackend.Entities;
using PrototypeBackend.Json;

namespace Contracts.Profile.Requests;

public struct CreateProfileRequest
{
    [Required] public string DisplayName { get; init; }
    [Required] public string Description { get; init; }
    [Required] public GenderIdentityRequest GenderIdentity { get; init; }
    [Required] public string PrimaryImageUrl { get; init; }
    public IEnumerable<string> ImageUrls { get; init; }
    [Required] public byte Age { get; init; }
    [Required] public GenderIdentityRequest PreferredGenderIdentity { get; init; }
    [Required] public string City { get; init; }
    [Required] public IEnumerable<InterestRequest> Interests { get; set; }
    [Required] public OccupationRequest Occupation { get; set; }
    [Range(1, int.MaxValue)] public int MaximumAcceptedDistance { get; init; }
    [Range(1, int.MaxValue)] public int PreferredMinimumAge { get; init; }
    [Range(1, int.MaxValue)] public int PreferredMaximumAge { get; init; }
}