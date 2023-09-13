using MediatR;

namespace EFCorePublishEventTime
{
    public interface IDomainEvents
    {
        IEnumerable<INotification> GetDomainEvents(); // 获取注册的领域事件
        void AddDomainEvent(INotification eventItem); // 注册领域事件
        void AddDomainEventIfAbsent(INotification eventItem); // 领域事件不存在时才注册
        void ClearDomainEvents(); // 清除注册的领域事件
    }
}
