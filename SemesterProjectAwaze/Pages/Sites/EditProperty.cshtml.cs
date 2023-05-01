using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace SemesterProjectAwaze.Pages.Sites
{
    [BindProperties]
    public class EditPropertyModel : PageModel
    {
        private IGenericRepositoryService<Property> _propertyService;
        public EditPropertyModel(IGenericRepositoryService<Property> service)
        {
            _propertyService = service;
        }

        [Required (ErrorMessage = "Id is required")]
        [Range(6,6, ErrorMessage = "Id must be 6 characters")]
        public string Id { get; set; }
        [Required(ErrorMessage = "OwnerId is required")]
        public HouseOwner OwnerId { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public Country Country { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "PricePrNight is required")]
        public double PricePrNight { get; set; }
        [Required(ErrorMessage = "Rating is required")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Facilities is required")]
        public Facilities Facilities { get; set; }
        [Required(ErrorMessage = "VR link is required")]
        public string VR { get; set; }

        public void OnGet(string id)
        {
            Property editProperty = _propertyService.GetById(id);

            Country = editProperty.Country;
            Address = editProperty.Address;
            Name = editProperty.Name;
            PricePrNight = editProperty.PricePrNight;
            Rating = editProperty.Rating;
            Description = editProperty.Description;
            VR = editProperty.VR;
            Facilities.Persons = editProperty.Facilities.Persons;
            Facilities.Bedrooms = editProperty.Facilities.Bedrooms;
            Facilities.Bathrooms = editProperty.Facilities.Bathrooms;
            Facilities.Sustainable = editProperty.Facilities.Sustainable;
            Facilities.AllowPets = editProperty.Facilities.AllowPets;
            Facilities.Wifi = editProperty.Facilities.Wifi;
            Facilities.Tv = editProperty.Facilities.Tv;
            Facilities.Type = editProperty.Facilities.Type;
        }

        public IActionResult OnPostEdit(string id)
        {
            Property newValues = new Property(Id, OwnerId.OwnerId, Country, Address, Name, PricePrNight, Rating, Description,
                                                new Facilities(Facilities.Persons, Facilities.Bedrooms, Facilities.Bathrooms,
                                                Facilities.Sustainable, Facilities.AllowPets, Facilities.Wifi, Facilities.Tv, Facilities.Type), VR);

            _propertyService.Update(id, newValues);
            return RedirectToPage("Index");
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Sites/Browse");
        }
    }
}
