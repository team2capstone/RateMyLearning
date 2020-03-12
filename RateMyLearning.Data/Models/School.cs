using System;
using System.Collections.Generic;

namespace RateMyLearning.Data
{
    public partial class School
    {
        public School()
        {
            Campus = new HashSet<Campus>();
            Interest = new HashSet<Interest>();
            Review = new HashSet<Review>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Campus> Campus { get; set; }
        public virtual ICollection<Interest> Interest { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
