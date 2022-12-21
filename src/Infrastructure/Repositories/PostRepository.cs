using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly IRepositoryAsync<Post, int> _repository;

    public PostRepository(IRepositoryAsync<Post, int> repository)
    {
        _repository = repository;
    }
}
