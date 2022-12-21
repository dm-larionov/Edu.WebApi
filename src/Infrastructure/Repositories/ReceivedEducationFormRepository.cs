using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class ReceivedEducationFormRepository : IReceivedEducationFormRepository
{
    private readonly IRepositoryAsync<ReceivedEducationForm, int> _repository;

    public ReceivedEducationFormRepository(IRepositoryAsync<ReceivedEducationForm, int> repository)
    {
        _repository = repository;
    }
}
