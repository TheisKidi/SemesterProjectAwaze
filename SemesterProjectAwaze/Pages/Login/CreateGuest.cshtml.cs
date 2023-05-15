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

        private IGenericRepositoryService<Guest> _guestService;
        public CreateGuestModel(IGenericRepositoryService<Guest> guestService)
        {
            _guestService = guestService;
        }
        private int _randomNumberForId;
        private string _personalId;

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
        ErrorMessage = "Ugyldigt password. Dit password skal være mindst 8 tegn langt, og det skal indeholde mindst ét tal, ét stort bogstav og ét lille bogstav.")]
        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        [BindProperty]
        public string ConfirmPassword { get; set; }

        private int RandomNumber() // calculates a random number for the personal ID in the interval [100-999]
        {
            Random rnd = new Random();
            _randomNumberForId = rnd.Next(100, 999);
            return _randomNumberForId;
        }

        private string MakeGuestId() // makes a personal id from the accounts first name and the RandomNumber() method
        {
            string threeLetters = FirstName[..3].ToUpper();
            _personalId = $"{threeLetters}" + $"{RandomNumber()}";
            return _personalId;
        }

        public void OnGet()
        {
        }

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
                Guest newGuest = new Guest(FirstName, LastName, Email, Phone, false, Password, MakeGuestId());
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
    }
}