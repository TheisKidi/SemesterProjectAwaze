using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;

namespace SemesterProjectAwaze.Pages.Login
{
    public class HouseOwnerProfileModel : PageModel
    {
        #region instance field
        private IGenericRepositoryService<Property> _propertyService;
        private IGenericRepositoryService<HouseOwner> _ownerRepo;
        #endregion

        #region constructor
        public HouseOwnerProfileModel(IGenericRepositoryService<HouseOwner> houseOwner,
                              IGenericRepositoryService<Property> propertyService)
        {
            _ownerRepo = houseOwner;
            _propertyService = propertyService;
        }
        #endregion

        #region properties
        [BindProperty]
        public HouseOwner LoggedInUser { get; set; }
        [BindProperty]
        public List<Property> Properties { get; set; }
        #endregion

        #region method
        /// <summary>
        /// OnGet henter den profil som er logget ind og henter alle husejerens boliger,
        /// så de kan blive vist på viewdelen.
        /// </summary>
        public void OnGet()
        {
            LoggedInUser = _ownerRepo.GetById(SessionHelper.GetProfile(HttpContext).Id);
            Properties = _propertyService.GetAll().Where(p => p.OwnerId == LoggedInUser.OwnerId).ToList();
        }
        #endregion
    }
}
