using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class EducationLevelRepository : IEducationLevelRepository
{
    private readonly IRepositoryAsync<EducationLevel, int> _repository;

    public EducationLevelRepository(IRepositoryAsync<EducationLevel, int> repository)
    {
        _repository = repository;
    }
}
