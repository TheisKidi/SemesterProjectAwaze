using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;
using System;

namespace SemesterProjectAwaze.Pages.Login
{
    public class GuestProfileModel : PageModel
    {
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

        private IGenericRepositoryService<Guest> _guestRepo;
        private IGenericRepositoryService<HouseOwner> _ownerRepo;
        private FavoriteRepositoryService _favRepo;
        private OrderRepositoryServiceDB _orderRepo;

        public GuestProfileModel(IGenericRepositoryService<Guest> guestRepo,
            FavoriteRepositoryService favRepo,
            OrderRepositoryServiceDB orderRepo,
            IGenericRepositoryService<HouseOwner> ownerRepo)
        {
            _favRepo = favRepo;
            _guestRepo = guestRepo;
            _orderRepo = orderRepo;
            _ownerRepo = ownerRepo;
        }


        public void OnGet()
        {
            LoggedInUser = _guestRepo.GetById(SessionHelper.GetProfile(HttpContext).Id);
            FavoriteList = _favRepo.GetFavoritesByUserEmail(LoggedInUser.Email);
            OrdersList = _orderRepo.GetOrderByUserEmail(LoggedInUser.Email);



            Guest editGuest = _guestRepo.GetById(LoggedInUser.MyBookingId);

            FirstName = editGuest.FirstName;
            LastName = editGuest.LastName;
            Phone = editGuest.Phone;
        }

        public string GetEmail(Order o)
        {
            OwnerEmail = _ownerRepo.GetById(_orderRepo.GetById(o.OrderId).Property.OwnerId).Email;
            return OwnerEmail;
        }

        public IActionResult OnPostEdit(string id)
        {
            Guest editGuest = _guestRepo.GetById(id);

            editGuest.FirstName = FirstName;
            editGuest.LastName = LastName;
            editGuest.Phone = Phone;

            _guestRepo.Update(id, editGuest);
            return RedirectToPage("GuestProfile");
        }

        public IActionResult OnPostUnFavorite(int id)
        {
            _favRepo.Delete(id);

            return RedirectToPage("GuestProfile");
        }

    }
}