using System.ComponentModel.DataAnnotations;
using Application.Authentication.Dto;
using Application.External.Interfaces.Authentication;
using Application.External.Interfaces.Services;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.PostgresSql;
using PrototypeBackend.Entities;

namespace Application.Authentication.Commands;

public struct RefreshTokenCommand : IRequest<Result<AuthenticationDto>>
{
    private readonly string _refreshToken;

    public RefreshTokenCommand(string refreshToken)
    {
        _refreshToken = refreshToken;
    }

    public class SignUpCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthenticationDto>>
    {
        private readonly PostgresDbContext _postgresDbContext;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;

        public SignUpCommandHandler(
            IJwtTokenGenerator tokenGenerator, 
            PostgresDbContext postgresDbContext, 
            IDateTimeProvider dateTimeProvider, 
            IUserProvider userProvider)
        {
            _tokenGenerator = tokenGenerator;
            _postgresDbContext = postgresDbContext;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
        }

        public async Task<Result<AuthenticationDto>> Handle(RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var validRefreshTokenCandidate = await _postgresDbContext.RefreshTokens.FirstOrDefaultAsync(c =>
                c.Token == request._refreshToken && c.UserId == _userProvider.UserId && c.Status == Status.Active &&
                c.ExpireDate > _dateTimeProvider.UtcNow, cancellationToken: cancellationToken);

            if (validRefreshTokenCandidate == null)
            {
                return new ValidationException(ExceptionsConstants.InvalidRefreshToken)
                    .ToResult<AuthenticationDto>();
            }

            validRefreshTokenCandidate.Status = Status.Deleted;
            await _postgresDbContext.SaveChangesAsync(cancellationToken);

            var generateToken =
                await _tokenGenerator.GenerateToken(_postgresDbContext, _userProvider.UserId, _userProvider.Email);

            return new AuthenticationDto()
            {
                UserId = _userProvider.UserId,
                Email = _userProvider.Email,
                Token = generateToken.Token,
                RefreshToken = generateToken.RefreshToken
            };
        }
    }
}