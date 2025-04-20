using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using restaurant_nikhil.Model;

namespace restaurant_nikhil.Services
{
    public class LoginService
    {
        private readonly string _connectionString;

        public LoginService()
        {
            //_connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=restaurant;Integrated Security=True;TrustServerCertificate=True";
        }
        public LoginResult AuthenticateUser(string email,string password)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "select * from AppUser where email = @Email and role = 'owner';";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string hashedPassword = reader["password"].ToString();
                                string firstName = reader["firstName"].ToString();
                                string id = reader["id"].ToString();

                                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
                                if (isPasswordCorrect)
                                {
                                    return LoginResult.SuccessResult(id, firstName);
                                }
                                else
                                {
                                    return LoginResult.FailureResult("Wrong Password");
                                    
                                }
                            }
                        }
                        else
                        {
                            return LoginResult.FailureResult("Admin does not exist");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[LoginService Error]: " + ex);
                    return LoginResult.FailureResult("An error occurred during login");
                }
            }

            return LoginResult.FailureResult("Unknown error");
        }
    }
    
}