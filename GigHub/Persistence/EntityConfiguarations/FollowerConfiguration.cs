using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfiguarations
{
    public class FollowerConfiguration : EntityTypeConfiguration<Following>
    {
        public FollowerConfiguration()
        {
            HasKey(f => new { f.FollowerId, f.FolloweeId });
        }
    }
}
