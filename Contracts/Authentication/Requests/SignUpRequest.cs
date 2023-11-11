using System.ComponentModel.DataAnnotations;

namespace Contracts.Authentication.Requests;

public struct SignUpRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}