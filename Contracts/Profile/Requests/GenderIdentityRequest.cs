using System.ComponentModel.DataAnnotations;

namespace Contracts.Profile.Requests;

public struct GenderIdentityRequest
{
    [Range(1, int.MaxValue)] public int GenderIdentityId { get; set; }
    [Required] public string Value { get; set; }
}