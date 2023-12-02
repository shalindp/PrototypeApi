using Application.External.Interfaces.Authentication;
using Application.Profile.Dto;
using Application.Profile.Mappers;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.PostgresSql;
using PrototypeBackend.Entities;

namespace Application.Profile.Queries;

public struct GetGenderIdentitiesQuery : IRequest<Result<IEnumerable<GenderIdentityDto>>>
{
    public class
        GetGenderIdentitiesQueryHandler : IRequestHandler<GetGenderIdentitiesQuery,
            Result<IEnumerable<GenderIdentityDto>>>
    {
        private readonly PostgresDbContext _postgresDbContext;

        public GetGenderIdentitiesQueryHandler(PostgresDbContext postgresDbContext, IUserProvider userProvider)
        {
            _postgresDbContext = postgresDbContext;
        }

        public async Task<Result<IEnumerable<GenderIdentityDto>>> Handle(GetGenderIdentitiesQuery request,
            CancellationToken cancellationToken)
        {
            var genderIdentityEntities = await _postgresDbContext.Genders
                .Where(c=> c.Status == Status.Active) 
                .OrderByDescending(c=>c.SortOrder)
                .ToListAsync(cancellationToken: cancellationToken);

            return new Result<IEnumerable<GenderIdentityDto>>(GenderIdentityDtoMapper.Map(genderIdentityEntities));
        }
    }
}