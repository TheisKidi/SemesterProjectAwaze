using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;
using System.ComponentModel.DataAnnotations;

namespace SemesterProjectAwaze.Pages.Sites
{
    [BindProperties]
    public class CreatePropertyModel : PageModel
    {
        private IGenericRepositoryService<Property> _repo;

        public CreatePropertyModel(IGenericRepositoryService<Property> repo)
        {
            _repo = repo;
        }

        [Required, MaxLength(6)]
        public string Id { get; set; }
        public HouseOwner HouseOwner { get; set; }
        public Country Country { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public double PricePrNight { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public Facilities Facilities { get; set; }
        public string VR { get; set; }


        public void OnGet()
        {
        }
    }
}
