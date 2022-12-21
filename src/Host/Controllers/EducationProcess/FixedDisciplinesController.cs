using Edu.WebApi.Application.EducationProcess.FixedDisciplines;
using Edu.WebApi.Application.EducationProcess.FixedDisciplines.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.FixedDisciplines.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.FixedDisciplines.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.FixedDisciplines.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.FixedDisciplines.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.FixedDisciplines.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class FixedDisciplinesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.FixedDisciplines)]
    [OpenApiOperation("Search fixed disciplines using available filters.", "")]
    public async Task<PaginationResponse<FixedDisciplineDto>> SearchAsync(SearchFixedDisciplinesRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.FixedDisciplines)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllFixedDisciplinesResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllFixedDisciplinesQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.FixedDisciplines)]
    [OpenApiOperation("Get All Fixed Disciplines", "")]
    public async Task<ICollection<FixedDisciplineDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchFixedDisciplinesByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.FixedDisciplines)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetFixedDisciplineByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetFixedDisciplineByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.FixedDisciplines)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditFixedDisciplineCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.FixedDisciplines)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditFixedDisciplineCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.FixedDisciplines)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteFixedDisciplineCommand() { Id = id });
    }
}
