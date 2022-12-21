using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.Posts.Queries.GetAll;

public class GetAllPostsQuery : IRequest<List<GetAllPostsResponse>>
{
    public GetAllPostsQuery()
    {
    }
}

internal class GetAllPostsCachedQueryHandler : IRequestHandler<GetAllPostsQuery, List<GetAllPostsResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllPostsCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllPostsResponse>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _unitOfWork.Repository<Post>().GetAllAsync();
        return posts.Adapt<List<GetAllPostsResponse>>();
    }
}
