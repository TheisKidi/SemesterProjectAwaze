using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

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
        public decimal PricePrNight { get; set; }
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
        public string PromoImg { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }


        public Property() : this ("default ID", "DEF001", "Denmark", "default address", "default name", -1, 0, 
            "default description", "default VR link", 0, 0, 0, false, false, false, 0, "HolidayCottage", "", "", "", "")
        {
        }

        public Property(string id, string ownerId, string country, string address, string name, decimal pricePrNight, 
            int rating, string description, string vr, int persons, int bedrooms, int bathrooms, bool sustainable, 
            bool allowPets, bool wifi, int tv, string houseType, string promoImg, string img1, string img2, string img3)
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
            PromoImg = promoImg;
            Img1 = img1;
            Img2 = img2;
            Img3 = img3;
        }

        public override string ToString()
        {
            return $"Id: {Id}, HouseOwner: {OwnerId}, Country: {Country}, Address: {Address}, Name: {Name}, " +
                   $"PricePrNight: {PricePrNight}, Rating: {Rating}, Description: {Description}, " +
                   $"VR: {VR}, Persons: {Persons}, Bedrooms: {Bedrooms}, Bathroom: {Bathrooms}, Sustainable: {Sustainable}, " +
                   $"AllowPets: {AllowPets}, Wifi: {Wifi}, Tv: {Tv}, Type: {HouseType}";
        }

//----------------------------------------------------
//Database setup
//----------------------------------------------------
//property
//| id | houseid | name | rating | fk_promo_img | fk_imgs |
//| 1  | ABC123 | mithus | 4      | 3            | 1,2,3 |

//images
//| id | url | 
//| 1  | /assets/house/nice_house.jpg |
//| 2  | /assets/house/nice_house2.jpg |
//| 3  | /assets/house/promo_house.jpg |
//| 4  | /assets/house/boat_house.jpg |


//----------------------------------------------------
///browse: Vis alle properties
//----------------------------------------------------
//SELECT* FROM property RIGHT JOIN property.fk_main_img = images.id;


//----------------------------------------------------
///abc123: Selected house
//----------------------------------------------------
//// Snup al data omkring huset og gem i propertydata
//Property propertydata = "SELECT * FROM property RIGHT JOIN property.fk_main_img = images.id WHERE houseid = '<url houseid(abc123)>'";

//        // Snup al data omkring billederne der ligger på huset ved at tilgå fk_imgs der kommer tilbage
//        fra første query.
//        PropertyImages propertyImages = "SELECT * FROM images WHERE id in (propertydata.fk_imgs)" 

//// Render lortet:
//foreach (propertydata as property) 
//{
//  <div class="images">
//    foreach (Image image in propertyImages) 
//    {
//      <img src = "image.url" />
//    }
//  </div>
//  <p>property.name</p>
//}

    }
}
