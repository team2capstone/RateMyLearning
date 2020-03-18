using RateMyLearning.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyLearning.ViewModels {
    public class SchoolViewModel {
        public IEnumerable<School> Schools { get; set; }
    }
}
