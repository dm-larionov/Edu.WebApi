using Edu.WebApi.Shared.Events;

namespace Edu.WebApi.Application.Common.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}