using Edu.WebApi.Application.EducationProcess.StudentGroups;
using Edu.WebApi.Application.EducationProcess.StudentGroups.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.StudentGroups.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.StudentGroups.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.StudentGroups.Queries.GetAllExisting;
using Edu.WebApi.Application.EducationProcess.StudentGroups.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.StudentGroups.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.StudentGroups.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class StudentGroupsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.StudentGroups)]
    [OpenApiOperation("Search student groups using available filters.", "")]
    public async Task<PaginationResponse<StudentGroupDto>> SearchAsync(SearchStudentGroupsRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.StudentGroups)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllStudentGroupsResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllStudentGroupsQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.StudentGroups)]
    [OpenApiOperation("Get All StudentGroups", "")]
    public async Task<ICollection<StudentGroupDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchStudentGroupsByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("existing/{date:datetime}")]
    [MustHavePermission(EduAction.View, EduResource.StudentGroups)]
    [OpenApiOperation("Get All Existing Student Groups", "")]
    public async Task<List<GetAllExistingStudentGroupsResponse>> GetAllExistingAsync(DateTime date)
    {
        return await Mediator.Send(new GetAllExistingStudentGroupsQuery() { Date = date });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.StudentGroups)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetStudentGroupByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetStudentGroupByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.StudentGroups)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditStudentGroupCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.StudentGroups)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditStudentGroupCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.StudentGroups)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteStudentGroupCommand() { Id = id });
    }
}
