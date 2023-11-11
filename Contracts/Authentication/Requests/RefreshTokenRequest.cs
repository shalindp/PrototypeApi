using System.ComponentModel.DataAnnotations;

namespace Contracts.Authentication.Requests;

public struct RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; }
}