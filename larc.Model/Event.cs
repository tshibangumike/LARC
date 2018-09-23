using System;

namespace larc.Model
{
    public partial class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int EventTypeId { get; set; }
        public string GoogleAttachmentId { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual EventType EventType { get; set; }
    }
}
