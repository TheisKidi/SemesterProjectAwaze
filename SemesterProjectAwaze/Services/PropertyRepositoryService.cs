using AwazeLib.model;
using System.Data.SqlClient;

namespace SemesterProjectAwaze.Services
{
    public class PropertyRepositoryService : IGenericRepositoryService<Property>
    {

        public Property Create(Property property)
        {
            string sql = "insert into Property values(@Id, @OwnerId, @Country, @Address, @Name, @PricePrNight, @Rating, @Description, @VR, @Persons, " +
                         "@Bedrooms, @Bathrooms, @Sustainable, @AllowPets, @Wifi, @Tv, @HouseType)";

            // forbindelse
            SqlConnection conn = new SqlConnection(Secret.GetConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", property.Id);
            cmd.Parameters.AddWithValue("@OwnerId", property.HouseOwner.OwnerId);
            cmd.Parameters.AddWithValue("@Country", property.Country);
            cmd.Parameters.AddWithValue("@Address", property.Address);
            cmd.Parameters.AddWithValue("@Name", property.Name);
            cmd.Parameters.AddWithValue("@PricePrNight", property.PricePrNight);
            cmd.Parameters.AddWithValue("@Rating", property.Rating);
            cmd.Parameters.AddWithValue("@Description", property.Description);
            cmd.Parameters.AddWithValue("@VR", property.VR);
            cmd.Parameters.AddWithValue("@Persons", property.Facilities.Persons);
            cmd.Parameters.AddWithValue("@Bedrooms", property.Facilities.Bedrooms);
            cmd.Parameters.AddWithValue("@Bathrooms", property.Facilities.Bathrooms);
            cmd.Parameters.AddWithValue("@Sustainable", property.Facilities.Sustainable);
            cmd.Parameters.AddWithValue("@AllowPets", property.Facilities.AllowPets);
            cmd.Parameters.AddWithValue("@Wifi", property.Facilities.Wifi);
            cmd.Parameters.AddWithValue("@Tv", property.Facilities.Tv);
            cmd.Parameters.AddWithValue("@HouseType", property.Facilities.Type);


            int row = cmd.ExecuteNonQuery();

            if (row == 1)
            {
                return property;
            }
            else
            {
                return null; // eller exception
            }
        }

        public Property Delete(string id)
        {
            // finder først personen
            Property p = GetById(id);
            if (p is null)
            {
                return null;
            }

            string sql = "delete from Property where Id = @Id";

            // forbindelse
            SqlConnection conn = new SqlConnection(Secret.GetConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);


            int row = cmd.ExecuteNonQuery();

            if (row == 1)
            {
                return p;
            }
            else
            {
                return null; // eller exception
            }
        }

        public List<Property> GetAll()
        {
            SqlConnection conn = new SqlConnection(Secret.GetConnectionString);
            conn.Open();

            string sql = "select * from Property";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Property> properties = new List<Property>();
            while (reader.Read())
            {
                properties.Add(ReadProperty(reader));

            }
            return properties;
        }

        private Property ReadProperty(SqlDataReader reader)
        {
            Property property = new Property();

            property.Id = reader.GetString(0);
            property.HouseOwner.OwnerId = reader.GetString(1);
            property.Country = Enum.Parse<Country>(reader.GetString(2));
            property.Address = reader.GetString(3);
            property.Name = reader.GetString(4);
            property.PricePrNight = reader.GetDouble(5);
            property.Rating = reader.GetInt32(6);
            property.Description = reader.GetString(7);
            property.VR = reader.GetString(8);
            property.Facilities.Persons = reader.GetInt32(8);
            property.Facilities.Bedrooms = reader.GetInt32(9);
            property.Facilities.Bathrooms = reader.GetInt32(10);
            property.Facilities.Sustainable = reader.GetBoolean(11);
            property.Facilities.AllowPets = reader.GetBoolean(12);
            property.Facilities.Wifi = reader.GetBoolean(13);
            property.Facilities.Tv = reader.GetInt32(14);
            property.Facilities.Type = Enum.Parse<HouseType>(reader.GetString(15));

            return property;
        }

        public Property GetById(string id)
        {
            string sqlInsert = "select * from Propery where Id = @Id";
            List<Property> list = new List<Property>();

            using (SqlConnection conn = new SqlConnection(Secret.GetConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return ReadProperty(reader);
                }
            }

            throw new KeyNotFoundException();
        }

        public Property Update(string id, Property property)
        {
            string sqlInsert = "update Property " +
                "set Id = @Id, OwnerId = @OwnerId, Country = @Country, Address = @Address, " +
                "Name = @Name, PricePrNight = @PricePrNight, Rating = @Rating, Description = @Description, " +
                "VR = @VR, Persons = @Persons, Bedrooms = @Bedrooms, Bathrooms = @Bathrooms, Sustainable = @Sustainable, " +
                "Allowpets = @AllowPets, Wifi = @Wifi, Tv = @Tv, HouseType = @HouseType" +
                "where Id = @Id";

            using (SqlConnection conn = new SqlConnection(Secret.GetConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@OwnerId", property.HouseOwner.OwnerId);
                cmd.Parameters.AddWithValue("@Country", property.Country.ToString());
                cmd.Parameters.AddWithValue("@Address", property.Address);
                cmd.Parameters.AddWithValue("@Name", property.Name);
                cmd.Parameters.AddWithValue("@PricePrNight", property.PricePrNight);
                cmd.Parameters.AddWithValue("@Rating", property.Rating);
                cmd.Parameters.AddWithValue("@Description", property.Description);
                cmd.Parameters.AddWithValue("@VR", property.VR);
                cmd.Parameters.AddWithValue("@Persons", property.Facilities.Persons);
                cmd.Parameters.AddWithValue("@Bedrooms", property.Facilities.Bedrooms);
                cmd.Parameters.AddWithValue("@Bathrooms", property.Facilities.Bathrooms);
                cmd.Parameters.AddWithValue("@Sustainable", property.Facilities.Sustainable);
                cmd.Parameters.AddWithValue("@AllowPets", property.Facilities.AllowPets);
                cmd.Parameters.AddWithValue("@Wifi", property.Facilities.Wifi);
                cmd.Parameters.AddWithValue("@Tv", property.Facilities.Tv);
                cmd.Parameters.AddWithValue("@HouseType", property.Facilities.Type.ToString());

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    property.Id = id;
                }
                else
                {
                    throw new ArgumentException("!!! Could not create a property !!!");
                }

            }

            return property;
        }
    }
}
