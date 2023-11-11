using System.ComponentModel.DataAnnotations;
using Application.Authentication.Dto;
using Application.External.Interfaces.Authentication;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.PostgresSql;

namespace Application.Authentication.Queries;

public struct SignInQuery : IRequest<Result<AuthenticationDto>>
{
    private readonly string _email;
    private readonly string _password;

    public SignInQuery(string email, string password)
    {
        _email = email;
        _password = password;
    }

    public class SignInQueryHandler : IRequestHandler<SignInQuery, Result<AuthenticationDto>>
    {
        private readonly PostgresDbContext _postgresDbContext;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public SignInQueryHandler(IJwtTokenGenerator tokenGenerator, PostgresDbContext postgresDbContext)
        {
            _tokenGenerator = tokenGenerator;
            _postgresDbContext = postgresDbContext;
        }

        public async Task<Result<AuthenticationDto>> Handle(SignInQuery request, CancellationToken cancellationToken)
        {
            var user = await _postgresDbContext.Users.FirstOrDefaultAsync(c =>
                c.Email == request._email, cancellationToken: cancellationToken);

            if (user == null)
            {
                return new ValidationException(ExceptionsConstants.SignInInvalidCredentials)
                    .ToResult<AuthenticationDto>();
            }

            if (!BCrypt.Net.BCrypt.Verify(request._password, user.PasswordHash))
            {
                return new ValidationException(ExceptionsConstants.SignInInvalidCredentials)
                    .ToResult<AuthenticationDto>();
            }

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