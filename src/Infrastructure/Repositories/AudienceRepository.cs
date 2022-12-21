using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class AudienceRepository : IAudienceRepository
{
    private readonly IRepositoryAsync<Audience, int> _repository;

    public AudienceRepository(IRepositoryAsync<Audience, int> repository)
    {
        _repository = repository;
    }
}
