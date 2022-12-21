using Edu.WebApi.Application.EducationProcess.EducationModules;
using Edu.WebApi.Application.EducationProcess.EducationModules.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.EducationModules.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.EducationModules.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.EducationModules.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.EducationModules.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.EducationModules.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class EducationModulesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.EducationModules)]
    [OpenApiOperation("Search education modules using available filters.", "")]
    public async Task<PaginationResponse<EducationModuleDto>> SearchAsync(SearchEducationModulesRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.EducationModules)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllEducationModulesResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllEducationModulesQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.EducationModules)]
    [OpenApiOperation("Get All Education Modules", "")]
    public async Task<ICollection<EducationModuleDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchEducationModulesByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.EducationModules)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetEducationModuleByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetEducationModuleByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.EducationModules)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditEducationModuleCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.EducationModules)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditEducationModuleCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.EducationModules)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteEducationModuleCommand() { Id = id });
    }
}
