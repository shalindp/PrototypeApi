using System.ComponentModel.DataAnnotations;
using Application.External.Interfaces.Authentication;
using Application.Profile.Dto;
using Application.Profile.Mappers;
using LanguageExt.Common;
using MediatR;
using Persistence.PostgresSql;

namespace Application.Profile.Commands;

public struct CreateProfileCommand : IRequest<Result<int>>
{
    private readonly ProfileDto _profileDto;

    public CreateProfileCommand(ProfileDto profileDto)
    {
        _profileDto = profileDto;
    }

    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Result<int>>
    {
        private readonly PostgresDbContext _postgresDbContext;
        private readonly IUserProvider _userProvider;

        public CreateProfileCommandHandler(PostgresDbContext postgresDbContext, IUserProvider userProvider)
        {
            _postgresDbContext = postgresDbContext;
            _userProvider = userProvider;
        }

        public async Task<Result<int>> Handle(CreateProfileCommand request,
            CancellationToken cancellationToken)
        {
            var profile = ProfileDtoMapper.Map(request._profileDto);
            profile.ProfileId = _userProvider.UserId;

            _postgresDbContext.Add(profile);

            try
            {
                return await _postgresDbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                return new ValidationException(ExceptionsConstants.ProfileCreationFailed).ToResult<int>();
            }
        }
    }
}