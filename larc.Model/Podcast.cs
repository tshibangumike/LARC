using System;

namespace larc.Model
{
    public partial class Podcast
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Nullable<Guid> PreacherId { get; set; }
        public string GoogleDriveLink { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual Preacher Preacher { get; set; }
    }
}
