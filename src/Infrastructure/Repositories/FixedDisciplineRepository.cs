using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class FixedDisciplineRepository : IFixedDisciplineRepository
{
    private readonly IRepositoryAsync<FixedDiscipline, int> _repository;

    public FixedDisciplineRepository(IRepositoryAsync<FixedDiscipline, int> repository)
    {
        _repository = repository;
    }
}
