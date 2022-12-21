using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class AudienceTypeRepository : IAudienceTypeRepository
{
    private readonly IRepositoryAsync<AudienceType, int> _repository;

    public AudienceTypeRepository(IRepositoryAsync<AudienceType, int> repository)
    {
        _repository = repository;
    }
}
