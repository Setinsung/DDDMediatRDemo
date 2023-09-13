using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EFCorePublishEventTime
{
    public class BaseDbContext : DbContext
    {
        private readonly IMediator mediator;

        public BaseDbContext(DbContextOptions options,IMediator mediator) : base(options)
        {
            this.mediator = mediator;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new NotImplementedException("Don not call SaveChanges, please call SaveChangesAsync instead.");
        }

        public async override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default) // 在调用父类SaveChangesAsync保存更改前发布所有追踪实体类注册的所有领域事件
        {
            var domainEntities = this.ChangeTracker.Entries<IDomainEvents>()
                .Where(x => x.Entity.GetDomainEvents().Any());
            var domainEvents = domainEntities.SelectMany(x => x.Entity.GetDomainEvents()).ToList();
            domainEntities.ToList().ForEach(e => e.Entity.ClearDomainEvents()); // 清除事件
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent); // 发布消息
            }
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
