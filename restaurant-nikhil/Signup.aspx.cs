using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace restaurant_nikhil
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void signupBTN_Click(object sender, EventArgs e)
        {
            string email = emailTBreg.Text;
            string pass = passwordTBreg.Text;
            string firstName = firstNameTB.Text;
            string lastName = lastNameTB.Text;
            string passhash = BCrypt.Net.BCrypt.HashPassword(pass);
            string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string loginq = "insert into AppUser (firstName,lastName,email,password,role,balance) values (@firstName,@lastName,@email,@password,'customer',0.00);";
                    SqlCommand cmd = new SqlCommand(loginq, conn);
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", passhash);
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('An error has occured')</script>");

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[Error]: " + ex);
                }
               
            }




        }

        protected void RedirectToLogin(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}