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
        #region instance field
        private IGenericRepositoryService<Property> _propRepo;
        private FavoriteRepositoryService _favRepo;
        private IGenericRepositoryService<Guest> _guestRepo;
        #endregion

        #region constructor
        public SelectedPropertyModel(IGenericRepositoryService<Property> propRepo, IGenericRepositoryService<Guest> guestRepo, FavoriteRepositoryService favRepo)
        {
            _favRepo = favRepo;
            _guestRepo = guestRepo;
            _propRepo = propRepo;
        }
        #endregion

        #region properties
        [BindProperty]
        public Property SelectedProperty { get; set; }
        [BindProperty]
        [Key]
        public int Id { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// Henter bolig id fra /browse siden
        /// </summary>
        /// <param name="id"> tager �t parameter ind, som er id fra en bolig </param>
        public void OnGet(string id)
        {
            SelectedProperty = _propRepo.GetById(id);
        }

        /// <summary>
        /// Tilf�j til favorit metoden. S�tter to objekter lig med den valgte bolig + den loggede ind g�st.
        /// Herefter l�ber metoden igennem en foreach l�kke, som skal tjekke om g�sten allerede har tilf�jet den
        /// valgte bolig til favorit. Hvis dette er tilf�ldet, siden genindl�st. Ellers tilf�jer metoden den valgte
        /// bolig til favorit og opdaterer databasen.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Her videresender metoden med Id som parameter for, 
        /// at 'SelectedProperty' siden ikke genindl�ses med default v�rdier
        /// </returns>
        public IActionResult OnPostAddToFavorite(string id)
        {
            Property selectedProperty = _propRepo.GetById(id);
            Guest loggedInGuest = _guestRepo.GetById(SessionHelper.GetProfile(HttpContext).Id);

            foreach (Favorite fav in _favRepo.GetAll())
            {
                if (fav.PropertyId == selectedProperty.Id && fav.GuestId == loggedInGuest.MyBookingId)
                {
                    return RedirectToPage("SelectedProperty", new { Id = id });
                }
            }
            Favorite newFav = new Favorite(Id, loggedInGuest.MyBookingId, selectedProperty.Id);
            _favRepo.Create(newFav);

            return RedirectToPage("SelectedProperty", new { Id = id }); 
        }
        #endregion
    }
}
