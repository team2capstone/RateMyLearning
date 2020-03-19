using Microsoft.EntityFrameworkCore;
using RateMyLearning.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyLearning.Services {
    public interface ISchoolService {
        IEnumerable<RateMyLearning.Data.Models.Program> GetPrograms();
        IEnumerable<Course> GetCourses(long programId);
        IEnumerable<Course> GetElectives();
    }

    public class SchoolService : ISchoolService {
        private readonly rmldbContext _context;

        public SchoolService(rmldbContext context) {
            _context = context;
        }

        public IEnumerable<RateMyLearning.Data.Models.Program> GetPrograms() {
            var programs = _context.Program
                .ToList();
            return programs;
        }

        public IEnumerable<Course> GetCourses(long programId) {
            var courses = _context.Course
                .Where(s => s.ProgramId == programId)
                .ToList();
            return courses;
        }

        public IEnumerable<Course> GetElectives() {
            var electives = _context.Course
                .Where(s => s.IsElective == true)
                .ToList();
            return electives;
        }
    }
}
