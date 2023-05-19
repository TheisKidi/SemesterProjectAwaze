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
        #region instance field
        private IGenericRepositoryService<HouseOwner> _houseOwnerService;
        private int _randomNumberForId;
        private string _personalId;
        #endregion

        #region constructor
        public CreateHouseOwnerModel(IGenericRepositoryService<HouseOwner> repo)
        {
            _repo = repo;
        }
        #endregion

        #region properties
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
        #endregion

        #region methods
        /// <summary>
        /// Udregner et tilfældigt nummer for et personligt husejer ID i intervalet [100-999]
        /// </summary>
        /// <returns> en int, som repræsenterer et tilfældigt nummer </returns>
        private int RandomNumber()
        {
            Random rnd = new Random();
            _randomNumberForId = rnd.Next(100, 999);
            return _randomNumberForId;
        }

        /// <summary>
        /// laver et personligt id, som kombinerer husejerns navns første tre bogstaver og
        /// kombinerer dem med RandomNumber() metodens output
        /// </summary>
        /// <returns> en string som repræsenterer et husejer id på 6 tegn </returns>
        private string MakeOwnerId()
        {
            string threeLetters = FirstName[..3].ToUpper();
            _personalId = $"{threeLetters}" + $"{RandomNumber()}";
            return _personalId;
        }

        /// <summary>
        /// Tjekker om adganskoderne passer, hvis ikke smider den en error tekst i feltet till 'confirm password'.
        /// Tjekker også om model state er gyldigt. Herudover laver metoden et tjek på om husejer listen er tom. Hvis
        /// den er tom laver den en gæst med det samme og omdiregerer husejeren til login siden. Hvis listen ikke er tom
        /// går den igennem en foreach løkke, som skal tjekke på om der allerede findes en husejer med den indtastede email.
        /// Ellers opretter den et nyt hussejer objekt om diregerer ejeren til login siden.
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

            if (_houseOwnerService.GetAll().Count == 0)
            {
                HouseOwner newOwner = new HouseOwner(FirstName, LastName, Email, Phone, true, Crypto.HashPassword(Password), MakeOwnerId(), Address);
                _houseOwnerService.Create(newOwner);

                return RedirectToPage("/Login/HouseOwnerLogin");
            }
            else
            {
                foreach (HouseOwner owner in _houseOwnerService.GetAll())
                {

                    if (owner.Email == Email)
                    {
                        ModelState.AddModelError("Email", "Email findes allerede");
                    }
                    else
                    {
                        HouseOwner newOwner = new HouseOwner(FirstName, LastName, Email, Phone, true, Crypto.HashPassword(Password), MakeOwnerId(), Address);
                        _houseOwnerService.Create(newOwner);

                        return RedirectToPage("/Login/HouseOwnerLogin");
                    }
                }
            }
            return Page();
        }
        #endregion
    }
}
