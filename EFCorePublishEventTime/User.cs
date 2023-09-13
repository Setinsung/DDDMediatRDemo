using EFCorePublishEventTime.Events;

namespace EFCorePublishEventTime
{
    public class User : BaseEntity
    {
        public Guid Id { get; init; }
        public string UserName { get; init; }
        public string Email { get; private set; }
        public string? NickName { get; private set; }
        public int? Age { get; private set; }
        public bool IsDeleted { get; private set; }
        private User()
        {
        }
        public User(string userName, string email)
        {
            this.Id = Guid.NewGuid();
            this.UserName = userName;
            this.Email = email;
            this.IsDeleted = false;
            AddDomainEvent(new UserAddedEvent(this));
        }
        public void ChangeEmail(string email)
        {
            this.Email = email;
            AddDomainEventIfAbsent(new UserUpdatedEvent(this.Id));
        }
        public void ChangeNickName(string? nickName)
        {
            this.NickName = nickName;
            AddDomainEventIfAbsent(new UserUpdatedEvent(this.Id));
        }

        public void ChangeAge(int? age)
        {
            this.Age = age;
            AddDomainEventIfAbsent(new UserUpdatedEvent(this.Id));
        }

        public void SoftDelete()    
        {
            this.IsDeleted = true;
            AddDomainEvent(new UserSoftDeletedEvent(this.Id));
        }

    }
}
