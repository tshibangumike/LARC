using larc.Model;
using System.Data.Entity.ModelConfiguration;

namespace larc.DAL.Persistence.EntityConfiguration
{
    public class EventTypeConfiguration : EntityTypeConfiguration<EventType>
    {
        public EventTypeConfiguration()
        {
            ToTable("EventType", "larc");
            HasKey(x => x.Id);
            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
