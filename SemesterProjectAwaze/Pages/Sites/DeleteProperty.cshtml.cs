using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;

namespace SemesterProjectAwaze.Pages.Sites
{
    public class DeletePropertyModel : PageModel
    {
        private IGenericRepositoryService<Property> _propertyService;

        public DeletePropertyModel(IGenericRepositoryService<Property> service)
        {
            _propertyService = service;
        }

        public Property Property { get; set; }

        public void OnGet(string id)
        {
            Property = _propertyService.GetById(id);
        }

        public IActionResult OnPostDelete(string id)
        {
            _propertyService.Delete(id);
            return RedirectToPage("Index");
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("Index");
        }
    }
}
