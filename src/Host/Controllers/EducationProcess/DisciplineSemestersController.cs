using Edu.WebApi.Application.EducationProcess.DisciplineSemesters;
using Edu.WebApi.Application.EducationProcess.DisciplineSemesters.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.DisciplineSemesters.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.DisciplineSemesters.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.DisciplineSemesters.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.DisciplineSemesters.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.DisciplineSemesters.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class DisciplineSemestersController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.DisciplineSemesters)]
    [OpenApiOperation("Search discipline semesters using available filters.", "")]
    public async Task<PaginationResponse<DisciplineSemesterDto>> SearchAsync(SearchDisciplineSemestersRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.DisciplineSemesters)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllDisciplineSemestersResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllDisciplineSemestersQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.DisciplineSemesters)]
    [OpenApiOperation("Get All Discipline Semesters", "")]
    public async Task<ICollection<DisciplineSemesterDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchDisciplineSemestersByIdRangeRequest() { Ids = ids });
    }


    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.DisciplineSemesters)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetDisciplineSemesterByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetDisciplineSemesterByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.DisciplineSemesters)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditDisciplineSemesterCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.DisciplineSemesters)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditDisciplineSemesterCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.DisciplineSemesters)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteDisciplineSemesterCommand() { Id = id });
    }
}
