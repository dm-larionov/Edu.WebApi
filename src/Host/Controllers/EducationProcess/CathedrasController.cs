using Edu.WebApi.Application.EducationProcess.Cathedras;
using Edu.WebApi.Application.EducationProcess.Cathedras.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.Cathedras.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.Cathedras.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.Cathedras.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.Cathedras.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.Cathedras.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class CathedrasController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.Cathedras)]
    [OpenApiOperation("Search cathedras using available filters.", "")]
    public async Task<PaginationResponse<CathedraDto>> SearchAsync(SearchCathedrasRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.Cathedras)]
    [OpenApiOperation("Get All Cathedras", "")]
    public async Task<List<GetAllCathedrasResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllCathedrasQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.Cathedras)]
    [OpenApiOperation("Get All Cathedras", "")]
    public async Task<ICollection<CathedraDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchCathedrasByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.Cathedras)]
    [OpenApiOperation("Get Cathedra details.", "")]
    public async Task<GetCathedraByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetCathedraByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.Cathedras)]
    [OpenApiOperation("Create a new Cathedra.", "")]
    public async Task<int> CreateAsync(AddEditCathedraCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.Cathedras)]
    [OpenApiOperation("Update a Cathedra.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditCathedraCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.Cathedras)]
    [OpenApiOperation("Delete a Cathedra.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteCathedraCommand() { Id = id });
    }
}
