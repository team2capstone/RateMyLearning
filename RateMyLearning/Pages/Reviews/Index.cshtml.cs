using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RateMyLearning.Data.Models;
using RateMyLearning.ViewModels;

namespace RateMyLearning.Pages.Reviews {
    public class IndexModel : PageModel {
        private readonly rmldbContext _context;

        public IndexModel(rmldbContext context) {
            _context = context;
        }

        public ReviewViewModel ReviewData { get; set; }

        public void OnGet() {

        }
    }
}