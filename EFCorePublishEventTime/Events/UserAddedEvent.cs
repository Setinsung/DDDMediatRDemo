using MediatR;

namespace EFCorePublishEventTime.Events
{
    public record UserAddedEvent(User Item) : INotification;
}
