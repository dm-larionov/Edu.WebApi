using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class ReceivedEducationRepository : IReceivedEducationRepository
{
    private readonly IRepositoryAsync<ReceivedEducation, int> _repository;

    public ReceivedEducationRepository(IRepositoryAsync<ReceivedEducation, int> repository)
    {
        _repository = repository;
    }
}
