using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SemesterProjectAwaze.Pages.Login
{
    public class CreateGuestModel : PageModel
    {
        [BindProperty]
        [Required]
        public string SurName { get; set; }
        [BindProperty]
        [Required]
        public string LastName { get; set; }
        [BindProperty]
        [Required]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        public string Phone { get; set; }
        public bool IsOwner { get; set; }
        [BindProperty]
        [Required]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Guest newGuest = new Guest();

            return RedirectToPage("/Profile");
        }
    }
}
