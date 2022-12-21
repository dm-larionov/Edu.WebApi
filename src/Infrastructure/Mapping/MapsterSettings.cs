using Edu.WebApi.Application.Catalog.Products;
using Edu.WebApi.Domain.Catalog;
using Mapster;

namespace Edu.WebApi.Infrastructure.Mapping;

public class MapsterSettings
{
    public static void Configure()
    {
        // here we will define the type conversion / Custom-mapping
        // More details at https://github.com/MapsterMapper/Mapster/wiki/Custom-mapping

        // This one is actually not necessary as it's mapped by convention
        TypeAdapterConfig<Product, ProductDto>.NewConfig();
    }
}