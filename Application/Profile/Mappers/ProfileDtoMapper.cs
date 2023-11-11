using Application.Profile.Dto;
using PrototypeBackend.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Profile.Mappers;

[Mapper]
public static partial class ProfileDtoMapper
{
    public static partial ProfileDto Map(ProfileEntity result);
    public static partial ProfileEntity Map(ProfileDto result);
}

