using MediatR;

namespace EFCorePublishEventTime.Events
{
    public class ModifyUserLogHandler : NotificationHandler<UserUpdatedEvent>
    {
        private readonly ILogger<ModifyUserLogHandler> logger;
        private readonly UserDbContext context;

        public ModifyUserLogHandler(ILogger<ModifyUserLogHandler> logger, UserDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }
        protected override async void Handle(UserUpdatedEvent notification)
        {
            //var user = notification.Item;
            var user = await context.Users.FindAsync(notification.Id);
            logger.LogInformation($"通知{user.Email}的信息被修改");
        }
    }
}
