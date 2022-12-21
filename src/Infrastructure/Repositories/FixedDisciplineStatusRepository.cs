using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class FixedDisciplineStatusRepository : IFixedDisciplineStatusRepository
{
    private readonly IRepositoryAsync<FixedDisciplineStatus, int> _repository;

    public FixedDisciplineStatusRepository(IRepositoryAsync<FixedDisciplineStatus, int> repository)
    {
        _repository = repository;
    }
}
