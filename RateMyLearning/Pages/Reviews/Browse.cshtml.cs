using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RateMyLearning.Data.Models;
using RateMyLearning.Services;
using RateMyLearning.ViewModels;

namespace RateMyLearning.Pages.Reviews {
    public class BrowseModel : PageModel {
        private readonly rmldbContext _context;
        private readonly ISchoolService _schoolService;

        [BindProperty(SupportsGet = true)]
        public Review Review { get; set; }
        public ReviewViewModel ReviewData { get; set; }
        public SelectList Schools { get; set; }

        public string ErrorMessage { get; set; }

        public BrowseModel(ISchoolService schoolService, rmldbContext context) {
            _schoolService = schoolService;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(bool? saveChangesError = false) {
            // populate dropdownlists
            Schools = new SelectList(_context.School,
                "Id", "Name");

            // retrieve all of the users reviews
            ReviewData = new ReviewViewModel();
            ReviewData.Reviews = await _context.Review
                .Include(x => x.Program)
                .Include(x => x.Course)
                .Include(x => x.Users)
                .Include(x => x.Users.Type)
                .Where(x => x.UsersId == 1)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

            // check for any errors caused by delete/edit
            if (saveChangesError.GetValueOrDefault()) {
                ErrorMessage = "Delete failed. Try again";
            }
            return Page();
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

        public async Task<IActionResult> OnPostAsync() {
            // TODO: use ModelBinding instead of this. Values from the selectlist are coming back as '0'
            // for whaterver reason instead of the selected value.
            long programId = (long)Convert.ToDouble(Request.Form["program"]);
            long schoolId = (long)Convert.ToDouble(Request.Form["school"]);
            long courseId = (long)Convert.ToDouble(Request.Form["course"]);
            decimal rating = Convert.ToDecimal(Request.Form["rating"]);

            if (!ModelState.IsValid) {
                return Page();
            }

            // create course review
            _context.Review.Add(new Review {
                Description = Review.Description,
                ProgramId = programId,
                SchoolId = schoolId,
                CourseId = courseId,
                InterestId = 1,
                CampusId = 1,
                Rating = rating,
                UsersId = 1
            });
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostElectiveAsync() {
            // TODO: use ModelBinding instead of this. Values from the selectlist are coming back as '0'
            // for whaterver reason instead of the selected value.
            long electiveId = (long)Convert.ToDouble(Request.Form["elective"]);
            decimal rating = Convert.ToDecimal(Request.Form["rating"]);

            if (!ModelState.IsValid) {
                return Page();
            }

            // find the selected elective's program and interest id
            var electives = await _context.Course
                .Include(i => i.Program)
                    .ThenInclude(i => i.Interest)
                .ToListAsync();
            var selectedElective = electives.FirstOrDefault(s => s.Id == electiveId);

            // create elective review
            _context.Review.Add(new Review {
                Description = Review.Description,
                ProgramId = selectedElective.ProgramId,
                SchoolId = selectedElective.Program.Interest.SchoolId,
                CourseId = electiveId,
                InterestId = selectedElective.Program.InterestId,
                CampusId = 1,
                Rating = rating,
                UsersId = 1
            });
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}