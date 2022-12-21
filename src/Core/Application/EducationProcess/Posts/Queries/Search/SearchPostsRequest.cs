using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Posts.Queries.Search;

public class SearchPostsRequest : PaginationFilter, IRequest<PaginationResponse<PostDto>>
{
}

public class SearchPostsRequestHandler : IRequestHandler<SearchPostsRequest, PaginationResponse<PostDto>>
{
    private readonly IReadAudRepository<Post> _repository;

    public SearchPostsRequestHandler(IReadAudRepository<Post> repository) => _repository = repository;

    public async Task<PaginationResponse<PostDto>> Handle(SearchPostsRequest request, CancellationToken cancellationToken)
    {
        var spec = new PostsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}