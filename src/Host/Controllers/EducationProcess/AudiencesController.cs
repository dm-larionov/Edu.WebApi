using Edu.WebApi.Application.EducationProcess.Audiences;
using Edu.WebApi.Application.EducationProcess.Audiences.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.Audiences.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.Audiences.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.Audiences.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.Audiences.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.Audiences.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class AudiencesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.Audiences)]
    [OpenApiOperation("Search audiences using available filters.", "")]
    public async Task<PaginationResponse<AudienceDto>> SearchAsync(SearchAudiencesRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.Audiences)]
    [OpenApiOperation("Get All Audiences", "")]
    public async Task<List<GetAllAudiencesResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllAudiencesQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.Audiences)]
    [OpenApiOperation("Get All Audiences", "")]
    public async Task<ICollection<AudienceDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchAudiencesByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.Audiences)]
    [OpenApiOperation("Get Audience details.", "")]
    public async Task<GetAudienceByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetAudienceByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.Audiences)]
    [OpenApiOperation("Create a new Audience.", "")]
    public async Task<int> CreateAsync(AddEditAudienceCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.Audiences)]
    [OpenApiOperation("Update a Audience.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditAudienceCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.Audiences)]
    [OpenApiOperation("Delete a Audience.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteAudienceCommand() { Id = id });
    }
}
