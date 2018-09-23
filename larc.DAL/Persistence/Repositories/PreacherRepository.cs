using larc.DAL.Core;
using larc.Model;

namespace larc.DAL.Persistence.EntityConfiguration
{
    public class PreacherRepository : Repository<Preacher>, IPreacherRepository
    {
        public PreacherRepository(LarcContext context)
            : base(context)
        {
        }

        public LarcContext XDContext
        {
            get { return Context as LarcContext; }
        }
    }
}
