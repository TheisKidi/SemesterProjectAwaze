using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;

namespace SemesterProjectAwaze.Pages.Login
{
    [BindProperties]
    public class CreateHouseOwnerModel : PageModel
    {

        private IGenericRepositoryService<HouseOwnerRepositoryService> _repo;

        public CreateHouseOwnerModel(IGenericRepositoryService<HouseOwnerRepositoryService> repo)
        {
            _repo = repo;
        }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }


        public void OnGet()
        {

        }



    }
}
