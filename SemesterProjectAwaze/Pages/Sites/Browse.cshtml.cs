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
        [BindProperty]
        public HashSet<Country> unique { get; set; }
        [BindProperty]
        public HashSet<HouseType> uniqueType { get; set; }

        public void OnGet()
        {

            Properties = _propRepo.GetAll();

            //unique = new HashSet<Country>(Properties.Select(c => c.Country));
            //uniqueType = new HashSet<HouseType>(Properties.Select(c => c.HouseType));
        }

        public IActionResult OnPost()
        {

            return RedirectToPage("SelectedProperty");
        }

    }
}
