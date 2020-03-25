using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyLearning.ViewModels {
    public class RegisterUserViewModel {
        [Required(ErrorMessage = "Please provide a valid first name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please provide a valid last name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please provide a valid password.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be atleast 8 characters long.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please provide a valid email address.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public long TypeId { get; set; }
    }
}
