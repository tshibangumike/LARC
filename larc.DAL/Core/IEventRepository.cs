using larc.Model;
using System;
using System.Collections.Generic;

namespace larc.DAL.Core
{
    public interface IEventRepository : IRepository<Event>
    {
        IEnumerable<object> GetEventsDto();
    }
}
