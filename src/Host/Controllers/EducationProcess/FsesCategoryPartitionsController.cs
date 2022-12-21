using Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions;
using Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions.Commands.AddEdit;
using Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions.Commands.Delete;
using Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions.Queries.GetAll;
using Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions.Queries.GetById;
using Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions.Queries.GetByIdRange;
using Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions.Queries.Search;

namespace Edu.WebApi.Host.Controllers.EducationProcess;

public class FsesCategoryPartitionsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.FsesCategoryPartitions)]
    [OpenApiOperation("Search fses category partitions using available filters.", "")]
    public async Task<PaginationResponse<FsesCategoryPartitionDto>> SearchAsync(SearchFsesCategoryPartitionsRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.FsesCategoryPartitions)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<List<GetAllFsesCategoryPartitionsResponse>> GetAllAsync()
    {
        return await Mediator.Send(new GetAllFsesCategoryPartitionsQuery());
    }

    [HttpGet("range")]
    [MustHavePermission(EduAction.View, EduResource.FsesCategoryPartitions)]
    [OpenApiOperation("Get All Academic Years", "")]
    public async Task<ICollection<FsesCategoryPartitionDto>> GetAllByIdRangeAsync([FromQuery] ICollection<int> ids)
    {
        return await Mediator.Send(new SearchFsesCategoryPartitionsByIdRangeRequest() { Ids = ids});
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(EduAction.View, EduResource.FsesCategoryPartitions)]
    [OpenApiOperation("Get Academic Year details.", "")]
    public async Task<GetFsesCategoryPartitionByIdResponse> GetAsync(int id)
    {
        return await Mediator.Send(new GetFsesCategoryPartitionByIdQuery() { Id = id });
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.FsesCategoryPartitions)]
    [OpenApiOperation("Create a new Academic Year.", "")]
    public async Task<int> CreateAsync(AddEditFsesCategoryPartitionCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(EduAction.Update, EduResource.FsesCategoryPartitions)]
    [OpenApiOperation("Update a Academic Year.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AddEditFsesCategoryPartitionCommand request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(EduAction.Delete, EduResource.FsesCategoryPartitions)]
    [OpenApiOperation("Delete a Academic Year.", "")]
    public async Task<int> DeleteAsync(int id)
    {
        return await Mediator.Send(new DeleteFsesCategoryPartitionCommand() { Id = id });
    }
}
