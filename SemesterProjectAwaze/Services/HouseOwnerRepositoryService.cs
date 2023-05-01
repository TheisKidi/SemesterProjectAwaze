using AwazeLib.model;
using System.Data.SqlClient;

namespace SemesterProjectAwaze.Services
{
    public class HouseOwnerRepositoryService : IGenericRepositoryService<HouseOwner>
    {
        public HouseOwner Create(HouseOwner houseOwner)
        {
            string sql = "insert into HouseOwner values(@OwnerId, @SurName, @LastName, @Email, @Phone, @IsOwner, @Password, @Address)";

            // forbindelse
            SqlConnection conn = new SqlConnection(Secret.GetConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@OwnerId", houseOwner.OwnerId);
            cmd.Parameters.AddWithValue("@SurName", houseOwner.SurName);
            cmd.Parameters.AddWithValue("@LastName", houseOwner.LastName);
            cmd.Parameters.AddWithValue("@Email", houseOwner.Email);
            cmd.Parameters.AddWithValue("@Phone", houseOwner.Phone);
            cmd.Parameters.AddWithValue("@IsOwner", houseOwner.IsOwner);
            cmd.Parameters.AddWithValue("@Password", houseOwner.Password);
            cmd.Parameters.AddWithValue("@Address", houseOwner.Address);

            int row = cmd.ExecuteNonQuery();

            if (row == 1)
            {
                return houseOwner;
            }
            else
            {
                return null; // eller exception
            }
        }

        public HouseOwner Delete(string id)
        {
            // finder først personen
            HouseOwner p = GetById(id);
            if (p is null)
            {
                return null;
            }

            string sql = "delete from HouseOwner where Id = @OwnerId";

            // forbindelse
            SqlConnection conn = new SqlConnection(Secret.GetConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@OwnerId", id);

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

        public List<HouseOwner> GetAll()
        {
            SqlConnection conn = new SqlConnection(Secret.GetConnectionString);
            conn.Open();

            string sql = "select * from HouseOwner";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            List<HouseOwner> houseOwner = new List<HouseOwner>();
            while (reader.Read())
            {
                houseOwner.Add(ReadHouseOwner(reader));
            }

            return houseOwner;
        }

        private HouseOwner ReadHouseOwner(SqlDataReader reader)
        {
            HouseOwner houseOwner = new HouseOwner();

            houseOwner.OwnerId = reader.GetString(0);
            houseOwner.SurName = reader.GetString(1);
            houseOwner.LastName = reader.GetString(2);
            houseOwner.Email = reader.GetString(3);
            houseOwner.Phone = reader.GetString(4);
            houseOwner.IsOwner = reader.GetBoolean(5);
            houseOwner.Password = reader.GetString(6);
            houseOwner.Address = reader.GetString(7);

            return houseOwner;
        }

        public HouseOwner GetById(string id)
        {
            string sqlInsert = "select * from HouseOwner where Id = @OwnerId";
            List<HouseOwner> list = new List<HouseOwner>();

            using (SqlConnection conn = new SqlConnection(Secret.GetConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                cmd.Parameters.AddWithValue("@OwnerId", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return ReadHouseOwner(reader);
                }
            }

            throw new KeyNotFoundException();
        }

        public HouseOwner Update(string id, HouseOwner houseOwner)
        {
            string sqlInsert = "update HouseOwner " +
                "set SurName = @SurName, LastName = @LastName, " +
                "Email = @Email, Phone = @Phone, Password = @Password, " +
                "Address = @Address where Id = @OwnerId";

            using (SqlConnection conn = new SqlConnection(Secret.GetConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                cmd.Parameters.AddWithValue("@OwnerId", id);
                cmd.Parameters.AddWithValue("@SurName", houseOwner.SurName);
                cmd.Parameters.AddWithValue("@LastName", houseOwner.LastName);
                cmd.Parameters.AddWithValue("@Email", houseOwner.Email);
                cmd.Parameters.AddWithValue("@Phone", houseOwner.Phone);
                cmd.Parameters.AddWithValue("@Password", houseOwner.Password);
                cmd.Parameters.AddWithValue("@Address", houseOwner.Address);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    houseOwner.OwnerId = id;
                }
                else
                {
                    throw new ArgumentException("!!! Could not create a HouseOwner !!!");
                }
            }
            return houseOwner;
        }
    }
}
