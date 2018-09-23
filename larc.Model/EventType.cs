using System.Collections.Generic;

namespace larc.Model
{
    public partial class EventType
    {
        public EventType()
        {
            this.Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
