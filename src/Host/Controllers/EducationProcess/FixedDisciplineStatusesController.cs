using Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses;
using Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class FixedDisciplineStatusesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.FixedDisciplineStatuses)]
    [OpenApiOperation("Search fixed discipline statuses using available filters.", "")]
    public async Task<PaginationResponse<FixedDisciplineStatusDto>> SearchAsync(SearchFixedDisciplineStatusesRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.FixedDisciplineStatuses)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllFixedDisciplineStatusesResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllFixedDisciplineStatusesQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.FixedDisciplineStatuses)]
    [OpenApiOperation("Get All Fixed Discipline Statuses", "")]
    public async Task<ICollection<FixedDisciplineStatusDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchFixedDisciplineStatusesByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.FixedDisciplineStatuses)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetFixedDisciplineStatusByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetFixedDisciplineStatusByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.FixedDisciplineStatuses)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditFixedDisciplineStatusCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.FixedDisciplineStatuses)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditFixedDisciplineStatusCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.FixedDisciplineStatuses)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteFixedDisciplineStatusCommand() { Id = id });
    }
}
