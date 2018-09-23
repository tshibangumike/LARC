using larc.DAL.Core;
using larc.DAL.Persistence.EntityConfiguration;

namespace larc.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LarcContext _context;

        public UnitOfWork(LarcContext context)
        {
            _context = context;
            Events = new EventRepository(_context);
            EventTypes = new EventTypeRepository(_context);
            Ministries = new MinistryRepository(_context);
            Podcasts = new PodcastRepository(_context);
            Preachers = new PreacherRepository(_context);
        }

        public IEventRepository Events { get; private set; }
        public IEventTypeRepository EventTypes { get; private set; }
        public IMinistryRepository Ministries { get; private set; }
        public IPodcastRepository Podcasts { get; private set; }
        public IPreacherRepository Preachers { get; private set; }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
