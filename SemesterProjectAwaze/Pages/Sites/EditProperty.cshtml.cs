using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;

namespace SemesterProjectAwaze.Pages.Sites
{
    public class EditPropertyModel : PageModel
    {
        private IGenericRepositoryService<Property> _propertyService;
        public EditPropertyModel(IGenericRepositoryService<Property> propertyService)
        {
            _propertyService = propertyService;
        }

        [BindProperty]
        public string Id { get; set; }
        [BindProperty]
        public string OwnerId { get; set; }
        [BindProperty]
        public string Country { get; set; }
        [BindProperty]
        public string Address { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public decimal PricePrNight { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public string VR { get; set; }
        [BindProperty]
        public int Rating { get; set; }
        [BindProperty]
        public int Persons { get; set; }
        [BindProperty]
        public int Bedrooms { get; set; }
        [BindProperty]
        public int Bathrooms { get; set; }
        [BindProperty]
        public bool Sustainable { get; set; }
        [BindProperty]
        public bool AllowPets { get; set; }
        [BindProperty]
        public bool Wifi { get; set; }
        [BindProperty]
        public int Tv { get; set; }
        [BindProperty]
        public string HouseType { get; set; }
        [BindProperty]
        public string PromoImg { get; set; }
        [BindProperty]
        public string Img1 { get; set; }
        [BindProperty]
        public string Img2 { get; set; }
        [BindProperty]
        public string Img3 { get; set; }

        public void OnGet(string id)
        {
            Property editProperty = _propertyService.GetById(id);

            Name = editProperty.Name;
            PricePrNight = editProperty.PricePrNight;
            VR = editProperty.VR;
            Persons = editProperty.Persons;
            Bedrooms = editProperty.Bedrooms;
            Bathrooms = editProperty.Bathrooms;
            Sustainable = editProperty.Sustainable;
            Description = editProperty.Description;
            Wifi = editProperty.Wifi;
            AllowPets = editProperty.AllowPets;
            Tv = editProperty.Tv;
        }

        public IActionResult OnPostEdit(string id)
        {
            Property newValues = new Property(Id, OwnerId, Country, Address, Name, PricePrNight, Rating,
                Description, VR, Persons, Bedrooms, Bathrooms, Sustainable, AllowPets, Wifi, Tv, HouseType, PromoImg, Img1, Img2, Img3);

            _propertyService.Update(id, newValues);
            return RedirectToPage("/Sites/Browse");
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Sites/Browse");
        }
    }
}
