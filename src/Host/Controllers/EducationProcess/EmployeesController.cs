using Edu.WebApi.Application.EducationProcess.Employees;
using Edu.WebApi.Application.EducationProcess.Employees.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.Employees.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.Employees.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.Employees.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.Employees.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.Employees.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class EmployeesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.Employees)]
    [OpenApiOperation("Search employees using available filters.", "")]
    public async Task<PaginationResponse<EmployeeDto>> SearchAsync(SearchEmployeesRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.Employees)]
    [OpenApiOperation("Get All Employees", "")]
    public async Task<List<GetAllEmployeesResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllEmployeesQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.Employees)]
    [OpenApiOperation("Get All Employees", "")]
    public async Task<ICollection<EmployeeDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchEmployeesByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.Employees)]
    [OpenApiOperation("Get Employee details.", "")]
    public async Task<GetEmployeeByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetEmployeeByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.Employees)]
    [OpenApiOperation("Create a new Employee.", "")]
    public async Task<int> CreateAsync(AddEditEmployeeCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.Employees)]
    [OpenApiOperation("Update a Employee.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditEmployeeCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.Employees)]
    [OpenApiOperation("Delete a Employee.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteEmployeeCommand() { Id = id });
    }
}
