using Edu.WebApi.Application.Catalog.Brands;

namespace Edu.WebApi.Host.Controllers.Catalog;

public class BrandsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.Brands)]
    [OpenApiOperation("Search brands using available filters.", "")]
    public async Task<PaginationResponse<BrandDto>> SearchAsync(SearchBrandsRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(EduAction.View, EduResource.Brands)]
    [OpenApiOperation("Get brand details.", "")]
    public async Task<BrandDto> GetAsync(Guid id)
    {
        return await Mediator.Send(new GetBrandRequest(id));
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.Brands)]
    [OpenApiOperation("Create a new brand.", "")]
    public async Task<Guid> CreateAsync(CreateBrandRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(EduAction.Update, EduResource.Brands)]
    [OpenApiOperation("Update a brand.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateBrandRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(EduAction.Delete, EduResource.Brands)]
    [OpenApiOperation("Delete a brand.", "")]
    public async Task<Guid> DeleteAsync(Guid id)
    {
        return await Mediator.Send(new DeleteBrandRequest(id));
    }

    [HttpPost("generate-random")]
    [MustHavePermission(EduAction.Generate, EduResource.Brands)]
    [OpenApiOperation("Generate a number of random brands.", "")]
    public async Task<string> GenerateRandomAsync(GenerateRandomBrandRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpDelete("delete-random")]
    [MustHavePermission(EduAction.Clean, EduResource.Brands)]
    [OpenApiOperation("Delete the brands generated with the generate-random call.", "")]
    [ApiConventionMethod(typeof(EduApiConventions), nameof(EduApiConventions.Search))]
    public async Task<string> DeleteRandomAsync()
    {
        return await Mediator.Send(new DeleteRandomBrandRequest());
    }
}