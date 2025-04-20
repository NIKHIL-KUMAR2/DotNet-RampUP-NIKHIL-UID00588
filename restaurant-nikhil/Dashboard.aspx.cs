using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection.Emit;
using System.Diagnostics;

namespace restaurant_nikhil
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            SignedUserName.Text = "Hello, " + Session["userName"].ToString();
            if (!IsPostBack)
            {

                LoadRestaurants();

            }
        }

        //Method to Retrieve all restaurants of an user
        public void LoadRestaurants()
        {
            
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    if (Session["userId"] == null)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    int userId = Int32.Parse(Session["userId"].ToString());
                    string query = "SELECT id, name, description FROM restaurant WHERE userId = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@id",SqlDbType.Int).Value = userId;

                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        RestaurantGrid.DataSource = dt;
                        RestaurantGrid.DataBind();
                    }
                    else
                    {
                        Response.Write("No restaurants added yet");
                    }


                }catch (Exception ex)
                {
                    Debug.WriteLine("[Error at LoadRestaurants]: " + ex);
                    Response.Write(ex.Message);
                    Response.End();
                }
              

            }
        }
        //Method to redirect user to orders page 
        protected void ViewOrdersBTN_CLICK(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string resId = btn.CommandArgument;

            Response.Redirect("Orders.aspx?resId=" + resId);
        }

        //Method to let user log out
        protected void LogoutBTN_CLICK(object sender, EventArgs e)
        {
            Session["userId"] = null;
            Session["userName"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}