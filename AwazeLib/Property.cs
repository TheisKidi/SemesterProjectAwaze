using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwazeLib
{
    public enum Country {Denmark, Croatia, Italy, TheNetherlands, Portugal}

    public class Property
    {
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

        public Property() : this ("default ID", HouseOwner, Country.Denmark, "default address", "default name", -1, 0, "default description", new Facilities(), "default VR link")
        {
        }

        public Property(string id, HouseOwner houseOwner, Country country, string address, string name, double pricePrNight, int rating, string description, Facilities facilities, string vr)
        {
            Id = id;
            HouseOwner = houseOwner;
            Country = country;
            Address = address;
            Name = name;
            PricePrNight = pricePrNight;
            Rating = rating;
            Description = description;
            Facilities = facilities;
            VR = vr;
        }

        public override string ToString()
        {
            return $"Id: {Id}, HouseOwner: {HouseOwner}, Country: {Country}, Address: {Address}, Name: {Name}, " +
                   $"PricePrNight: {PricePrNight}, Rating: {Rating}, Description: {Description}, " +
                   $"Facilities: {Facilities}, VR: {VR}";
        }
    }
}
