using System;
using System.Collections.Generic;

namespace RateMyLearning.Data.Models
{
    public partial class Address
    {
        public Address()
        {
            Campus = new HashSet<Campus>();
        }

        public long Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public long CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Campus> Campus { get; set; }
    }
}
