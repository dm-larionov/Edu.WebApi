using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class EducationFormRepository : IEducationFormRepository
{
    private readonly IRepositoryAsync<EducationForm, int> _repository;

    public EducationFormRepository(IRepositoryAsync<EducationForm, int> repository)
    {
        _repository = repository;
    }
}
