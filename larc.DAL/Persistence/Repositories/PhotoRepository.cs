using larc.DAL.Core;
using larc.Model;

namespace larc.DAL.Persistence.EntityConfiguration
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        public PhotoRepository(LarcContext context)
            : base(context)
        {
        }

        public LarcContext XDContext
        {
            get { return Context as LarcContext; }
        }
    }
}
