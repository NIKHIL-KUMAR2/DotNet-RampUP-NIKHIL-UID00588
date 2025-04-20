using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using restaurant_nikhil.Model;
using restaurant_nikhil.Services;

namespace restaurant_nikhil
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If session is still active then redirect user to dashboard
            if (Session["userId"] != null)
            {
                Response.Redirect("Dashboard.aspx");
            }
        }

        //Login button click handler
        protected void loginBTN_Click(object sender, EventArgs e)
        {

            string passByUser = passwordTB.Text; //password entered by user
            string email = usernameTB.Text;

            //created LoginService class for authentication logic
            LoginService loginService = new LoginService();

            //Login result model
            LoginResult result=loginService.AuthenticateUser(email, passByUser);
            Debug.WriteLine("This worked" + result.Success);

            if (result.Success)
            {
                Session["userId"] = result.UserId;
                Session["userName"] = result.FirstName;
                Response.Redirect("Dashboard.aspx");

            }
            else
            {
                Response.Write($"<script>alert('{result.ErrorMessage}')</script>");
            }



        }

        protected void RedirectToSignup(object sender, EventArgs e)
        {
            Response.Redirect("Signup.aspx");
        }
    }
}