using RateMyLearning.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyLearning.ViewModels {
    public class CreateReviewViewModel {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ProgramId { get; set; }
        public long CourseId { get; set; }
        public long SchoolId { get; set; }
        public int? Rating { get; set; }
    }
}
