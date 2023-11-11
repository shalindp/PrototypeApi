using System.ComponentModel.DataAnnotations;
using Application.Profile.Dto;
using PrototypeBackend.Entities;
using PrototypeBackend.Json;

namespace Contracts.Profile.Requests;

public struct OccupationRequest
{
    [Range(1, int.MaxValue)] public int OccupationId { get; set; }
    [Required] public string Value { get; set; }
}

