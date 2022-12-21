using Edu.WebApi.Application.EducationProcess.AudienceTypes;
using Edu.WebApi.Application.EducationProcess.AudienceTypes.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.AudienceTypes.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.AudienceTypes.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.AudienceTypes.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.AudienceTypes.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.AudienceTypes.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class AudienceTypesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.Audiences)]
    [OpenApiOperation("Search audience types using available filters.", "")]
    public async Task<PaginationResponse<AudienceTypeDto>> SearchAsync(SearchAudienceTypesRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.AudienceTypes)]
    [OpenApiOperation("Get All Audience Types", "")]
    public async Task<List<GetAllAudienceTypesResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllAudienceTypesQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.AudienceTypes)]
    [OpenApiOperation("Get All Audience Types", "")]
    public async Task<ICollection<AudienceTypeDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchAudienceTypesByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.AudienceTypes)]
    [OpenApiOperation("Get Audience Type details.", "")]
    public async Task<GetAudienceTypeByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetAudienceTypeByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.AudienceTypes)]
    [OpenApiOperation("Create a new Audience Type.", "")]
    public async Task<int> CreateAsync(AddEditAudienceTypeCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.AudienceTypes)]
    [OpenApiOperation("Update a Audience Type.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditAudienceTypeCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.AudienceTypes)]
    [OpenApiOperation("Delete a Audience Type.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteAudienceTypeCommand() { Id = id });
    }
}
