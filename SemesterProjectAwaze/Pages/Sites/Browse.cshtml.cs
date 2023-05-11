using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using SemesterProjectAwaze.Services;

namespace SemesterProjectAwaze.Pages.Sites
{
    public class BrowseModel : PageModel
    {

        private readonly IGenericRepositoryService<Property> _propRepo;
        public BrowseModel(IGenericRepositoryService<Property> propRepo)
        {
            _propRepo = propRepo;
        }

        [BindProperty]
        public List<Property> Properties { get; set; }

        public void OnGet()
        {
            Properties = _propRepo.GetAll();
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("SelectedProperty");
        }

        public IActionResult OnPostFilter()
        {
            return null;
        }

    }
}
