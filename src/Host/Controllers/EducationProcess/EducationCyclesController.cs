using Edu.WebApi.Application.EducationProcess.EducationCycles;
using Edu.WebApi.Application.EducationProcess.EducationCycles.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.EducationCycles.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.EducationCycles.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.EducationCycles.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.EducationCycles.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.EducationCycles.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class EducationCyclesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.EducationCycles)]
    [OpenApiOperation("Search education cycles using available filters.", "")]
    public async Task<PaginationResponse<EducationCycleDto>> SearchAsync(SearchEducationCyclesRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.EducationCycles)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllEducationCyclesResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllEducationCyclesQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.EducationCycles)]
    [OpenApiOperation("Get All Education Cycles", "")]
    public async Task<ICollection<EducationCycleDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchEducationCyclesByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.EducationCycles)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetEducationCycleByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetEducationCycleByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.EducationCycles)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditEducationCycleCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.EducationCycles)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditEducationCycleCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.EducationCycles)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteEducationCycleCommand() { Id = id });
    }
}
