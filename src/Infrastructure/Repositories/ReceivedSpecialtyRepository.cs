using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class ReceivedSpecialtyRepository : IReceivedSpecialtyRepository
{
    private readonly IRepositoryAsync<ReceivedSpecialty, int> _repository;

    public ReceivedSpecialtyRepository(IRepositoryAsync<ReceivedSpecialty, int> repository)
    {
        _repository = repository;
    }
}
