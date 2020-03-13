using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyLearning.Controllers;
using RateMyLearning.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RateMyLearning.Tests {
    public class ProvincesControllerTests : IDisposable {
        // run after every test
        public void Dispose() {
            _context.Database.EnsureDeleted(); // deletes InMemory database
        }

        protected readonly rmldbContext _context;

        public ProvincesControllerTests() {
            // create new database instance
            var options = new DbContextOptionsBuilder<rmldbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            _context = new rmldbContext(options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public void PostProvince_ReturnsNewlyCreatedProvince() {
            var service = new ProvincesController(_context);
            service.CreateProvince("British Columbia");
            _context.SaveChanges();

            Assert.Equal("British Columbia", _context.Province.Single().Name);
        }
    }
}
