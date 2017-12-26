using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfiguarations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasKey(a => new { a.GigId, a.AttendeeId });
        }
    }
}
