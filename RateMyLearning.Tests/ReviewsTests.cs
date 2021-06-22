using Microsoft.EntityFrameworkCore;
using Moq;
using RateMyLearning.Data.Models;
using RateMyLearning.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RateMyLearning.Tests {
    public class ReviewsTests : RateMyLearningTestBase {
        //[Fact]
        //public void GetPrograms_ReturnsListOfPrograms() {
        //    // arrange
        //    var service = new Mock<ISchoolService>();
        //    var programs = new[] {
        //            new RateMyLearning.Data.Models.Program { Id = 1, Name = "ITID"},
        //            new RateMyLearning.Data.Models.Program { Id = 2, Name = "CPA"},
        //            new RateMyLearning.Data.Models.Program { Id = 3, Name = "CP"},
        //        };
        //    _context.Program.AddRange(programs);
        //    _context.SaveChanges();

        //    // act
        //    service.Setup(x => x.GetPrograms());

        //    // assert
        //    Assert.NotNull(service);
        //}

        //[Fact]
        //public void GetCourses_ReturnsNull_GivenInvalidProgramId() {
        //    // arrange
        //    var programs = new Mock<ISchoolService>();

        //    // act
        //    programs.Setup(x => x.GetCourses(99));
        //    // assert

        //}
    }
}
