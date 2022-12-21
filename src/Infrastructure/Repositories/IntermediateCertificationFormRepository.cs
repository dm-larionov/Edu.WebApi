using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class IntermediateCertificationFormRepository : IIntermediateCertificationFormRepository
{
    private readonly IRepositoryAsync<IntermediateCertificationForm, int> _repository;

    public IntermediateCertificationFormRepository(IRepositoryAsync<IntermediateCertificationForm, int> repository)
    {
        _repository = repository;
    }
}
