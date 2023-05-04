using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SemesterProjectAwaze.Pages.Login
{
    public class CreateGuestModel : PageModel
    {
        private int _randomNumberForId;
        private string _personalId;

        public string Id
        {
            get { return _personalId; }
            set { _personalId = value; }
        }
        [BindProperty]
        [Required]
        public string FirstName { get; set; }
        [BindProperty]
        [Required]
        public string LastName { get; set; }
        [BindProperty]
        [Required]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        public string Phone { get; set; }
        public bool IsOwner { get; set; }
        [BindProperty]
        [Required]
        public string Password { get; set; }

        private int RandomNumber() // calculates a random number for the personal ID in the interval [0-999]
        {
            Random rnd = new Random();
            _randomNumberForId = rnd.Next(0, 999);
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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Guest newGuest = new Guest(FirstName, LastName, Email, Phone, false, Password, MakeGuestId());

            return RedirectToPage("/Profile");
        }
    }
}
