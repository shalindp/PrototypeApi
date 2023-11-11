using Application.Profile.Dto;
using PrototypeBackend.Entities;
using PrototypeBackend.Json;
using Riok.Mapperly.Abstractions;

namespace Application.Profile.Mappers;

[Mapper]
public static partial class GenderIdentityDtoMapper
{
    public static partial IEnumerable<GenderIdentityDto> Map(IEnumerable<GenderIdentityEntity> result);
    public static partial GenderIdentityDto Map(GenderIdentityEntity result);
    public static partial GenderIdentityJson Map(GenderIdentityDto result);
}

