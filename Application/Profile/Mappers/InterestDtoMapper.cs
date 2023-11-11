using Application.Profile.Dto;
using PrototypeBackend.Entities;
using PrototypeBackend.Json;
using Riok.Mapperly.Abstractions;

namespace Application.Profile.Mappers;

[Mapper]
public static partial class InterestDtoMapper
{
    public static partial InterestDto Map(InterestEntity result);
    public static partial InterestJson Map(InterestDto result);
    public static partial IEnumerable<InterestJson> Map(IEnumerable<InterestDto> result);
}

