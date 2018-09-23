using larc.Model;
using System.Data.Entity.ModelConfiguration;

namespace larc.DAL.Persistence.EntityConfiguration
{
    public class PreacherConfiguration : EntityTypeConfiguration<Preacher>
    {
        public PreacherConfiguration()
        {
            ToTable("Preacher", "larc");
            HasKey(x => x.Id);
            Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(150);
            HasOptional(x => x.Ministry)
                .WithMany(x => x.Preachers)
                .HasForeignKey(x => x.MinistryId)
                .WillCascadeOnDelete(false);
        }
    }
}
