using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;

namespace SemesterProjectAwaze.Pages.Sites
{
    public class SelectedPropertyModel : PageModel
    {
        private IGenericRepositoryService<Property> _propRepo;

        public SelectedPropertyModel(IGenericRepositoryService<Property> propRepo)
        {
            _propRepo = propRepo;
        }

        [BindProperty]
        public Property SelectedProperty { get; set; }

        public void OnGet(string id)
        {
            SelectedProperty = _propRepo.GetById(id);
        }
    }
}
