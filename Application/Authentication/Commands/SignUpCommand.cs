using System.ComponentModel.DataAnnotations;
using Application.Authentication.Dto;
using Application.External.Interfaces.Authentication;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence.PostgresSql;
using PrototypeBackend.Entities;

namespace Application.Authentication.Commands;

public struct SignUpCommand : IRequest<Result<AuthenticationDto>>
{
    private readonly string _email;
    private readonly string _password;

    public SignUpCommand(string email, string password)
    {
        _email = email;
        _password = password;
    }

    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result<AuthenticationDto>>
    {
        private readonly PostgresDbContext _postgresDbContext;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly Settings _settings;

        public SignUpCommandHandler(IJwtTokenGenerator tokenGenerator, PostgresDbContext postgresDbContext,
            IOptions<Settings> settings)
        {
            _tokenGenerator = tokenGenerator;
            _postgresDbContext = postgresDbContext;
            _settings = settings.Value;
        }

        public async Task<Result<AuthenticationDto>> Handle(SignUpCommand request,
            CancellationToken cancellationToken)
        {
            var userWithEmail = await _postgresDbContext.Users.FirstOrDefaultAsync(c => c.Email == request._email,
                cancellationToken: cancellationToken);

            if (userWithEmail != null)
            {
                return new ValidationException(ExceptionsConstants.SignUpDuplicate)
                    .ToResult<AuthenticationDto>();
            }

            var user = _postgresDbContext.Add(new UserEntity()
            {
                Email = request._email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request._password)
            }).Entity;

            await _postgresDbContext.SaveChangesAsync(cancellationToken);

            var generateToken =await _tokenGenerator.GenerateToken(_postgresDbContext, user.UserId, request._email);

            return new AuthenticationDto()
            {
                UserId = user.UserId,
                Email = user.Email,
                Token = generateToken.Token,
                RefreshToken = generateToken.RefreshToken
            };
        }
    }
}