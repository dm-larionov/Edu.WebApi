using Edu.WebApi.Application.EducationProcess.IntermediateCertificationForms;
using Edu.WebApi.Application.EducationProcess.IntermediateCertificationForms.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.IntermediateCertificationForms.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.IntermediateCertificationForms.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.IntermediateCertificationForms.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.IntermediateCertificationForms.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.IntermediateCertificationForms.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class IntermediateCertificationFormsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.IntermediateCertificationForms)]
    [OpenApiOperation("Search intermediate certification forms using available filters.", "")]
    public async Task<PaginationResponse<IntermediateCertificationFormDto>> SearchAsync(SearchIntermediateCertificationFormsRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.IntermediateCertificationForms)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllIntermediateCertificationFormsResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllIntermediateCertificationFormsQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.IntermediateCertificationForms)]
    [OpenApiOperation("Get All Intermediate Certification Forms", "")]
    public async Task<ICollection<IntermediateCertificationFormDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchIntermediateCertificationFormsByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.IntermediateCertificationForms)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetIntermediateCertificationFormByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetIntermediateCertificationFormByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.IntermediateCertificationForms)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditIntermediateCertificationFormCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.IntermediateCertificationForms)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditIntermediateCertificationFormCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.IntermediateCertificationForms)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteIntermediateCertificationFormCommand() { Id = id });
    }
}
