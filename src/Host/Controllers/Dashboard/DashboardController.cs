using Edu.WebApi.Application.Dashboard;

namespace Edu.WebApi.Host.Controllers.Dashboard;

public class DashboardController : VersionedApiController
{
    [HttpGet]
    [MustHavePermission(EduAction.View, EduResource.Dashboard)]
    [OpenApiOperation("Get statistics for the dashboard.", "")]
    public Task<StatsDto> GetAsync()
    {
        return Mediator.Send(new GetStatsRequest());
    }
}