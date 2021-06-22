using Microsoft.EntityFrameworkCore;
using RateMyLearning.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RateMyLearning.Tests {
    public class RateMyLearningTestBase : IDisposable {
        protected readonly rmldbContext _context;

        public RateMyLearningTestBase() {
            var options = new DbContextOptionsBuilder<rmldbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            _context = new rmldbContext(options);
            _context.Database.EnsureCreated();
        }

        public void Dispose() {
            _context.Database.EnsureDeleted(); // deletes InMemory database
            _context.Dispose();
        }
    }
}
