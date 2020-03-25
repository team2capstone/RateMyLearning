using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RateMyLearning.Data.Models;

namespace RateMyLearning.Pages.Account {
    public class SignInModel : PageModel {
        private readonly rmldbContext _context;
        [BindProperty] public Users Users { get; set; }
        public string ErrorInvalidLogin { get; set; }

        public SignInModel(rmldbContext context) {
            _context = context;
        }

        public IActionResult OnGet() {
            return Page();
        }

        public IActionResult OnPost() {
            if (!ModelState.IsValid) {
                return Page();
            }

            // verify the user's credentials setup a session
            var user = Login(Users.Email, Users.Password);
            if (user != null) {
                HttpContext.Session.Clear();
                HttpContext.Session.Remove("_email");
                HttpContext.Session.SetString("_email", Users.Email);
                return RedirectToPage("/Index");
            }

            // credentials were incorrect
            ErrorInvalidLogin = "Invalid email or password. Please try again.";
            return Page();
        }

        // Find the user by email and verify credentials
        private Users Login(string email, string password) {
            var user = _context.Users.SingleOrDefault(a => a.Email.Equals(email));
            if (user != null) {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password)) {
                    return user;
                }
            }
            return null;
        }
    }
}