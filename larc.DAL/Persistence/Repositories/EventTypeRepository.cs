using larc.DAL.Core;
using larc.Model;

namespace larc.DAL.Persistence.EntityConfiguration
{
    public class EventTypeRepository : Repository<EventType>, IEventTypeRepository
    {
        public EventTypeRepository(LarcContext context)
            : base(context)
        {
        }

        public LarcContext XDContext
        {
            get { return Context as LarcContext; }
        }
    }
}
