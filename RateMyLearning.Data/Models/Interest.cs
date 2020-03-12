using System;
using System.Collections.Generic;

namespace RateMyLearning.Data
{
    public partial class Interest
    {
        public Interest()
        {
            Program = new HashSet<Program>();
            Review = new HashSet<Review>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long SchoolId { get; set; }

        public virtual School School { get; set; }
        public virtual ICollection<Program> Program { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
