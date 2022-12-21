using Edu.WebApi.Application.EducationProcess.Disciplines;
using Edu.WebApi.Application.EducationProcess.Disciplines.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.Disciplines.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.Disciplines.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.Disciplines.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.Disciplines.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.Disciplines.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class DisciplinesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.Disciplines)]
    [OpenApiOperation("Search disciplines using available filters.", "")]
    public async Task<PaginationResponse<DisciplineDto>> SearchAsync(SearchDisciplinesRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.Disciplines)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllDisciplinesResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllDisciplinesQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.Disciplines)]
    [OpenApiOperation("Get All Disciplines", "")]
    public async Task<ICollection<DisciplineDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchDisciplinesByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.Disciplines)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetDisciplineByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetDisciplineByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.Disciplines)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditDisciplineCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.Disciplines)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditDisciplineCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.Disciplines)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteDisciplineCommand() { Id = id });
    }
}
