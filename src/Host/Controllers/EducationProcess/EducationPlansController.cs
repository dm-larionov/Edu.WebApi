using Edu.WebApi.Application.EducationProcess.EducationPlans;
using Edu.WebApi.Application.EducationProcess.EducationPlans.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.EducationPlans.Commands.AddEducationPlanSemesterDiscipline;
using Edu.WebApi.Application.EducationProcess.EducationPlans.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.EducationPlans.Queries.Export;
using Edu.WebApi.Application.EducationProcess.EducationPlans.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.EducationPlans.Queries.GetAllEducationPlanDisciplineSemesters;
using Edu.WebApi.Application.EducationProcess.EducationPlans.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.EducationPlans.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.EducationPlans.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class EducationPlansController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.EducationPlans)]
    [OpenApiOperation("Search education plans using available filters.", "")]
    public async Task<PaginationResponse<EducationPlanDto>> SearchAsync(SearchEducationPlansRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.EducationPlans)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllEducationPlansResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllEducationPlansQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.EducationPlans)]
    [OpenApiOperation("Get All Education Plans", "")]
    public async Task<ICollection<EducationPlanDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchEducationPlansByIdRangeRequest() { Ids = ids });
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.EducationPlans)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetEducationPlanByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetEducationPlanByIdQuery() { Id = id });
    }

    [HttpGet("{id:int}/discipline-semesters")]
    [MustHavePermission(EduAction.View, EduResource.EducationPlans)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<List<GetAllEducationPlanDisciplineSemestersResponse>> GetEducationPlanDetailsAsync(int id)
    {
        return await Mediator.Send(new GetAllEducationPlanDisciplineSemestersQuery() { EducationPlanId = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.EducationPlans)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditEducationPlanCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPost("{id:int}/discipline-semesters")]
    [MustHavePermission(EduAction.Create, EduResource.EducationPlans)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<ActionResult<int>> CreateSemesterDisciplineAsync(AddEducationPlanDisciplineSemesterCommand request, int id)
    {
        return id != request.EducationPlanId
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.EducationPlans)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditEducationPlanCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.EducationPlans)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteEducationPlanCommand() { Id = id });
    }

    [HttpPost("{id:int}/export")]
    [MustHavePermission(EduAction.Export, EduResource.EducationPlans)]
    [OpenApiOperation("Export a education plan.", "")]
    public async Task<FileResult> ExportAsync(int id)
    {
        var result = await Mediator.Send(new ExportEducationPlansQuery() { EducationPlanId = id });
        return File(result, "application/octet-stream", "EducationPlanExport");
    }
}
