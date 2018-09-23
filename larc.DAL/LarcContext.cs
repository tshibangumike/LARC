using larc.DAL.Persistence.EntityConfiguration;
using larc.Model;
using System.Data.Entity;

namespace larc.DAL
{
    public class LarcContext: DbContext
    {
        public LarcContext() : base("name=larcContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventType { get; set; }
        public virtual DbSet<Ministry> Ministries { get; set; }
        public virtual DbSet<Podcast> Podcasts { get; set; }
        public virtual DbSet<Preacher> Preachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EventConfiguration());
            modelBuilder.Configurations.Add(new EventTypeConfiguration());
            modelBuilder.Configurations.Add(new MinistryConfiguration());
            modelBuilder.Configurations.Add(new PodcastConfiguration());
            modelBuilder.Configurations.Add(new PreacherConfiguration());

        }
    }
}
