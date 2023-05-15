using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SemesterProjectAwaze.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemesterProjectAwaze.Pages.Sites
{
    [PrimaryKey(nameof(Id))]
    public class CreatePropertyModel : PageModel
    {
        private IGenericRepositoryService<Property> _repo;
        private IGenericRepositoryService<HouseOwner> _houseOwnerRepo;
        private int _randomNumberForId;
        private string _personalId;

        public CreatePropertyModel(IGenericRepositoryService<Property> repo, IGenericRepositoryService<HouseOwner> houseOwner)
        {
            _repo = repo;
            _houseOwnerRepo = houseOwner;
        }

        public string Id
        {
            get { return _personalId; }
            set { _personalId = value; }
        }

        public HouseOwner LoggedInOwner { get; set; }
        public string OwnerId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Country is required")]
        public Country Country { get; set; }
        public List<Country> Countries { get; private set; }
        public string CountryToString
        {
            get { return Country.ToString(); }
            set { CountryToString = value; }
        }
        [BindProperty]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "PricePrNight is required")]
        public decimal PricePrNight { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Rating is required")]
        public int Rating { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "VR link is required")]
        public string VR { get; set; }
        [BindProperty]
        public HouseType Type { get; set; }
        public List<HouseType> HouseTypes { get; private set; }
        public string TypeToString
        {
            get { return Type.ToString(); }
            set { TypeToString = value; }
        }

        [BindProperty]
        [Required(ErrorMessage = "Persons is required")]
        public int Persons { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Bedrooms is required")]
        public int Bedrooms { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Bathrooms is required")]
        public int Bathrooms { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Sustainable is required")]
        public bool Sustainable { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Allow pets is required")]
        public bool AllowPets { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Wifi is required")]
        public bool Wifi { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "TV is required")]
        public int Tv { get; set; }

        [BindProperty]
        public string PromoImg { get; set; }
        [BindProperty]
        public string Img1 { get; set; }
        [BindProperty]
        public string Img2 { get; set; }
        [BindProperty]
        public string Img3 { get; set; }

        private int RandomNumber() // calculates a random number for the personal ID in the interval [0-999]
        {
            Random rnd = new Random();
            _randomNumberForId = rnd.Next(100, 999);
            return _randomNumberForId;
        }

        private string MakePropertyId() // makes a personal id from the accounts first name and the RandomNumber() method
        {
            string threeLetters = Name[..3].ToUpper();
            _personalId = $"{threeLetters}" + $"{RandomNumber()}";
            return _personalId;
        }

        public IActionResult OnGet()
        {
            try
            {
                LoggedInOwner = _houseOwnerRepo.GetById(SessionHelper.GetProfile(HttpContext).Id);
            } catch (Exception ex)
            {
                return RedirectToPage("../Login/HouseOwnerLogin");
            }

                HouseTypes = Enum.GetValues<HouseType>().ToList();
                Countries = Enum.GetValues<Country>().ToList();
            return Page();
        }

        public IActionResult OnPost()
            {
            
            OwnerId = _houseOwnerRepo.GetById(SessionHelper.GetProfile(HttpContext).Id).OwnerId;

            if (Id == null)
            {
                Id = MakePropertyId();
            }

            if (!ModelState.IsValid)
            {
                HouseTypes = Enum.GetValues<HouseType>().ToList();
                Countries = Enum.GetValues<Country>().ToList();
                return Page();
            }
            Property newProperty = new Property(MakePropertyId(), OwnerId, CountryToString, Address, Name, Math.Round(PricePrNight,2), Rating, Description,
                VR, Persons, Bedrooms, Bathrooms, Sustainable, AllowPets, Wifi, Tv, TypeToString, PromoImg, Img1, Img2, Img3);

            _repo.Create(newProperty);
            
            return RedirectToPage("/Sites/Browse");
        }
    }
}
