using RateMyLearning.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyLearning.ViewModels {
    public class UserProfileViewModel {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please provide a valid email address.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public long? StudentId { get; set; }
        public long? EmployeeId { get; set; }
    }
}
