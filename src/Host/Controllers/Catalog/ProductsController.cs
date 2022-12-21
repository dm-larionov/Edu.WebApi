using Edu.WebApi.Application.Catalog.Products;

namespace Edu.WebApi.Host.Controllers.Catalog;

public class ProductsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(EduAction.Search, EduResource.Products)]
    [OpenApiOperation("Search products using available filters.", "")]
    public async Task<PaginationResponse<ProductDto>> SearchAsync(SearchProductsRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(EduAction.View, EduResource.Products)]
    [OpenApiOperation("Get product details.", "")]
    public async Task<ProductDetailsDto> GetAsync(Guid id)
    {
        return await Mediator.Send(new GetProductRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(EduAction.View, EduResource.Products)]
    [OpenApiOperation("Get product details via dapper.", "")]
    public async Task<ProductDto> GetDapperAsync(Guid id)
    {
        return await Mediator.Send(new GetProductViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(EduAction.Create, EduResource.Products)]
    [OpenApiOperation("Create a new product.", "")]
    public async Task<Guid> CreateAsync(CreateProductRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(EduAction.Update, EduResource.Products)]
    [OpenApiOperation("Update a product.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateProductRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(EduAction.Delete, EduResource.Products)]
    [OpenApiOperation("Delete a product.", "")]
    public async Task<Guid> DeleteAsync(Guid id)
    {
        return await Mediator.Send(new DeleteProductRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(EduAction.Export, EduResource.Products)]
    [OpenApiOperation("Export a products.", "")]
    public async Task<FileResult> ExportAsync(ExportProductsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "ProductExports");
    }
}