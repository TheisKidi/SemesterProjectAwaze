using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;

namespace SemesterProjectAwaze.Pages.Login
{
    public class GuestProfileModel : PageModel
    {
        [BindProperty]
        public Guest LoggedInUser { get; set; }
        [BindProperty]
        public List<Property> Properties { get; set; }

        private LoggedInUser _eee;

        private ILoginService _loginService;
        private IGenericRepositoryService<Property> _propRepo;
        private IGenericRepositoryService<Guest> _guestRepo;

        public GuestProfileModel(IGenericRepositoryService<Guest> guestRepo, IGenericRepositoryService<Property> propRepo)
        {
            _guestRepo = guestRepo;
            _propRepo = propRepo;
        }


        public void OnGet(string email)
        {
            LoggedInUser = _guestRepo.GetByEmail(SessionHelper.GetProfile(HttpContext).Email);
            Properties = _propRepo.GetAll();
        }


    }
}
