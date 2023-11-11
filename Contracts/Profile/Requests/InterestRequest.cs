using System.ComponentModel.DataAnnotations;
using Application.Profile.Dto;
using PrototypeBackend.Entities;
using PrototypeBackend.Json;

namespace Contracts.Profile.Requests;

public struct InterestRequest
{
    [Range(1, int.MaxValue)] public int InterestId { get; set; }
    [Required] public string Value { get; set; }
}

