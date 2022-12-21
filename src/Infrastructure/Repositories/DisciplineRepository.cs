using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class DisciplineRepository : IDisciplineRepository
{
    private readonly IRepositoryAsync<Discipline, int> _repository;

    public DisciplineRepository(IRepositoryAsync<Discipline, int> repository)
    {
        _repository = repository;
    }
}
