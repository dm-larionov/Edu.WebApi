using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Posts.Queries.GetByIdRange;

public class SearchPostsByIdRangeRequest : BaseFilter, IRequest<ICollection<PostDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchPostsByIdRangeRequestHandler : IRequestHandler<SearchPostsByIdRangeRequest, ICollection<PostDto>>
{
    private readonly IReadAudRepository<Post> _repository;

    public SearchPostsByIdRangeRequestHandler(IReadAudRepository<Post> repository) => _repository = repository;

    public async Task<ICollection<PostDto>> Handle(SearchPostsByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new PostsByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}