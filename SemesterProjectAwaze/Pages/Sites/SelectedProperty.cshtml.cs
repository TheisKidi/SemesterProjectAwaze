using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SemesterProjectAwaze.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.WebPages;

namespace SemesterProjectAwaze.Pages.Sites
{
    [Table("Favorite")]
    public class SelectedPropertyModel : PageModel
    {
        #region instance field
        private IGenericRepositoryService<Property> _propRepo;
        private FavoriteRepositoryService _favRepo;
        private IGenericRepositoryService<Guest> _guestRepo;
        private OrderRepositoryServiceDB _orderRepo;
        #endregion

        #region constructor
        public SelectedPropertyModel(IGenericRepositoryService<Property> propRepo, IGenericRepositoryService<Guest> guestRepo, FavoriteRepositoryService favRepo, OrderRepositoryServiceDB orderRepo)
        {
            _favRepo = favRepo;
            _guestRepo = guestRepo;
            _propRepo = propRepo;
            _orderRepo = orderRepo;
        }
        #endregion

        #region properties
        [BindProperty]
        public Property SelectedProperty { get; set; }
        [BindProperty]
        public Guest GuestLoggedIn { get; set; }
        [BindProperty]
        [Key]
        public int Id { get; set; }
        [BindProperty]
        public Order Order { get; set; }
        [BindProperty]
        public DateTime ArrivalDate { get; set; }
        [BindProperty]
        public DateTime DepartureDate { get; set; }
        public decimal Price { get; set; }
        [Key]
        public int OrderId { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// Metode til at udregne total prisen for en booking. Udregner først hvor mange nætter,
        /// gæsten har booket. Herefter converterer den nætterne til et decimal, så
        /// den kan gange det med pris pr nat.
        /// </summary>
        /// <returns>
        /// Returnerer et decimal som svarer til total prisen
        /// </returns>
        public decimal CalculatePrice()
        {
            var nights = DepartureDate - ArrivalDate;
            decimal totalDays = Convert.ToDecimal(nights.TotalDays);

            Price = totalDays * SelectedProperty.PricePrNight;
            return Price;
        }

        /// <summary>
        /// Henter bolig id fra /browse siden
        /// </summary>
        /// <param name="id"> tager ét parameter ind, som er id fra en bolig </param>
        public void OnGet(string id)
        {
            SelectedProperty = _propRepo.GetById(id);
            ArrivalDate = DateTime.Now;
            DepartureDate = DateTime.Now.AddDays(7);
        }

        /// <summary>
        /// Tilføj til favorit metoden. Sætter to objekter lig med den valgte bolig + den loggede ind gæst.
        /// Herefter løber metoden igennem en foreach løkke, som skal tjekke om gæsten allerede har tilføjet den
        /// valgte bolig til favorit. Hvis dette er tilfældet, siden genindlæst. Ellers tilføjer metoden den valgte
        /// bolig til favorit og opdaterer databasen.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Her videresender metoden med Id som parameter for, 
        /// at 'SelectedProperty' siden ikke genindlæses med default værdier
        /// </returns>
        public IActionResult OnPostAddToFavorite(string id)
        {
            Property selectedProperty = _propRepo.GetById(id);
            Guest loggedInGuest = _guestRepo.GetById(SessionHelper.GetProfile(HttpContext).Id);

            foreach (Favorite fav in _favRepo.GetAll())
            {
                if (fav.PropertyId == selectedProperty.Id && fav.GuestId == loggedInGuest.MyBookingId)
                {
                    return RedirectToPage("SelectedProperty", new { Id = id });
                }
            }
            Favorite newFav = new Favorite(Id, loggedInGuest.MyBookingId, selectedProperty.Id);
            _favRepo.Create(newFav);

            return RedirectToPage("SelectedProperty", new { Id = id }); 
        }

        public bool IsNotBooked()
        {
            foreach (Order order in _orderRepo.GetAll())
            {
                if (order.PropertyId == SelectedProperty.Id)
                {
                    if (ArrivalDate.Ticks > order.DepartureDate.Ticks)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CheckDate()
        {
            if (ArrivalDate < DepartureDate)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Metode til at booke en bolig. Finder først den valgte bolig. Derefter
        /// tjekker den om en gæst er logget ind, hvis ikke sender den gæsten til logind siden.
        /// Herefter kalder den CalculatePrice() metoden og opretter et nyt order objekt og
        /// gemmer det i databasen.
        /// </summary>
        /// <param name="id">
        /// Tager et property ID ind
        /// </param>
        /// <returns>
        /// Returnerer gæstens side.
        /// </returns>
        public IActionResult OnPostSelectPeriod(string id)
        {

            SelectedProperty = _propRepo.GetById(id);
            try
            {
                GuestLoggedIn = _guestRepo.GetById(SessionHelper.GetProfile(HttpContext).Id);
            }
            catch (Exception ex)
            {
                return RedirectToPage("../Login/GuestLogin");
            }

            Price = CalculatePrice();

            if (IsNotBooked() && CheckDate()){

                Order newOrder = new Order(OrderId, GuestLoggedIn.MyBookingId, SelectedProperty.Id,
                               ArrivalDate, DepartureDate, Price);

                _orderRepo.Create(newOrder);
                return RedirectToPage("../Login/GuestProfile");
            }
            else
            {
                ModelState.AddModelError("ArrivalDate", "Dato er allerede booked");
            }

            return RedirectToPage("SelectedProperty", new { Id = id });
        }
        #endregion
    }
}
