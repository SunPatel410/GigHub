using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfiguarations
{
    public class GigConfiguration : EntityTypeConfiguration<Gig>
    {
        public GigConfiguration()
        {

            //Key Configurations
            //Properties Configurations
            Property(g => g.ArtistId)
            .IsRequired();

            Property(g => g.GenreId)
            .IsRequired();

            Property(g => g.Venue)
            .IsRequired()
            .HasMaxLength(255);


            //Relational Configurations
            HasMany(g => g.Attendances)
                .WithRequired(a => a.Gig)
                .WillCascadeOnDelete(false);

        }
    }
}
