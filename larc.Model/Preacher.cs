using System;
using System.Collections.Generic;

namespace larc.Model
{
    public class Preacher
    {
        public Preacher()
        {
            this.Podcasts = new HashSet<Podcast>();
        }

        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public Nullable<Guid> MinistryId { get; set; }

        public virtual Ministry Ministry { get; set; }
        public virtual ICollection<Podcast> Podcasts { get; set; }
    }
}
