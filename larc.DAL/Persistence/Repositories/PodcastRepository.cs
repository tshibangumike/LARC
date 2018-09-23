using larc.DAL.Core;
using larc.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace larc.DAL.Persistence.EntityConfiguration
{
    public class PodcastRepository : Repository<Podcast>, IPodcastRepository
    {
        public PodcastRepository(LarcContext context)
            : base(context)
        {
        }

        public LarcContext LarcContext
        {
            get { return Context as LarcContext; }
        }

        public object GetDto(Guid id)
        {
            return Context
                .Podcasts
                .AsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Date,
                    x.AddedOn,
                    x.Description,
                    x.GoogleDriveLink,
                    PreacherName = x.Preacher.FullName,
                    PreacherId = x.Preacher.Id,
                    PreacherTitle = x.Preacher.Title
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<object> GetLast10()
        {
            return Context
                .Podcasts
                .AsNoTracking()
                .Select(x=> new
                {
                    x.Id,
                    x.Title,
                    x.Date,
                    x.AddedOn,
                    x.Description,
                    x.GoogleDriveLink,
                    PreacherName = x.Preacher.FullName,
                    PreacherId = x.Preacher.Id,
                    PreacherTitle = x.Preacher.Title
                })
                .OrderBy(x => x.AddedOn).Take(10);
        }
    }
}
