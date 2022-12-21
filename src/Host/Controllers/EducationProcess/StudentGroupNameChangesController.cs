using Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges;
using Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class StudentGroupNameChangesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.StudentGroupNameChanges)]
    [OpenApiOperation("Search student group name changes using available filters.", "")]
    public async Task<PaginationResponse<StudentGroupNameChangeDto>> SearchAsync(SearchStudentGroupNameChangesRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.StudentGroupNameChanges)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllStudentGroupNameChangesResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllStudentGroupNameChangesQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.StudentGroupNameChanges)]
    [OpenApiOperation("Get All StudentGroupNameChanges", "")]
    public async Task<ICollection<StudentGroupNameChangeDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchStudentGroupNameChangesByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.StudentGroupNameChanges)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetStudentGroupNameChangeByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetStudentGroupNameChangeByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.StudentGroupNameChanges)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditStudentGroupNameChangeCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.StudentGroupNameChanges)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditStudentGroupNameChangeCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.StudentGroupNameChanges)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteStudentGroupNameChangeCommand() { Id = id });
    }
}
