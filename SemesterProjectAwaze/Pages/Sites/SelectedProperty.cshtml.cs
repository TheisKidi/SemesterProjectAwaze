using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SemesterProjectAwaze.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemesterProjectAwaze.Pages.Sites
{
    [Table("Favorite")]
    public class SelectedPropertyModel : PageModel
    {
        private IGenericRepositoryService<Property> _propRepo;
        private FavoriteRepositoryService _favRepo;
        private IGenericRepositoryService<Guest> _guestRepo;


        public SelectedPropertyModel(IGenericRepositoryService<Property> propRepo, IGenericRepositoryService<Guest> guestRepo, FavoriteRepositoryService favRepo)
        {
            _favRepo = favRepo;
            _guestRepo = guestRepo;
            _propRepo = propRepo;
        }

        [BindProperty]
        public Property SelectedProperty { get; set; }
        [BindProperty]
        [Key]
        public int Id { get; set; }

        public void OnGet(string id)
        {
            SelectedProperty = _propRepo.GetById(id);
        }

        public IActionResult OnPostAddToFavorite(string id)
        {
            Property selectedProperty = _propRepo.GetById(id);
            Guest loggedInGuest = _guestRepo.GetByEmail(SessionHelper.GetProfile(HttpContext).Email);

            Favorite newFav = new Favorite(Id, loggedInGuest.MyBookingId, selectedProperty.Id);
            _favRepo.Create(newFav);

            return Page();
        }


    }
}
