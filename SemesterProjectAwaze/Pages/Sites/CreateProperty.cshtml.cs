using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace SemesterProjectAwaze.Pages.Sites
{
    public class CreatePropertyModel : PageModel
    {
        private IGenericRepositoryService<Property> _propertyService;
        private IGenericRepositoryService<HouseOwner> _houseOwnerService;
        private int _randomNumberForId;
        private string _personalId;

        public CreatePropertyModel(IGenericRepositoryService<Property> propertyService, IGenericRepositoryService<HouseOwner> houseOwnerService)
        {
            _propertyService = propertyService;
            _houseOwnerService = houseOwnerService;
        }

        [BindProperty]
        public string Id
        {
            get { return _personalId; }
            set { _personalId = value; }
        }
        [BindProperty]
        [Required(ErrorMessage = "OwnerId is required")]
        public string OwnerId { get; set; } // skal erstattes, når vi har lavet loginService
        [BindProperty]
        [Required(ErrorMessage = "Country is required")]
        public Country Country { get; set; }
        public List<Country> Countries { get; private set; }
        [BindProperty]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "PricePrNight is required")]
        public double PricePrNight { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Rating is required")]
        public int Rating { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Facilities is required")]
        public Facilities Facilities { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "VR link is required")]
        public string VR { get; set; }
        [BindProperty]
        public HouseType Type { get; set; }
        public List<HouseType> HouseTypes { get; private set; }

        private int RandomNumber() // calculates a random number for the personal ID in the interval [0-999]
        {
            Random rnd = new Random();
            _randomNumberForId = rnd.Next(0, 999);
            return _randomNumberForId;
        }

        private string MakePersonalId() // makes a personal id from the accounts first name and the RandomNumber() method
        {
            string threeLetters = Name[..3].ToUpper();
            _personalId = $"{threeLetters}" + $"{RandomNumber()}";
            return _personalId;
        }

        public void OnGet()
        {
            HouseTypes = Enum.GetValues<HouseType>().ToList();
            Countries = Enum.GetValues<Country>().ToList();
        }

        public IActionResult OnPost()
        {
            Id = MakePersonalId();
            if (!ModelState.IsValid)
            {
                HouseTypes = Enum.GetValues<HouseType>().ToList();
                Countries = Enum.GetValues<Country>().ToList();
                return Page();
            }
            Property newProperty = new Property(Id, OwnerId /* skal ertstates, når vi har loginService */, Country, Address, Name, PricePrNight, Rating, Description,
                                new Facilities(Facilities.Persons, Facilities.Bedrooms, Facilities.Bathrooms, Facilities.Sustainable,
                                Facilities.AllowPets, Facilities.Wifi, Facilities.Tv, Type), VR);

            _propertyService.Create(newProperty);

            return RedirectToPage("Index");
        }
    }
}
