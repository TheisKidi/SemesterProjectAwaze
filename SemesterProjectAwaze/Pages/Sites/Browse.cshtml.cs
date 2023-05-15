using AwazeLib.model;
using Microsoft.AspNetCore.Components.Web;
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
        public List<Property> PropertyFiltered { get; set; }

        [BindProperty]
        public decimal Price { get; set; }

        [BindProperty]
        public bool HouseTypeHouse { get; set; }
        public string HouseTypeHouseString { get; set; }

        [BindProperty]
        public bool HouseTypeVac { get; set; }
        public string HouseTypeVacString { get; set; }

        [BindProperty]
        public bool HouseTypeBoat { get; set; }
        public string HouseTypeBoatString { get; set; }

        [BindProperty]
        public int Rating { get; set; }

        [BindProperty]
        public int Bedrooms { get; set; }

        [BindProperty]
        public int Bathrooms { get; set; }

        [BindProperty]
        public bool Sustainable { get; set; }

        [BindProperty]
        public HouseType SelectedType { get; set; }

        public void OnGet()
        {
            Properties = _propRepo.GetAll();
            HouseTypeHouse = false;
            HouseTypeVac = false;
            HouseTypeBoat = false;
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("SelectedProperty");
        }

        public void OnPostFilter()
        {
            HouseType selectedType = SelectedType;

            if (HouseTypeHouse)
            {
                HouseTypeHouseString = "HolidayCottage";
            }

            if (HouseTypeVac)
            {
                HouseTypeVacString = "HollidayApartment";
            }

            if (HouseTypeBoat)
            {
                HouseTypeBoatString = "HouseBoat";
            }

            Properties = _propRepo.GetAll()
                .Where(p => Price == 0 || p.PricePrNight <= Price)
                .Where(p => Bedrooms == 0 || p.Bedrooms >= Bedrooms)
                .Where(p => Bathrooms == 0 || p.Bathrooms >= Bathrooms)
                .Where(p => Rating == 0 || p.Rating >= Rating)
                .Where(p => !Sustainable || p.Sustainable == Sustainable)
                .Where(p => string.IsNullOrEmpty(HouseTypeHouseString) && 
                        string.IsNullOrEmpty(HouseTypeVacString) && 
                        string.IsNullOrEmpty(HouseTypeBoatString) ||
                        (p.HouseType == HouseTypeHouseString || 
                        p.HouseType == HouseTypeVacString || 
                        p.HouseType == HouseTypeBoatString))
                .OrderByDescending(p => p.PricePrNight)
                .ToList();
        }

        public IActionResult OnGetReset()
        {
            return Page();
        }
    }
}
