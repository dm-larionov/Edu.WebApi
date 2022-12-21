using Edu.WebApi.Application.EducationProcess.Posts;
using Edu.WebApi.Application.EducationProcess.Posts.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.Posts.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.Posts.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.Posts.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.Posts.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.Posts.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class PostsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.Posts)]
    [OpenApiOperation("Search posts using available filters.", "")]
    public async Task<PaginationResponse<PostDto>> SearchAsync(SearchPostsRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.Posts)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllPostsResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllPostsQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.Posts)]
    [OpenApiOperation("Get All Posts", "")]
    public async Task<ICollection<PostDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchPostsByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.Posts)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetPostByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetPostByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.Posts)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditPostCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.Posts)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditPostCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.Posts)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeletePostCommand() { Id = id });
    }
}
