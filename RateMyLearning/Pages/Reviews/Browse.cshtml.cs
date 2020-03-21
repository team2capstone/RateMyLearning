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

        public BrowseModel(ISchoolService schoolService, rmldbContext context) {
            _schoolService = schoolService;
            _context = context;
        }

        public IActionResult OnGet() {
            FillSchoolListItem();
            return Page();
        }

        public async Task<IActionResult> OnPostProgramsCoursesAsync() {
            var selectedCourse = (long)Convert.ToDouble(Request.Form["f-pc-course"]);

            ReviewData = new ReviewViewModel();
            ReviewData.Reviews = await _context.Review
                .Include(x => x.Program)
                .Include(x => x.Course)
                .Include(x => x.Users)
                .Include(x => x.Users.Type)
                .Where(x => x.CourseId == selectedCourse)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();
            FillSchoolListItem();
            return Page();
        }

        public async Task<IActionResult> OnPostElectivesAsync() {
            var selectedCourse = (long)Convert.ToDouble(Request.Form["f-e-course"]);

            ReviewData = new ReviewViewModel();
            ReviewData.Reviews = await _context.Review
                .Include(x => x.Program)
                .Include(x => x.Course)
                .Include(x => x.Users)
                .Include(x => x.Users.Type)
                .Where(x => x.CourseId == selectedCourse)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();
            FillSchoolListItem();
            return Page();
        }

        private SelectList FillSchoolListItem() {
            // populate dropdownlists
            return Schools = new SelectList(_context.School,
                "Id", "Name");
        }

        public JsonResult OnGetAllPrograms() {
            var programs = _context.Program
                .ToList();
            return new JsonResult(programs);
        }

        public JsonResult OnGetCourses() {
            return new JsonResult(_schoolService.GetCourses(Review.ProgramId));
        }

        public JsonResult OnGetElectives() {
            return new JsonResult(_schoolService.GetElectives());
        }
    }
}