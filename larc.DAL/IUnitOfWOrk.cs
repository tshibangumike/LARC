using larc.DAL.Core;
using System;

namespace larc.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IEventRepository Events { get; }
        IEventTypeRepository EventTypes { get; }
        IMinistryRepository Ministries { get; }
        IPodcastRepository Podcasts { get; }
        IPreacherRepository Preachers { get; }
        int SaveChanges();
    }
}
