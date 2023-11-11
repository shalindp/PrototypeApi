using System.ComponentModel.DataAnnotations;

namespace PrototypeBackend.Json;

public struct OccupationJson
{
    [Range(1, int.MaxValue)]
    public int OccupationId { get; set; }

    public string Value { get; set; }
}