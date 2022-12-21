using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class EducationCycleRepository : IEducationCycleRepository
{
    private readonly IRepositoryAsync<EducationCycle, int> _repository;

    public EducationCycleRepository(IRepositoryAsync<EducationCycle, int> repository)
    {
        _repository = repository;
    }
}
