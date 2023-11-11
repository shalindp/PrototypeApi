using Application.Authentication.Dto;
using Contracts.Authentication.Responses;
using Riok.Mapperly.Abstractions;

namespace Contracts.Authentication.Mappers;

[Mapper]
public static partial class AuthenticationResponseMapper
{
    public static partial AuthenticationResponse Map(AuthenticationDto dto);
}
