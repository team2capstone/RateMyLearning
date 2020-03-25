﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RateMyLearning.Pages.Account {
    public class IndexModel : PageModel {
        public void OnGet() {

        }

        public IActionResult OnGetSignOut() {
            HttpContext.Session.Remove("_email");
            return RedirectToPage("/Index");
        }

        public IActionResult OnPost() {
            HttpContext.Session.Remove("_email");
            return RedirectToPage("/Index");
        }
    }
}