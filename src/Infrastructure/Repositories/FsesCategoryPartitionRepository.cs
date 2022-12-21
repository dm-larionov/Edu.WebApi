using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class FsesCategoryPartitionRepository : IFsesCategoryPartitionRepository
{
    private readonly IRepositoryAsync<FsesCategoryPartition, int> _repository;

    public FsesCategoryPartitionRepository(IRepositoryAsync<FsesCategoryPartition, int> repository)
    {
        _repository = repository;
    }
}
