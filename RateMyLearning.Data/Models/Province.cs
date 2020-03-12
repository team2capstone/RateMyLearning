using System;
using System.Collections.Generic;

namespace RateMyLearning.Data
{
    public partial class Province
    {
        public Province()
        {
            City = new HashSet<City>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<City> City { get; set; }
    }
}
