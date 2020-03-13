using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyLearning.Data.Models;

namespace RateMyLearning.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly rmldbContext _context;

        public ReviewsController(rmldbContext context) {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id) {
            var review = await _context.Review.SingleOrDefaultAsync(m => m.Id == id);

            if (review == null) {
                return NotFound();
            }

            return Ok(review);
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews() {
            return Ok(await _context.Review.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(Review review) {

            // check for any model errors
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            _context.Review.Add(review);
            await _context.SaveChangesAsync();

            return Ok(review);
        }
    }
}