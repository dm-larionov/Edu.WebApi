using Edu.WebApi.Application.EducationProcess.ReceivedEducationForms;
using Edu.WebApi.Application.EducationProcess.ReceivedEducationForms.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.ReceivedEducationForms.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.ReceivedEducationForms.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.ReceivedEducationForms.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.ReceivedEducationForms.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.ReceivedEducationForms.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class ReceivedEducationFormsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.ReceivedEducationForms)]
    [OpenApiOperation("Search received education forms using available filters.", "")]
    public async Task<PaginationResponse<ReceivedEducationFormDto>> SearchAsync(SearchReceivedEducationFormsRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.ReceivedEducationForms)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllReceivedEducationFormsResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllReceivedEducationFormsQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.ReceivedEducationForms)]
    [OpenApiOperation("Get All Received Education Forms", "")]
    public async Task<ICollection<ReceivedEducationFormDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchReceivedEducationFormsByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.ReceivedEducationForms)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetReceivedEducationFormByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetReceivedEducationFormByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.ReceivedEducationForms)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditReceivedEducationFormCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.ReceivedEducationForms)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditReceivedEducationFormCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.ReceivedEducationForms)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteReceivedEducationFormCommand() { Id = id });
    }
}
