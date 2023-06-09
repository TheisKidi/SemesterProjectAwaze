﻿using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;

namespace SemesterProjectAwaze.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public string userName { get; set; }

        [BindProperty]
        public List<Country> Countries { get; private set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        
        public void OnGet()
        {
            Countries = Enum.GetValues<Country>().ToList();
            var user = SessionHelper.GetProfile(HttpContext);
            if (user != null)
            {
                userName = user.ProfileName;
            }
        }
    }
}