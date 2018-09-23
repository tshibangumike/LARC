using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace larc.Model
{
    using System;
    using System.Collections.Generic;

    public partial class Ministry
    {
        public Ministry()
        {
            this.Preachers = new HashSet<Preacher>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Preacher> Preachers { get; set; }
    }
}
