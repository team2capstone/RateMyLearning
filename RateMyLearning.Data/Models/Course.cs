using System;
using System.Collections.Generic;

namespace RateMyLearning.Data.Models
{
    public partial class Course
    {
        public Course()
        {
            Review = new HashSet<Review>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ProgramId { get; set; }
        public string CourseCode { get; set; }


        public virtual Program Program { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
