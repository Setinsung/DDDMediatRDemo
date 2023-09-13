using MediatR;

namespace EFCorePublishEventTime.Events
{
    public record UserUpdatedEvent(Guid Id) : INotification;
}
