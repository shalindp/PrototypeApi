using Application.Profile.Dto;
using PrototypeBackend.Entities;
using PrototypeBackend.Json;
using Riok.Mapperly.Abstractions;

namespace Application.Profile.Mappers;

[Mapper]
public static partial class OccupationDtoMapper
{
    public static partial OccupationDto Map(OccupationEntity result);
    public static partial OccupationJson MapToJson(OccupationDto result);
}

