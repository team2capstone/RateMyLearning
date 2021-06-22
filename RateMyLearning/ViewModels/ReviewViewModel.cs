using RateMyLearning.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyLearning.ViewModels {
    public class ReviewViewModel {
        public IEnumerable<Review> Reviews { get; set; }
    }
}
