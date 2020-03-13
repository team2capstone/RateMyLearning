using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyLearning.Controllers;
using RateMyLearning.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RateMyLearning.Tests {
    public class ReviewsControllerTests : IDisposable {
        protected readonly rmldbContext _context;

        // run after every test
        public void Dispose() {
            _context.Database.EnsureDeleted(); // deletes InMemory database
        }

        // setup inmemory database
        public ReviewsControllerTests() {
            var options = new DbContextOptionsBuilder<rmldbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            _context = new rmldbContext(options);
            _context.Database.EnsureCreated();

            var reviews = new[] {
                new Review { Id = 1, Description = "Test1", ProgramId = 2, CourseId = 3, 
                    SchoolId = 1, Rating = 5, CampusId = 1, InterestId = 3, UsersId = 1},
                new Review { Id = 2, Description = "Test2", ProgramId = 2, CourseId = 3,
                    SchoolId = 1, Rating = 5, CampusId = 1, InterestId = 3, UsersId = 2},
                new Review { Id = 3, Description = "Test3", ProgramId = 2, CourseId = 3,
                    SchoolId = 1, Rating = 5, CampusId = 1, InterestId = 3, UsersId = 3},
            };
            _context.Review.AddRange(reviews);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetReview_ReturnsNotFound_GivenInvalidId() {
            // arrange
            var controller = new ReviewsController(_context);

            // act
            var result = await controller.GetCustomer(90);

            // assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetReview_ReturnsReview_GivenValidId() {
            // arrange
            var controller = new ReviewsController(_context);

            // act
            var result = await controller.GetCustomer(1);

            // assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var review = Assert.IsAssignableFrom<Review>(objectResult.Value);
            Assert.Equal("Test1", review.Description);
        }

        [Fact]
        public async Task GetReviews_ReturnsAllReviews() {
            // arrange
            var controller = new ReviewsController(_context);

            // act
            var result = await controller.GetReviews();

            // assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var reviews = Assert.IsAssignableFrom<IEnumerable<Review>>(objectResult.Value);
            Assert.Equal(3, reviews.Count());
        }

        [Fact]
        public async Task CreateReview_ReturnsBadRequest_WhenModelStateIsInvalid() {
            // arrange
            var controller = new ReviewsController(_context);
            controller.ModelState.AddModelError("Description", "Required");

            // act
            var result = await controller.CreateReview(new Review());

            // assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateReview_ReturnsOkObjectResult_WhenInsertingReview() {
            // arrange
            var controller = new ReviewsController(_context);

            // act
            var result = await controller.CreateReview(new Review {
                Id = 4,
                Description = "Good course!",
                ProgramId = 6,
                CourseId = 3,
                SchoolId = 1,
                Rating = 4,
                CampusId = 1,
                InterestId = 1,
                UsersId = 1
            });

            // assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
