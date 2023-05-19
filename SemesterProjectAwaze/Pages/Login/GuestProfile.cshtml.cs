using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;

namespace SemesterProjectAwaze.Pages.Login
{
    public class GuestProfileModel : PageModel
    {
        #region instance field
        private IGenericRepositoryService<Guest> _guestService;
        private IGenericRepositoryService<HouseOwner> _houseOwnerService;
        private FavoriteRepositoryService _favoriteService;
        private OrderRepositoryServiceDB _orderRepo;
        #endregion

        #region constructor
        public GuestProfileModel(IGenericRepositoryService<Guest> guestService,
                                FavoriteRepositoryService favortieService,
                                OrderRepositoryServiceDB orderRepo,
                                IGenericRepositoryService<HouseOwner> houseOwnerService)
        {
            _favoriteService = favortieService;
            _guestService = guestService;
            _orderRepo = orderRepo;
            _houseOwnerService = houseOwnerService;
        }
        #endregion

        #region properties
        [BindProperty]
        public Guest LoggedInUser { get; set; }
        [BindProperty]
        public List<Favorite> FavoriteList { get; set; }
        [BindProperty]
        public List<Order> OrdersList { get; set; }
        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Phone { get; set; }
        [BindProperty]
        public string MyBookingId { get; set; }
        [BindProperty]
        public string OwnerEmail { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// OnGet henter den gæst, som er logget ind. Henter alle deres favorit boliger.
        /// Henter alle deres bookinger. Herudover sætter den tre properties til, at
        /// være lig med en redigeret gæst.
        /// </summary>
        public void OnGet()
        {
            LoggedInUser = _guestService.GetById(SessionHelper.GetProfile(HttpContext).Id);
            FavoriteList = _favoriteService.GetFavoritesByUserEmail(LoggedInUser.Email);
            OrdersList = _orderRepo.GetOrderByUserEmail(LoggedInUser.Email);



            Guest editGuest = _guestService.GetById(LoggedInUser.MyBookingId);

            FirstName = editGuest.FirstName;
            LastName = editGuest.LastName;
            Phone = editGuest.Phone;
        }

        /// <summary>
        /// Henter en email fra en husejer, som passer til den respektive bolig for, at
        /// gæsten kan komme i kontakt med ejeren.
        /// </summary>
        /// <param name="o">
        /// Tager en ordre ind, som den får fra viewdelen
        /// </param>
        /// <returns>
        /// Returnerer en string, som repræsenterer en email.
        /// </returns>
        public string GetEmail(Order o)
        {
            OwnerEmail = _houseOwnerService.GetById(_orderRepo.GetById(o.OrderId).Property.OwnerId).Email;
            return OwnerEmail;
        }

        /// <summary>
        /// Edit metode til, at redigere tre forskellige properties. Sætter et nyt objekt lig med
        /// den loggede ind gæst. Herefter ændrer den de ønskede properties og opdaterer dem.
        /// </summary>
        /// <param name="id">
        /// Tager et gæste ID ind
        /// </param>
        /// <returns>
        /// Returnerer gæste siden.
        /// </returns>
        public IActionResult OnPostEdit(string id)
        {
            Guest editGuest = _guestService.GetById(id);

            editGuest.FirstName = FirstName;
            editGuest.LastName = LastName;
            editGuest.Phone = Phone;

            _guestService.Update(id, editGuest);
            return RedirectToPage("GuestProfile");
        }

        /// <summary>
        /// Metode til at 'un-favorite' en bolig.
        /// </summary>
        /// <param name="id">
        /// Tager et bolig ID ind
        /// </param>
        /// <returns>
        /// Returnerer gæste siden.
        /// </returns>
        public IActionResult OnPostUnFavorite(int id)
        {
            _favoriteService.Delete(id);

            return RedirectToPage("GuestProfile");
        }

        public IActionResult OnPostCancel(int id)
        {
            _orderRepo.Delete(id);

            return RedirectToPage("GuestProfile");
        }


        #endregion
    }
}