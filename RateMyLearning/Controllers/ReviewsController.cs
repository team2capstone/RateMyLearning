using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyLearning.Data.Models;

namespace RateMyLearning.Controllers {
    public class ReviewsController : Controller {
        private readonly rmldbContext _context;

        public ReviewsController(rmldbContext context) {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(long id) {
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(long id, Review review) {
            // id doesn't match the id of the review being updated
            if (id != review.Id) {
                return BadRequest();
            }

            // check for model errors
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            // could not find existing review
            var entity = await _context.Review.FindAsync(id);
            if (entity == null) {
                return NotFound();
            }

            // update review
            entity.Description = review.Description;
            entity.ProgramId = review.ProgramId;
            entity.CourseId = review.CourseId;
            entity.Rating = review.Rating;
            entity.CampusId = review.CampusId;
            entity.InterestId = review.InterestId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(long id) {
            var review = await _context.Review.SingleOrDefaultAsync(m => m.Id == id);

            // no review was found with that id
            if (review == null) {
                return NotFound();
            }

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();

            return Ok(review);
        }
    }
}