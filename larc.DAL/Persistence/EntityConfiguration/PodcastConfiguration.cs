using larc.Model;
using System.Data.Entity.ModelConfiguration;

namespace larc.DAL.Persistence.EntityConfiguration
{
    public class PodcastConfiguration : EntityTypeConfiguration<Podcast>
    {
        public PodcastConfiguration()
        {
            ToTable("Podcast", "larc");
            HasKey(x => x.Id);
            Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(250);
            HasRequired(x => x.Preacher)
                .WithMany(x => x.Podcasts)
                .HasForeignKey(x => x.PreacherId)
                .WillCascadeOnDelete(false);
        }
    }
}
