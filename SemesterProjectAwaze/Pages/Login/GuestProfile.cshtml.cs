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
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Phone { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string MyBookingId { get; set; }

        private IGenericRepositoryService<Property> _propRepo;
        private IGenericRepositoryService<Guest> _guestRepo;
        private FavoriteRepositoryService _favRepo;

        public GuestProfileModel(IGenericRepositoryService<Guest> guestRepo, IGenericRepositoryService<Property> propRepo,
            FavoriteRepositoryService favRepo)
        {
            _favRepo = favRepo;
            _guestRepo = guestRepo;
            _propRepo = propRepo;
        }


        public void OnGet()
        {


            LoggedInUser = _guestRepo.GetByEmail(SessionHelper.GetProfile(HttpContext).Email);
            
            try
            {
                FavoriteList = _favRepo.GetFavoritesByUserEmail(LoggedInUser.Email);
            }
            catch(Exception ex)
            {
                FavoriteList = null;
            }
            

            Guest editGuest = _guestRepo.GetById(LoggedInUser.MyBookingId);

            FirstName = editGuest.FirstName;
            LastName = editGuest.LastName;
            Email = editGuest.Email;
            Phone = editGuest.Phone;
            Password = editGuest.Password;
        }

        public IActionResult OnPostEdit(string id)
        {
            Guest editGuest = _guestRepo.GetById(id);

            editGuest.FirstName = FirstName;
            editGuest.LastName = LastName;
            editGuest.Email = Email;
            editGuest.Phone = Phone;
            editGuest.Password = Password;

            _guestRepo.Update(id, editGuest);
            return RedirectToPage("GuestProfile");
        }


    }
}
