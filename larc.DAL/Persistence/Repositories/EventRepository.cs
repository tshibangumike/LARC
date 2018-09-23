using FluentDateTime;
using larc.DAL.Core;
using larc.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace larc.DAL.Persistence.EntityConfiguration
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(LarcContext context)
            : base(context)
        {
        }

        public LarcContext LarcContext
        {
            get { return Context as LarcContext; }
        }

        public IEnumerable<object> GetEventsDto()
        {
            var lastMonday = DateTime.Now.Previous(DayOfWeek.Monday);
            var nextMonday = DateTime.Now.Next(DayOfWeek.Monday);
            return Context
                .Events
                .AsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Description,
                    x.StartTime,
                    x.EndTime,
                    x.EventTypeId
                })
                .Where(x => x.StartTime >= lastMonday && x.EndTime < nextMonday)
                .OrderBy(x => x.EventTypeId).ThenBy(x => x.StartTime);
        }

    }
}
