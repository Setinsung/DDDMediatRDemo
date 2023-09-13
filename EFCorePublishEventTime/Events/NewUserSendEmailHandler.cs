using MediatR;

namespace EFCorePublishEventTime.Events
{
    public class NewUserSendEmailHandler : NotificationHandler<UserAddedEvent>
    {
        private readonly ILogger<NewUserSendEmailHandler> logger;

        public NewUserSendEmailHandler(ILogger<NewUserSendEmailHandler> logger)
        {
            this.logger = logger;
        }

        protected override void Handle(UserAddedEvent notification)
        {
            var user = notification.Item;
            logger.LogInformation($"向{user.Email}发送欢迎邮件");
        }
    }
}
