using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class CathedraRepository : ICathedraRepository
{
    private readonly IRepositoryAsync<Cathedra, int> _repository;

    public CathedraRepository(IRepositoryAsync<Cathedra, int> repository)
    {
        _repository = repository;
    }
}
