using larc.Model;
using System.Data.Entity.ModelConfiguration;

namespace larc.DAL.Persistence.EntityConfiguration
{
    public class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
        {
            ToTable("Event", "larc");
            HasKey(x => x.Id);
            Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(250);
            HasRequired(x => x.EventType)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.EventTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
