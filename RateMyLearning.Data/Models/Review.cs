using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RateMyLearning.Data.Models
{
    public partial class Review
    {
        public long Id { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ProgramId { get; set; }
        public long CourseId { get; set; }
        public long SchoolId { get; set; }
        public decimal? Rating { get; set; }
        public long? CampusId { get; set; }
        public long? InterestId { get; set; }
        public long UsersId { get; set; }

        public virtual Campus Campus { get; set; }
        public virtual Course Course { get; set; }
        public virtual Interest Interest { get; set; }
        public virtual Program Program { get; set; }
        public virtual School School { get; set; }
        public virtual Users Users { get; set; }
    }
}
