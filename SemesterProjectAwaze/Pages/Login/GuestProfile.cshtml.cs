using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;

namespace SemesterProjectAwaze.Pages.Login
{
    public class GuestProfileModel : PageModel
    {
        private IGenericRepositoryService<Guest> _guestRepo;

        public GuestProfileModel(IGenericRepositoryService<Guest> guestRepo)
        {
            _guestRepo = guestRepo;
        }

        public Guest? Profile { get; set; }

        public void OnGet(string email)
        {
            Profile = _guestRepo.GetByEmail(email);
        }
    }
}
