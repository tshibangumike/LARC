using larc.Model;
using System.Data.Entity.ModelConfiguration;

namespace larc.DAL.Persistence.EntityConfiguration
{
    public class MinistryConfiguration : EntityTypeConfiguration<Ministry>
    {
        public MinistryConfiguration()
        {
            ToTable("Ministry", "larc");
            HasKey(x => x.Id);
            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
