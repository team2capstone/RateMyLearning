using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RateMyLearning.Data.Models;
using RateMyLearning.Services;
using RateMyLearning.ViewModels;

namespace RateMyLearning.Pages.Account {
    public class IndexModel : PageModel {
        private readonly rmldbContext _context;
        private readonly ISchoolService _schoolService;

        [BindProperty] public UserProfileViewModel UserData { get; set; }

        public IndexModel(ISchoolService schoolService, rmldbContext context) {
            _schoolService = schoolService;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync() {
            var currentSignedInUser = _schoolService.GetSignedInUserDetails(HttpContext);

            // user not signed in, can't access profile
            if (currentSignedInUser == null) {
                return RedirectToPage("/Index");
            }

            // get user info
            var findUser = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(currentSignedInUser.Email));

            // load data onto form
            UserData = new UserProfileViewModel();
            UserData.Id = findUser.Id;
            UserData.FirstName = findUser.FirstName;
            UserData.LastName = findUser.LastName;
            UserData.Email = findUser.Email;
            UserData.StudentId = findUser.StudentId;
            UserData.EmployeeId = findUser.EmployeeId;
            return Page();
        }

        public async Task<IActionResult> OnPost() {
            if (!ModelState.IsValid) {
                return Page();
            }

            var userToUpdate = await _context.Users.FindAsync(UserData.Id);

            if (userToUpdate == null) {
                return NotFound();
            }

            // update user info
            userToUpdate.Email = UserData.Email;
            userToUpdate.StudentId = UserData.StudentId;
            userToUpdate.EmployeeId = UserData.EmployeeId;
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }

        public IActionResult OnGetSignOut() {
            HttpContext.Session.Remove("_email");
            return RedirectToPage("/Index");
        }
    }
}