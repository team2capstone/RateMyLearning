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

namespace RateMyLearning.Pages.Reviews {
    public class EditModel : PageModel {
        private readonly rmldbContext _context;

        [BindProperty] public Review Review { get; set; }

        public EditModel(rmldbContext context) {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(long? id) {
            if (id == null) {
                return NotFound();
            }

            Review = await _context.Review
                .Include(r => r.Program)
                .Include(r => r.School)
                .Include(r => r.Course)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (Review == null) {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long id) {
            var reviewToUpdate = await _context.Review.FindAsync(id);

            // no review found
            if (reviewToUpdate == null) {
                return NotFound();
            }

            reviewToUpdate.Rating = Convert.ToInt32(Request.Form["rating"]);

            if (await TryUpdateModelAsync<Review>(
                reviewToUpdate,
                "review",
                s => s.Description, s => s.Rating)) {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }

        public IActionResult OnPostCancelAsync(long? id) {
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(long? id) {
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
                return RedirectToAction("./Index");
            }
        }
    }
}