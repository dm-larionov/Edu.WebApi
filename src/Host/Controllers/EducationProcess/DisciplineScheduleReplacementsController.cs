using Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements;
using Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class DisciplineScheduleReplacementsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.DisciplineScheduleReplacements)]
    [OpenApiOperation("Search discipline schedule replacements using available filters.", "")]
    public async Task<PaginationResponse<DisciplineScheduleReplacementDto>> SearchAsync(SearchDisciplineScheduleReplacementsRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.DisciplineScheduleReplacements)]
    [OpenApiOperation("Get All Discipline Schedule Replacements", "")]
    public async Task<List<GetAllDisciplineScheduleReplacementsResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllDisciplineScheduleReplacementsQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.DisciplineScheduleReplacements)]
    [OpenApiOperation("Get All Discipline Schedule Replacements", "")]
    public async Task<ICollection<DisciplineScheduleReplacementDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchDisciplineScheduleReplacementsByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.DisciplineScheduleReplacements)]
    [OpenApiOperation("Get Discipline Schedule Replacement details.", "")]
    public async Task<GetDisciplineScheduleReplacementByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetDisciplineScheduleReplacementByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.DisciplineScheduleReplacements)]
    [OpenApiOperation("Create a new Discipline Schedule Replacement.", "")]
    public async Task<int> CreateAsync(AddEditDisciplineScheduleReplacementCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.DisciplineScheduleReplacements)]
    [OpenApiOperation("Update a Discipline Schedule Replacement.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditDisciplineScheduleReplacementCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.DisciplineScheduleReplacements)]
    [OpenApiOperation("Delete a Discipline Schedule Replacement.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteDisciplineScheduleReplacementCommand() { Id = id });
    }
}
