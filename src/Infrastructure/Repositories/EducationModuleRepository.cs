using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class EducationModuleRepository : IEducationModuleRepository
{
    private readonly IRepositoryAsync<EducationModule, int> _repository;

    public EducationModuleRepository(IRepositoryAsync<EducationModule, int> repository)
    {
        _repository = repository;
    }
}
