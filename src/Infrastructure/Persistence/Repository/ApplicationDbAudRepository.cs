using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Edu.WebApi.Application.Common.Persistence;
using Edu.WebApi.Domain.Common.Contracts;
using Edu.WebApi.Infrastructure.Persistence.Context;
using Mapster;

namespace Edu.WebApi.Infrastructure.Persistence.Repository;

// Inherited from Ardalis.Specification's RepositoryBase<T>
public class ApplicationDbAudRepository<T> : RepositoryBase<T>, IReadAudRepository<T>, IRepositoryAud<T>
    where T : class, IAuditableEntity
{
    public ApplicationDbAudRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    // We override the default behavior when mapping to a dto.
    // We're using Mapster's ProjectToType here to immediately map the result from the database.
    // This is only done when no Selector is defined, so regular specifications with a selector also still work.
    protected override IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification) =>
        specification.Selector is not null
            ? base.ApplySpecification(specification)
            : ApplySpecification(specification, false)
                .ProjectToType<TResult>();
}