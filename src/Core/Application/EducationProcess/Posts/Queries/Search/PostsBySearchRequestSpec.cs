using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Posts.Queries.Search;

public class PostsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Post, PostDto>
{
    public PostsBySearchRequestSpec(SearchPostsRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}