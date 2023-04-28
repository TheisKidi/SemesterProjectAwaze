namespace AwazeLib
{

    public enum HouseType {HolidayCottage, HollidayApartment, HouseBoat}
    public class Facilities
    {
        public int Persons { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public bool Sustainable { get; set; }
        public bool AllowPets { get; set; }
        public bool Wifi { get; set; }
        public int Tv { get; set; }
        public HouseType Type { get; set;}

        public Facilities(int persons, int bedrooms, int bathrooms, bool sustainable, bool allowPets, bool wifi, int tv, HouseType type)
        {
            Persons = persons;
            Bedrooms = bedrooms;
            Bathrooms = bathrooms;
            Sustainable = sustainable;
            AllowPets = allowPets;
            Wifi = wifi;
            Tv = tv;
            Type = type;
        }   

        public Facilities() : this(0, 0, 0, false, false, false, false, HouseType.HolidayCottage) 
        {
        }

        public override string ToString()
        {
            return $"Perons: {Persons}, Bedrooms: {Bedrooms}, Bathrooms: {Bathrooms}, Sustainable: {Sustainable}, Is pets allowed: {AllowPets}, Wifi: {Wifi}, Tv: {Tv}, HouseType: {HouseType}";
        }

    }
}