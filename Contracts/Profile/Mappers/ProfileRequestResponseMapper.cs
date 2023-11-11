using Application.Profile.Dto;
using Contracts.Profile.Requests;
using Contracts.Profile.Responses;
using Riok.Mapperly.Abstractions;

namespace Contracts.Profile.Mappers;

[Mapper]
public static partial class ProfileRequestResponseMapper
{
    public static partial ProfileResponse Map(ProfileDto dto);
    public static partial ProfileDto Map(CreateProfileRequest dto);
}