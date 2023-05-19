using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SemesterProjectAwaze.Services;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;

namespace SemesterProjectAwaze.Pages.Login
{
    [PrimaryKey(nameof(MyBookingId))]
    public class CreateGuestModel : PageModel
    {
        #region instance field
        private IGenericRepositoryService<Guest> _guestService;
        private int _randomNumberForId;
        private string _personalId;
        #endregion

        #region constructor
        public CreateGuestModel(IGenericRepositoryService<Guest> guestService)
        {
            _guestService = guestService;
        }

        #endregion

        #region properties
        public string MyBookingId
        {
            get { return _personalId; }
            set { _personalId = value; }
        }
        [BindProperty]
        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        public string FirstName { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        public string LastName { get; set; }
        [BindProperty]
        [RegularExpression(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b", ErrorMessage = "Ikke gyldig Email")]
        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        public string Email { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        public string Phone { get; set; }
        [BindProperty]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$", 
        ErrorMessage = "Ugyldigt password. Dit password skal v�re mindst 8 tegn langt, og det skal indeholde mindst �t tal, �t stort bogstav og �t lille bogstav.")]
        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        [BindProperty]
        public string ConfirmPassword { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// Udregner et tilf�ldigt nummer for et personligt g�ste ID i intervalet [100-999]
        /// </summary>
        /// <returns> en int, som repr�senterer et tilf�ldigt nummer </returns>
        private int RandomNumber() // calculates a random number for the personal ID in the interval [100-999]
        {
            Random rnd = new Random();
            _randomNumberForId = rnd.Next(100, 999);
            return _randomNumberForId;
        }

        /// <summary>
        /// laver et personligt id, som kombinerer g�stens navns f�rste tre bogstaver og
        /// kombinerer dem med RandomNumber() metodens output
        /// </summary>
        /// <returns> en string som repr�senterer et g�ste id p� 6 tegn </returns>
        private string MakeGuestId() // makes a personal id from the accounts first name and the RandomNumber() method
        {
            string threeLetters = FirstName[..3].ToUpper();
            _personalId = $"{threeLetters}" + $"{RandomNumber()}";
            return _personalId;
        }

        public void OnGet()
        {
        }

        /// <summary>
        /// Tjekker om adganskoderne passer, hvis ikke smider den en error tekst i feltet till 'confirm password'.
        /// Tjekker ogs� om model state er gyldigt. Herudover laver metoden et tjek p� om g�ste listen er tom. Hvis
        /// den er tom laver den en g�st med det samme og omdiregerer g�sten til login siden. Hvis listen ikke er tom
        /// g�r den igennem en foreach l�kke, som skal tjekke p� om der allerede findes en g�st med den indtastede email.
        /// Ellers opretter den et nyt g�ste objekt om diregerer g�sten til login siden.
        /// </summary>
        /// <returns> returnerer en side. </returns>
        public IActionResult OnPost()
        {
            if (ConfirmPassword != Password)
            {
                ModelState.AddModelError("ConfirmPassword", "Password does not match");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_guestService.GetAll().Count == 0)
            {
                Guest newGuest = new Guest(FirstName, LastName, Email, Phone, false, Crypto.HashPassword(Password), MakeGuestId());
                _guestService.Create(newGuest);

                return RedirectToPage("/Login/GuestLogin");
            }
            else
            {
                foreach (Guest guest in _guestService.GetAll())
                {

                    if (guest.Email == Email)
                    {
                        ModelState.AddModelError("Email", "Email findes allerede");
                    }
                    else
                    {
                        Guest newGuest = new Guest(FirstName, LastName, Email, Phone, false, Crypto.HashPassword(Password), MakeGuestId());
                        _guestService.Create(newGuest);

                        return RedirectToPage("/Login/GuestLogin");
                    }
                }
            }

            return Page();  
        }
        #endregion
    }
}