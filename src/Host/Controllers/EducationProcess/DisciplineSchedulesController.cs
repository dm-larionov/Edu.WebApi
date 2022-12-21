using Edu.WebApi.Application.EducationProcess.DisciplineSchedules;
using Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.GetByStudentGroupId;
using Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.GetByTeacherId;
using Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.GetForWeekByDate;
using Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class DisciplineSchedulesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.DisciplineSchedules)]
    [OpenApiOperation("Search discipline schedules using available filters.", "")]
    public async Task<PaginationResponse<DisciplineScheduleDto>> SearchAsync(SearchDisciplineSchedulesRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("student-groups/{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.DisciplineSchedules)]
    [OpenApiOperation("Get Discipline Schedule For Student Group For Week.", "")]
    public async Task<ICollection<DisciplineScheduleDto>> GetByStudentGroupAsync(int id, DateTime? date = null)
    {
        return await Mediator.Send(new GetScheduleByStudentGroupIdQuery() { StudentGroupId = id, Date = date ?? DateTime.Now });
    }

    [HttpGet("teachers/{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.DisciplineSchedules)]
    [OpenApiOperation("Get Discipline Schedule For Teacher For Week.", "")]
    public async Task<ICollection<DisciplineScheduleDto>> GetByTeacherAsync(int id, DateTime? date = null)
    {
        return await Mediator.Send(new GetScheduleByTeacherIdQuery() { TeacherId = id, Date = date ?? DateTime.Now });
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.DisciplineSchedules)]
    [OpenApiOperation("Get All Discipline Schedules", "")]
    public async Task<ICollection<DisciplineScheduleDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchDisciplineSchedulesByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.DisciplineSchedules)]
    [OpenApiOperation("Get Discipline Schedule details.", "")]
    public async Task<GetDisciplineScheduleByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetDisciplineScheduleByIdQuery() { Id = id });
    }

    [HttpGet("week/{date:datetime}")]
    [MustHavePermission(EduAction.View, EduResource.DisciplineSchedules)]
    [OpenApiOperation("Get Discipline Schedule details.", "")]
    public async Task<ICollection<DisciplineScheduleDto>> GetAllForWeekByDateAsync(DateTime date)
    {
        return await Mediator.Send(new GetScheduleForWeekByDateQuery() { Date = date });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.DisciplineSchedules)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditDisciplineScheduleCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.DisciplineSchedules)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditDisciplineScheduleCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.DisciplineSchedules)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteDisciplineScheduleCommand() { Id = id });
    }
}
