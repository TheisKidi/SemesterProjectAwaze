using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;
using System.ComponentModel.DataAnnotations;

namespace SemesterProjectAwaze.Pages.Login
{
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
        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        public string Email { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        public string Phone { get; set; }
        [BindProperty]
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

            Guest newGuest = new Guest(FirstName, LastName, Email, Phone, false, Password, MakeGuestId());
            _guestService.Create(newGuest);

            return RedirectToPage("/Login/GuestLogin");
        }
    }
}