using larc.Model;
using System.Data.Entity.ModelConfiguration;

namespace larc.DAL.Persistence.EntityConfiguration
{
    public class PhotoConfiguration : EntityTypeConfiguration<Photo>
    {
        public PhotoConfiguration()
        {
            ToTable("Photo", "larc");
            HasKey(x => x.Id);
        }
    }
}
