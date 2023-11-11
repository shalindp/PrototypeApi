using Application.Profile.Dto;
using Contracts.Profile.Responses;
using Riok.Mapperly.Abstractions;

namespace Contracts.Profile.Mappers;

[Mapper]
public static partial class GenderIdentityRequestResponseMapper
{
    public static partial IEnumerable<GenderIdentityResponse> Map(IEnumerable<GenderIdentityDto> dto);
}