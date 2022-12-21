using Edu.WebApi.Application.EducationProcess.ReceivedEducations;
using Edu.WebApi.Application.EducationProcess.ReceivedEducations.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.ReceivedEducations.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.ReceivedEducations.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.ReceivedEducations.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.ReceivedEducations.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.ReceivedEducations.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class ReceivedEducationsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.ReceivedEducations)]
    [OpenApiOperation("Search received educations using available filters.", "")]
    public async Task<PaginationResponse<ReceivedEducationDto>> SearchAsync(SearchReceivedEducationsRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.ReceivedEducations)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllReceivedEducationsResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllReceivedEducationsQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.ReceivedEducations)]
    [OpenApiOperation("Get All ReceivedEducations", "")]
    public async Task<ICollection<ReceivedEducationDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchReceivedEducationsByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.ReceivedEducations)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetReceivedEducationByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetReceivedEducationByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.ReceivedEducations)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditReceivedEducationCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.ReceivedEducations)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditReceivedEducationCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.ReceivedEducations)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteReceivedEducationCommand() { Id = id });
    }
}
