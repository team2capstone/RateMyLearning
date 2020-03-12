using System;
using System.Collections.Generic;

namespace RateMyLearning.Data
{
    public partial class City
    {
        public City()
        {
            Address = new HashSet<Address>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? ProvinceId { get; set; }

        public virtual Province Province { get; set; }
        public virtual ICollection<Address> Address { get; set; }
    }
}
