using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class DisciplineSemesterRepository : IDisciplineSemesterRepository
{
    private readonly IRepositoryAsync<DisciplineSemester, int> _repository;

    public DisciplineSemesterRepository(IRepositoryAsync<DisciplineSemester, int> repository)
    {
        _repository = repository;
    }
}
