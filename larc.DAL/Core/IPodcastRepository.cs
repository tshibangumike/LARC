using larc.Model;
using System;
using System.Collections.Generic;

namespace larc.DAL.Core
{
    public interface IPodcastRepository : IRepository<Podcast>
    {
        IEnumerable<object> GetLast10();
        object GetDto(Guid id);
    }
}
