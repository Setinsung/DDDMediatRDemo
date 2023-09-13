using MediatR;

namespace EFCorePublishEventTime.Events
{
    public record UserSoftDeletedEvent(Guid Id) : INotification;
}
