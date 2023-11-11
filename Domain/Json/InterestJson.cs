using System.ComponentModel.DataAnnotations;

namespace PrototypeBackend.Json;

public struct InterestJson
{
    [Range(1, int.MaxValue)]
    public int InterestId { get; set; }

    public string Value { get; set; }
}