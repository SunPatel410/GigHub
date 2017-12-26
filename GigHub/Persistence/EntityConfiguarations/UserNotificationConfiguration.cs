using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfiguarations
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            HasRequired(n => n.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);
        }
    }
}
