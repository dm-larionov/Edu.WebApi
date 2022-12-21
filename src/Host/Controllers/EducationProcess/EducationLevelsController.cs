using Edu.WebApi.Application.EducationProcess.EducationLevels;
using Edu.WebApi.Application.EducationProcess.EducationLevels.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.EducationLevels.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.EducationLevels.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.EducationLevels.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.EducationLevels.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.EducationLevels.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class EducationLevelsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.EducationLevels)]
    [OpenApiOperation("Search education levels using available filters.", "")]
    public async Task<PaginationResponse<EducationLevelDto>> SearchAsync(SearchEducationLevelsRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.EducationLevels)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllEducationLevelsResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllEducationLevelsQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.EducationLevels)]
    [OpenApiOperation("Get All Education Levels", "")]
    public async Task<ICollection<EducationLevelDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchEducationLevelsByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.EducationLevels)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetEducationLevelByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetEducationLevelByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.EducationLevels)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditEducationLevelCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.EducationLevels)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditEducationLevelCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.EducationLevels)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteEducationLevelCommand() { Id = id });
    }
}
