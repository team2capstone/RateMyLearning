using System;
using System.Collections.Generic;

namespace RateMyLearning.Data.Models
{
    public partial class Campus
    {
        public Campus()
        {
            Review = new HashSet<Review>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long AddressId { get; set; }
        public long SchoolId { get; set; }

        public virtual Address Address { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
