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
        [Required(ErrorMessage = "Amount of persons is required")]
        public int Persons { get; set; }
        [Required(ErrorMessage = "Amount of bedrooms is required")]
        public int Bedrooms { get; set; }
        [Required(ErrorMessage = "Amount of Bathrooms is required")]
        public int Bathrooms { get; set; }
        [Required(ErrorMessage = "Is this house sustainable?")]
        public bool Sustainable { get; set; }
        [Required(ErrorMessage = "Does this house allow pets?")]
        public bool AllowPets { get; set; }
        [Required(ErrorMessage = "Is WIFI available in this house?")]
        public bool Wifi { get; set; }
        [Required(ErrorMessage = "Amount of TV's required")]
        public int Tv { get; set; }
        [Required(ErrorMessage = "House type is required")]
        public HouseType Type { get; set; }
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
            Persons = editProperty.Facilities.Persons;
            Bedrooms = editProperty.Facilities.Bedrooms;
            Bathrooms = editProperty.Facilities.Bathrooms;
            Sustainable = editProperty.Facilities.Sustainable;
            AllowPets = editProperty.Facilities.AllowPets;
            Wifi = editProperty.Facilities.Wifi;
            Tv = editProperty.Facilities.Tv;
            Type = editProperty.Facilities.Type;
        }

        public IActionResult OnPostEdit(string id)
        {
            Property newValues = new Property(Id, OwnerId.OwnerId, Country, Address, Name, PricePrNight, Rating, Description,
                                                new Facilities(Persons, Bedrooms, Bathrooms, Sustainable, AllowPets, Wifi, Tv, Type), VR);

            _propertyService.Update(id, newValues);
            return RedirectToPage("Index");
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Sites/Browse");
        }
    }
}
