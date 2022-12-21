using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Posts.Queries.GetByIdRange;

public class PostsByIdRangeRequestSpec : EntitiesByBaseFilterSpec<Post, PostDto>
{
    public PostsByIdRangeRequestSpec(SearchPostsByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}