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

        private IGenericRepositoryService<Property> _propRepo;
        private IGenericRepositoryService<Guest> _guestRepo;

        public GuestProfileModel(IGenericRepositoryService<Guest> guestRepo, IGenericRepositoryService<Property> propRepo)
        {
            _guestRepo = guestRepo;
            _propRepo = propRepo;
        }


        public void OnGet()
        {
            LoggedInUser = _guestRepo.GetByEmail(SemesterProjectAwaze.Pages.Login.GuestLoginModel.EmailId);
            Properties = _propRepo.GetAll();
        }


    }
}
