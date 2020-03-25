using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RateMyLearning.Data.Models;
using RateMyLearning.Services;
using RateMyLearning.ViewModels;

namespace RateMyLearning.Pages.Reviews {
    public class IndexModel : PageModel {
        private readonly rmldbContext _context;
        private readonly ISchoolService _schoolService;

        [BindProperty(SupportsGet = true)] public Review Review { get; set; }
        public ReviewViewModel ReviewData { get; set; }
        public SelectList Schools { get; set; }
        public string ErrorNotSignedIn { get; set; }

        public IndexModel(ISchoolService schoolService, rmldbContext context) {
            _schoolService = schoolService;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync() {
            // populate dropdownlists
            Schools = new SelectList(_context.School,
                "Id", "Name");

            var currentSignedInUser = _schoolService.GetSignedInUserDetails(HttpContext);

            // user not signed in
            if (currentSignedInUser == null) {
                return Page();
            }

            // get all of the reviews they have made
            ReviewData = new ReviewViewModel();
            ReviewData.Reviews = await _context.Review
                .Include(x => x.Program)
                .Include(x => x.Course)
                .Include(x => x.Users)
                .Include(x => x.Users.Type)
                .Where(x => x.UsersId == currentSignedInUser.Id)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();
            return Page();
        }

        // Delete a review
        public async Task<IActionResult> OnGetDeleteAsync(long? id) {
            if (id == null) {
                return NotFound();
            }

            // find review
            var review = await _context.Review.FindAsync(id);

            // review not found
            if (Review == null) {
                return NotFound();
            }

            // delete review
            try {
                _context.Review.Remove(review);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException) {
                return RedirectToAction("./Index",
                     new { id, saveChangesError = true }); ;
            }
        }

        // Create a review
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            var currentSignedInUser = _schoolService.GetSignedInUserDetails(HttpContext);

            // user not signed in
            if (currentSignedInUser == null) {
                ErrorNotSignedIn = "Please <a href='/Account/SignIn'>sign in</a> to create a review.";
                return Page();
            }

            // insert review
            _context.Review.Add(new Review {
                Description = Review.Description,
                ProgramId = (long)Convert.ToDouble(Request.Form["program"]),
                SchoolId = (long)Convert.ToDouble(Request.Form["school"]),
                CourseId = (long)Convert.ToDouble(Request.Form["course"]),
                InterestId = 1,
                CampusId = 1,
                Rating = Convert.ToDecimal(Request.Form["rating"]),
                UsersId = currentSignedInUser.Id
            });
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        // Create a review for an elective course
        public async Task<IActionResult> OnPostElectiveAsync() {
            long electiveId = (long)Convert.ToDouble(Request.Form["elective"]);

            if (!ModelState.IsValid) {
                return Page();
            }

            var currentSignedInUser = _schoolService.GetSignedInUserDetails(HttpContext);

            // user not signed in
            if (currentSignedInUser == null) {
                ErrorNotSignedIn = "Please <a href='/Account/SignIn'>sign in</a> to create a review.";
                return Page();
            }

            // find the selected elective's program and interest id
            var electives = await _context.Course
                .Include(i => i.Program)
                    .ThenInclude(i => i.Interest)
                .ToListAsync();
            var selectedElective = electives.FirstOrDefault(s => s.Id == electiveId);

            // insert elective review
            _context.Review.Add(new Review {
                Description = Review.Description,
                ProgramId = selectedElective.ProgramId,
                SchoolId = selectedElective.Program.Interest.SchoolId,
                CourseId = electiveId,
                InterestId = selectedElective.Program.InterestId,
                CampusId = 1,
                Rating = Convert.ToDecimal(Request.Form["elective_rating"]),
                UsersId = currentSignedInUser.Id
            });
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        // Create a review for a continuing education program/course
        public async Task<IActionResult> OnPostContinuingEducationAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            var currentSignedInUser = _schoolService.GetSignedInUserDetails(HttpContext);

            // user not signed in
            if (currentSignedInUser == null) {
                ErrorNotSignedIn = "Please <a href='/Account/SignIn'>sign in</a> to create a review.";
                return Page();
            }

            // insert review
            _context.Review.Add(new Review {
                Description = Review.Description,
                ProgramId = (long)Convert.ToDouble(Request.Form["continuing-education-program"]),
                SchoolId = (long)Convert.ToDouble(Request.Form["continuing-education-school"]),
                CourseId = (long)Convert.ToDouble(Request.Form["continuing-education-course"]),
                InterestId = 1,
                CampusId = 1,
                Rating = Convert.ToDecimal(Request.Form["continuing_education_rating"]),
                UsersId = currentSignedInUser.Id
            });
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public JsonResult OnGetPrograms() {
            return new JsonResult(_schoolService.GetPrograms());
        }

        public JsonResult OnGetCourses() {
            return new JsonResult(_schoolService.GetCourses(Review.ProgramId));
        }

        public JsonResult OnGetElectives() {
            return new JsonResult(_schoolService.GetElectives());
        }

        public JsonResult OnGetContinuingEducationPrograms() {
            return new JsonResult(_schoolService.GetContinuingEducationPrograms());
        }

        public JsonResult OnGetContinuingEducationCourses() {
            return new JsonResult(_schoolService.GetContinuingEducationCourses(Review.ProgramId));
        }
    }
}