﻿using AwazeLib.model;
using System.Data.SqlClient;

namespace SemesterProjectAwaze.Services
{
    public class GuestRepositoryService : IGenericRepositoryService<Guest>
    {
        public Guest Create(Guest guest)
        {
            string sql = "INSERT INTO [dbo].[Guest] VALUES(@MyBookingId, @FirstName, @LastName, @Email, @Phone, @IsOwner, @Password)";

            // connection to database with ConnectionString
            SqlConnection conn = new SqlConnection(Secret.GetConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MyBookingId", guest.MyBookingId);
            cmd.Parameters.AddWithValue("@FirstName", guest.FirstName);
            cmd.Parameters.AddWithValue("@LastName", guest.LastName);
            cmd.Parameters.AddWithValue("@Email", guest.Email);
            cmd.Parameters.AddWithValue("@Phone", guest.Phone);
            cmd.Parameters.AddWithValue("@IsOwner", guest.IsOwner);
            cmd.Parameters.AddWithValue("@Password", guest.Password);

            int row = cmd.ExecuteNonQuery();

            if (row == 1)
            {
                return guest;
            }
            else
            {
                return null; // or could throw exception instead
            }
        }

        public Guest Delete(string id)
        {
            // find the guest with the GetById() method
            Guest g = GetById(id);
            if (g is null)
            {
                return null;
            }

            string sql = "delete from Guest where MyBookingId = @MyBookingId";

            // connection to database with ConnectionString
            SqlConnection conn = new SqlConnection(Secret.GetConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MyBookingId", id);


            int row = cmd.ExecuteNonQuery();

            if (row == 1)
            {
                return g;
            }
            else
            {
                return null; // or could throw exception instead
            }
        }

        public List<Guest> GetAll()
        {
            SqlConnection conn = new SqlConnection(Secret.GetConnectionString);
            conn.Open();

            string sql = "select * from Guest";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Guest> properties = new List<Guest>();
            while (reader.Read())
            {
                properties.Add(ReadGuest(reader));

            }
            return properties;
        }

        private Guest ReadGuest(SqlDataReader reader)
        {
            Guest guest = new Guest();

            guest.MyBookingId = reader.GetString(0);
            guest.FirstName = reader.GetString(1);
            guest.LastName = reader.GetString(2);
            guest.Email = reader.GetString(3);
            guest.Phone = reader.GetString(4);
            guest.IsOwner = reader.GetBoolean(5);
            guest.Password = reader.GetString(6);

            return guest;
        }


        public Guest GetById(string id)
        {
            string sqlInsert = "select * from Guest where MyBookingId = @Id";
            List<Guest> list = new List<Guest>();

            using (SqlConnection conn = new SqlConnection(Secret.GetConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return ReadGuest(reader);
                }
            }
            throw new KeyNotFoundException();
        }

        public Guest GetByEmail(string email)
        {
            // connection
            SqlConnection conn = new SqlConnection(Secret.GetConnectionString);
            conn.Open();

            string sql = "SELECT * FROM [dbo].[Guest] WHERE Email = @Email";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Email", email);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return ReadGuest(reader);
            }

            throw new KeyNotFoundException();
        }

        public Guest Update(string id, Guest guest)
        {
            string sqlInsert = "update Guest " +
                               "set FirstName = @FirstName, LastName = @LastName, Email = @Email, " +
                               "Phone = @Phone, Password = @Password where MyBookingId = @MyBookingId";

            using (SqlConnection conn = new SqlConnection(Secret.GetConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                cmd.Parameters.AddWithValue("@MyBookingId", id);
                cmd.Parameters.AddWithValue("@FirstName", guest.FirstName);
                cmd.Parameters.AddWithValue("@LastName", guest.LastName);
                cmd.Parameters.AddWithValue("@Email", guest.Email);
                cmd.Parameters.AddWithValue("@Phone", guest.Phone);
                cmd.Parameters.AddWithValue("@Password", guest.Password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    guest.MyBookingId = id;
                }
                else
                {
                    throw new ArgumentException("!!! Could not create a Guest !!!");
                }
            }
            return guest;
        }
    }
}
