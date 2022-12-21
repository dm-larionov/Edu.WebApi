using Edu.WebApi.Application.EducationProcess.ReceivedSpecialties;
using Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class ReceivedSpecialtiesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.ReceivedSpecialties)]
    [OpenApiOperation("Search received specialties using available filters.", "")]
    public async Task<PaginationResponse<ReceivedSpecialtyDto>> SearchAsync(SearchReceivedSpecialtiesRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.ReceivedSpecialties)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllReceivedSpecialtiesResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllReceivedSpecialtiesQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.ReceivedSpecialties)]
    [OpenApiOperation("Get All Received Specialties", "")]
    public async Task<ICollection<ReceivedSpecialtyDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchReceivedSpecialtiesByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.ReceivedSpecialties)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetReceivedSpecialtyByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetReceivedSpecialtyByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.ReceivedSpecialties)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditReceivedSpecialtyCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.ReceivedSpecialties)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditReceivedSpecialtyCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.ReceivedSpecialties)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteReceivedSpecialtyCommand() { Id = id });
    }
}
