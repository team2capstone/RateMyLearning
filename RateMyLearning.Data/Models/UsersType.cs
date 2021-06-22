using System;
using System.Collections.Generic;

namespace RateMyLearning.Data.Models
{
    public partial class UsersType
    {
        public UsersType()
        {
            Users = new HashSet<Users>();
        }

        public long TypeId { get; set; }
        public bool? IsStudent { get; set; }
        public bool? IsFaculty { get; set; }
        public bool? IsNeither { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
