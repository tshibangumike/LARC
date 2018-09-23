using larc.DAL.Core;
using larc.Model;

namespace larc.DAL.Persistence.EntityConfiguration
{
    public class MinistryRepository : Repository<Ministry>, IMinistryRepository
    {
        public MinistryRepository(LarcContext context)
            : base(context)
        {
        }

        public LarcContext XDContext
        {
            get { return Context as LarcContext; }
        }
    }
}
