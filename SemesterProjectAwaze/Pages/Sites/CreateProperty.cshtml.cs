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
        #region instance field
        private IGenericRepositoryService<Property> _propertyService;
        private IGenericRepositoryService<HouseOwner> _houseOwnerService;
        private int _randomNumberForId;
        private string _personalId;
        #endregion

        #region constructor
        public CreatePropertyModel(IGenericRepositoryService<Property> propertyService, IGenericRepositoryService<HouseOwner> houseOwnerService)
        {
            _propertyService = propertyService;
            _houseOwnerService = houseOwnerService;
        }
        #endregion

        #region propeties
        public string Id
        {
            get { return _personalId; }
            set { _personalId = value; }
        }
        public string CountryToString
        {
            get { return Country.ToString(); }
            set { CountryToString = value; }
        }
        public string TypeToString
        {
            get { return Type.ToString(); }
            set { TypeToString = value; }
        }
        public HouseOwner LoggedInOwner { get; set; }
        public string OwnerId { get; set; }
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
        #endregion

        #region methods
        /// <summary>
        /// Udregner et tilfældigt nummer for et bolig ID i intervalet [100-999]
        /// </summary>
        /// <returns> en int, som repræsenterer et tilfældigt nummer </returns>
        private int RandomNumber()
        {
            Random rnd = new Random();
            _randomNumberForId = rnd.Next(100, 999);
            return _randomNumberForId;
        }

        /// <summary>
        /// laver et personligt id, som kombinerer boligens første tre bogstaver fra navnet og
        /// kombinerer dem med RandomNumber() metodens output
        /// </summary>
        /// <returns> en string som repræsenterer et bolig id på 6 tegn </returns>
        private string MakePropertyId()
        {
            string threeLetters = Name[..3].ToUpper();
            _personalId = $"{threeLetters}" + $"{RandomNumber()}";
            return _personalId;
        }

        /// <summary>
        /// Prøver at finde en husejer, da det kun er en husejer der kanm oprette en bolig.
        /// Hvis der ikke er en husejer, som et logget ind, bliver gæsten omdiregeret til login siden.
        /// </summary>
        /// <returns> en side </returns>
        public IActionResult OnGet()
        {
            try
            {
                LoggedInOwner = _houseOwnerService.GetById(SessionHelper.GetProfile(HttpContext).Id);
            } catch (Exception ex)
            {
                return RedirectToPage("../Login/HouseOwnerLogin");
            }

                HouseTypes = Enum.GetValues<HouseType>().ToList();
                Countries = Enum.GetValues<Country>().ToList();
            return Page();
        }

        /// <summary>
        /// Sætter OwnerId ved hjælp af vore SessionHelper.
        /// Tjekker om bolig Id er null ellers laver den et.
        /// Tjekker om modelState er gyldigt og sætter vores enums.
        /// Opretter til sidst en ny bolig og tilføjer den til databasen, hvis alt opfyldes.
        /// </summary>
        /// <returns> en side </returns>
        public IActionResult OnPost()
        {
            
            OwnerId = _houseOwnerService.GetById(SessionHelper.GetProfile(HttpContext).Id).OwnerId;

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

            _propertyService.Create(newProperty);
            
            return RedirectToPage("/Sites/Browse");
        }
        #endregion
    }
}
