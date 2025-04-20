using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace restaurant_nikhil
{
    public partial class OrderDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            string userId = Session["userId"].ToString();
            if (!IsPostBack)
            {
                string orderId = Request.QueryString["orderId"];

                if (!string.IsNullOrEmpty(orderId) && DoesRestaurantBelongsToOwner(userId, orderId))
                {
                    LoadOrderDetails(orderId);
                }
                else
                {
                    Response.Write("No order details found");
                }

            }
        }
        public void LoadOrderDetails(string orderId)
        {


            string connStr = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string query = "SELECT MI.id, OI.orderId, OI.itemPrice, OI.quantity, MI.itemName,ME.menuName from orderItem OI LEFT JOIN menuItem MI on OI.menuItemId = MI.id LEFT JOIN menu ME on MI.menuId= ME.id WHERE OI.orderId = @orderId;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@orderId", orderId);

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        OrderDetailsGrid.DataSource = dt;
                        OrderDetailsGrid.DataBind();
                    }
                    else
                    {

                    }



                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[Error]:" + ex);
                }
                finally
                {
                    conn.Close();
                }

            }
        }

        public bool DoesRestaurantBelongsToOwner(string userId, string orderId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string query = "SELECT AO.id FROM AppOrder AO LEFT JOIN restaurant R on AO.restaurantId = R.id WHERE R.userId = @userId and AO.id=@orderId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@orderId", orderId);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    
                    return result != null;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[Error]:" + ex);
                    return false;
                }
               

            }
        }
    }
}