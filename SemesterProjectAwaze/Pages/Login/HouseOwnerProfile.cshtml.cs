using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;

namespace SemesterProjectAwaze.Pages.Login
{
    public class HouseOwnerProfileModel : PageModel
    {

        [BindProperty]
        public HouseOwner LoggedInUser { get; set; }
        [BindProperty]
        public List<Property> Properties { get; set; }

        private IGenericRepositoryService<Property> _propRepo;
        private IGenericRepositoryService<HouseOwner> _ownerRepo;

        public HouseOwnerProfileModel(IGenericRepositoryService<HouseOwner> houseOwner, IGenericRepositoryService<Property> propRepo)
        {
            _ownerRepo = houseOwner;
            _propRepo = propRepo;
        }


        public void OnGet()
        {
            LoggedInUser = _ownerRepo.GetByEmail(SessionHelper.GetProfile(HttpContext).Email);
            Properties = _propRepo.GetAll().Where(p => p.OwnerId == LoggedInUser.OwnerId).ToList();
        }


    }
}
