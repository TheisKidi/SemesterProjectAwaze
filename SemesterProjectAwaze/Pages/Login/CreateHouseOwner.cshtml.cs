using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SemesterProjectAwaze.Services;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;

namespace SemesterProjectAwaze.Pages.Login
{
    [PrimaryKey(nameof(OwnerId))]
    public class CreateHouseOwnerModel : PageModel
    {
        private IGenericRepositoryService<HouseOwner> _repo;

        public CreateHouseOwnerModel(IGenericRepositoryService<HouseOwner> repo)
        {
            _repo = repo;
        }

        private int _randomNumberForId;
        private string _personalId;
        public string OwnerId
        {
            get { return _personalId; }
            set { _personalId = value; }
        }

        [BindProperty]
        [RegularExpression(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b", ErrorMessage = "Ikke gyldig Email")]
        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        [BindProperty]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$",
        ErrorMessage = "Ugyldigt password. Dit password skal være mindst 8 tegn langt, og det skal indeholde mindst ét tal, ét stort bogstav og ét lille bogstav.")]
        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Password does not match")]
        [BindProperty]
        
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        [BindProperty]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Du mangler at udfylde feltet")]
        [BindProperty]
        public string Address { get; set; }

        private int RandomNumber() // calculates a random number for the personal ID in the interval [0-999]
        {
            Random rnd = new Random();
            _randomNumberForId = rnd.Next(100, 999);
            return _randomNumberForId;
        }

        private string MakeOwnerId() // makes a personal id from the accounts first name and the RandomNumber() method
        {
            string threeLetters = FirstName[..3].ToUpper();
            _personalId = $"{threeLetters}" + $"{RandomNumber()}";
            return _personalId;
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


            foreach (HouseOwner houseOwner in _repo.GetAll())
            {
                if (houseOwner.Email == Email)
                {
                    ModelState.AddModelError("Email", "Email findes allerede");
                    return Page();
                }
            }
            HouseOwner newHouseOwner = new HouseOwner(FirstName, LastName, Email, Phone, true, Crypto.HashPassword(Password), MakeOwnerId(), Address);
            _repo.Create(newHouseOwner);

            return RedirectToPage("/Login/HouseOwnerLogin");
        }

    }
}
