using Edu.WebApi.Application.EducationProcess.EducationForms;
using Edu.WebApi.Application.EducationProcess.EducationForms.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.EducationForms.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.EducationForms.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.EducationForms.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.EducationForms.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.EducationForms.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class EducationFormsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.EducationForms)]
    [OpenApiOperation("Search education forms using available filters.", "")]
    public async Task<PaginationResponse<EducationFormDto>> SearchAsync(SearchEducationFormsRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.EducationForms)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllEducationFormsResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllEducationFormsQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.EducationForms)]
    [OpenApiOperation("Get All Education Forms", "")]
    public async Task<ICollection<EducationFormDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchEducationFormsByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.EducationForms)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetEducationFormByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetEducationFormByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.EducationForms)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditEducationFormCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.EducationForms)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditEducationFormCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.EducationForms)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteEducationFormCommand() { Id = id });
    }
}
