using System;
using System.Collections.Generic;

namespace RateMyLearning.Data
{
    public partial class Program
    {
        public Program()
        {
            Course = new HashSet<Course>();
            Review = new HashSet<Review>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public long InterestId { get; set; }

        public virtual Interest Interest { get; set; }
        public virtual ICollection<Course> Course { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
