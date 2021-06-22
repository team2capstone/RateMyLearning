using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RateMyLearning.Data.Models;
using RateMyLearning.ViewModels;

namespace RateMyLearning.Pages.Account {
    public class RegisterModel : PageModel {
        private readonly rmldbContext _context;

        [BindProperty] public RegisterUserViewModel Users { get; set; }

        public RegisterModel(rmldbContext context) {
            _context = context;
        }

        public IActionResult OnGet() {
            return Page();
        }

        public async Task<IActionResult> OnPost() {
            if (!ModelState.IsValid) {
                return Page();
            }

            Users.Password = BCrypt.Net.BCrypt.HashPassword(Users.Password);

            // check for the current enrollment status of the new user
            if (Request.Form["UserType"] == "Student") {
                Users.TypeId = 1;
            }
            else if (Request.Form["UserType"] == "Faculty") {
                Users.TypeId = 2;
            } else {
                Users.TypeId = 3;
            }

            _context.Add(new Users {
                FirstName = Users.FirstName,
                LastName = Users.LastName,
                Password = Users.Password,
                Email = Users.Email,
                TypeId = Users.TypeId
            });
            await _context.SaveChangesAsync();

            // log in user and setup a session
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("_email");
            HttpContext.Session.SetString("_email", Users.Email);
            return RedirectToPage("/Index");
        }
    }
}