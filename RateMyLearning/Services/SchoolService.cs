using Microsoft.AspNetCore.Http;
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
        IEnumerable<RateMyLearning.Data.Models.Program> GetContinuingEducationPrograms();
        IEnumerable<Course> GetContinuingEducationCourses(long? programId);
        Users GetSignedInUserDetails(HttpContext currentSession);
    }

    public class SchoolService : ISchoolService {
        private readonly rmldbContext _context;

        public SchoolService(rmldbContext context) {
            _context = context;
        }

        /// <summary>
        /// Get a list of programs that are offered at a selected school.
        /// </summary>
        /// <returns>List<Program></returns>
        public IEnumerable<RateMyLearning.Data.Models.Program> GetPrograms() {
            // filter out continuing education and elective programs
            var filter = new[] { "elective", "continuing education" };

            var programs = _context.Program
                .Where(x => !filter.Contains(x.Keywords))
                .ToList();
            return programs;
        }

        /// <summary>
        /// Get list of courses that relate to the selected program.
        /// </summary>
        /// <param name="programId"></param>
        /// <returns>List<Course></returns>
        public IEnumerable<Course> GetCourses(long programId) {
            var courses = _context.Course
                .Where(s => s.ProgramId == programId)
                .ToList();
            return courses;
        }

        /// <summary>
        /// Get a list of electives that are offered at a selected school.
        /// </summary>
        /// <returns>List<Course></returns>
        public IEnumerable<Course> GetElectives() {
            var electives = _context.Course
                .Where(s => s.IsElective == true)
                .ToList();
            return electives;
        }

        /// <summary>
        /// Get a list of Continuing Education programs that are offered at a selected school.
        /// </summary>
        /// <returns>List<Program></returns>
        public IEnumerable<RateMyLearning.Data.Models.Program> GetContinuingEducationPrograms() {
            var continuingEducationPrograms = _context.Program
                .Where(e => e.Id == 11)
                .ToList();
            return continuingEducationPrograms;
        }

        /// <summary>
        /// Get a list of Continuing Education courses that relate to the selected program.
        /// </summary>
        /// <param name="programId"></param>
        /// <returns>List<Course></returns>
        public IEnumerable<Course> GetContinuingEducationCourses(long? programId) {
            var continuingEducationCourses = _context.Course
                .Where(s => s.ProgramId == programId)
                .ToList();
            return continuingEducationCourses;
        }

        /// <summary>
        /// Find the user account details in relation to the current sessions signed in user.
        /// </summary>
        /// <param name="currentSession">currentSession</param>
        /// <returns>List<Course></returns>
        public Users GetSignedInUserDetails(HttpContext currentSession) {
            var signedInUser = _context.Users.SingleOrDefault(a => a.Email.Equals(
                currentSession.Session.GetString("_email")));
            return signedInUser;
        }
    }
}
