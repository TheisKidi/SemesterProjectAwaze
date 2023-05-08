using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwazeLib.model
{
    public enum Country {Denmark, Croatia, Italy, TheNetherlands, Portugal}
    public enum HouseType { HolidayCottage, HollidayApartment, HouseBoat }

    public class Property
    {
        public string Id { get; set; }
        public string OwnerId { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public double PricePrNight { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public string VR { get; set; }
        public int Persons { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public bool Sustainable { get; set; }
        public bool AllowPets { get; set; }
        public bool Wifi { get; set; }
        public int Tv { get; set; }
        public string HouseType { get; set; }

        public Property() : this ("default ID", "DEF001", "Denmark", "default address", "default name", -1, 0, 
            "default description", "default VR link", 0, 0, 0, false, false, false, 0, "HolidayCottage")
        {
        }

        public Property(string id, string ownerId, string country, string address, string name, double pricePrNight, 
            int rating, string description, string vr, int persons, int bedrooms, int bathrooms, bool sustainable, 
            bool allowPets, bool wifi, int tv, string houseType)
        {
            Id = id;
            OwnerId = ownerId;
            Country = country;
            Address = address;
            Name = name;
            PricePrNight = pricePrNight;
            Rating = rating;
            Description = description;
            VR = vr;
            Persons = persons;
            Bedrooms = bedrooms;
            Bathrooms = bathrooms;
            Sustainable = sustainable;
            AllowPets = allowPets;
            Wifi = wifi;
            Tv = tv;
            HouseType = houseType;
        }

        public override string ToString()
        {
            return $"Id: {Id}, HouseOwner: {OwnerId}, Country: {Country}, Address: {Address}, Name: {Name}, " +
                   $"PricePrNight: {PricePrNight}, Rating: {Rating}, Description: {Description}, " +
                   $"VR: {VR}, Persons: {Persons}, Bedrooms: {Bedrooms}, Bathroom: {Bathrooms}, Sustainable: {Sustainable}, " +
                   $"AllowPets: {AllowPets}, Wifi: {Wifi}, Tv: {Tv}, Type: {HouseType}";
        }
    }
}
