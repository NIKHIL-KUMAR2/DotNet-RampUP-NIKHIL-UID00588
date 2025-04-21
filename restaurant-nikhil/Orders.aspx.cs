using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data.Common;

namespace restaurant_nikhil
{
    public partial class Orders : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null || string.IsNullOrEmpty(Session["userId"].ToString()))
            {
                Response.Redirect("Login.aspx");
            }



            if (!IsPostBack)
            {
                string resId = Request.QueryString["resId"];
                //string userId = Session["userId"].ToString();
                // method to check if current authenticated user owns the restaurant currently not using it
                // OrdersBelongsToOwner(resId, userId)
                if (!string.IsNullOrEmpty(resId) && !Regex.IsMatch(resId, "[^0-9]"))
                {

                    GetOrders();
                }
                else
                {
                    Response.Redirect("ErrorPage.aspx?error=No restaurant found. Invalid Restaurant Id");
                }

            }
        }

        public void GetOrders()
        {
            string rId = Request.QueryString["resId"];
            string uId = Session["userId"].ToString();
            int resId = 0, userId = 0;
            if (int.TryParse(rId, out int x) && int.TryParse(uId, out int y))
            {
                resId = x;
                userId = y;
            }
            else
            {
                Response.Redirect("ErrorPage.aspx?error=No restaurant found");
            }

            string filterQuery = ViewState["FilterQuery"] != null ? ViewState["FilterQuery"].ToString() : "";
            string searchQuery = ViewState["SearchQuery"] != null ? ViewState["SearchQuery"].ToString() : "";


            //create sorting query for orders
            string sortQuery = CreateSortQuery();
            try
            {

                DataTable dt = FetchAllOrdersFromDB(userId, resId, sortQuery, searchQuery, filterQuery);
                if (dt.Rows.Count > 0)
                {
                    OrdersGrid.Visible = true;
                    ViewState["OrdersData"] = dt;
                    OrdersGrid.DataSource = dt;
                    OrdersGrid.DataBind();
                    ErrorLabelOrders.Text = "";

                }
                else
                {
                    ErrorLabelOrders.ForeColor = System.Drawing.Color.Red;
                    if (!IsPostBack)
                    {
                        OrdersForm.Visible = false;
                        Response.Write("No Orders found");

                    }
                    else
                    {
                        ErrorLabelOrders.Text = "No orders match your search or filter please update your search or filter";
                        OrdersGrid.Visible = false;
                    }

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                ErrorLabelOrders.ForeColor = System.Drawing.Color.Red;
                ErrorLabelOrders.Text = "An error occurred while fetching orders. Please try again.";

            }





        }
        private string CreateSortQuery()
        {
            string sortExpression = ViewState["SortExpression"] != null ? ViewState["SortExpression"].ToString() : "";
            string sortDirection = ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC";

            // hashset to check for valid column for sorting
            HashSet<string> allowedColumns = new HashSet<string>
            {
                "status",
                "customerName",
                "id",
                "orderValue",
                "createdAt",
                "updatedAt"
            };

            //if sortExpression is null or contains invalid column then return default value
            if (string.IsNullOrEmpty(sortExpression) || !allowedColumns.Contains(sortExpression))
            {
                return "ORDER BY AO.id ASC";
            }

            //if user wants to sort based on a calculated column
            if (sortExpression == "customerName")
            {
                return "ORDER BY AU.firstName " + sortDirection;
            }

            return "ORDER BY AO." + sortExpression + " " + sortDirection;
        }


        //method to fetch all orders from database based on filter, search and sort expression if provided
        //method takes arguments so that it can be tested without ui

        public DataTable FetchAllOrdersFromDB(int userId, int resId, string sortQuery, string searchQuery = "", string filterQuery = "")
        {

            int searchOrderId = 0;
            //if (int.TryParse(searchQuery, out int x))
            //{
            //    searchOrderId = x;
            //}

            string query = "SELECT AO.id, AO.status, AO.orderValue, AO.createdAt,AO.updatedAt, CONCAT(AU.firstName,' ', AU.lastName) AS customerName FROM AppOrder AO LEFT JOIN AppUser AU on AO.userId = AU.id LEFT JOIN restaurant R on AO.restaurantId= R.id WHERE restaurantId = @resId and R.userId = @CurrentUser ";

            if (!string.IsNullOrEmpty(searchQuery))
            {
                if (int.TryParse(searchQuery, out int x))
                {
                    searchOrderId = x;
                    query += "and AO.id = @SearchById ";
                }
                else
                {
                    query += "and (AU.firstName LIKE @Search or AU.lastName LIKE @Search) ";
                }

            }
            if (!string.IsNullOrEmpty(filterQuery))
            {
                query += "and AO.status = @Filter ";
            }
            string formattedDate = "";
          
            if (DateTime.TryParse(DateTextBox.Text, out DateTime selectedDate))
            {
                formattedDate = selectedDate.ToString("yyyy-MM-dd"); 
                query += "and AO.createdAt >= @FilterByDate ";
                
            }


            query += sortQuery;





            string connStr = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@resId", SqlDbType.Int).Value = resId;
                    cmd.Parameters.Add("@CurrentUser", SqlDbType.Int).Value = userId;

                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        if (searchOrderId != 0)
                        {
                            cmd.Parameters.Add("@SearchById", SqlDbType.Int).Value = searchOrderId;
                        }
                        else
                        {
                            cmd.Parameters.Add("@Search", SqlDbType.VarChar, 100).Value = "%" + searchQuery + "%";
                        }



                    }
                    if (!string.IsNullOrEmpty(filterQuery))
                    {
                        cmd.Parameters.Add("@Filter", SqlDbType.VarChar, 100).Value = filterQuery;
                    }
                    if (query.Contains("@FilterByDate"))
                    {
                        Debug.WriteLine(formattedDate);
                        cmd.Parameters.Add("@FilterByDate", SqlDbType.DateTime).Value = formattedDate;
                    }




                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[Error]: " + ex);
                    throw ex;
                }
            }

        }

        //method to check if current restaurant belongs to user (currently not in used)

        //private bool OrdersBelongsToOwner(string resId, string userId)
        //{
        //    string connStr = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        //    using (SqlConnection conn = new SqlConnection(connStr))
        //    {
        //        try
        //        {
        //            string query = "SELECT 1 FROM restaurant WHERE id = @resId AND userId = @userId";

        //            SqlCommand cmd = new SqlCommand(query, conn);
        //            cmd.Parameters.AddWithValue("@resId", resId);
        //            cmd.Parameters.AddWithValue("@userId", userId);

        //            conn.Open();
        //            var result = cmd.ExecuteScalar();
        //            return result != null;
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine("[Error]:" + ex);
        //            return false;
        //        }

        //    }
        //}
        protected void OrdersGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            //toggle sorting expression and save them in viewstate
            SetSortDirection(sortExpression);
            GetOrders();
        }

        private void SetSortDirection(string column)
        {
            string sortDirection = "ASC";


            if (ViewState["SortExpression"] != null && ViewState["SortDirection"] != null)
            {
                if (ViewState["SortExpression"].ToString() == column)
                {

                    sortDirection = (ViewState["SortDirection"].ToString() == "ASC") ? "DESC" : "ASC";
                }
            }


            ViewState["SortExpression"] = column;
            ViewState["SortDirection"] = sortDirection;


        }

        protected void SearchOrdersBTN_CLICK(object sender, EventArgs e)
        {
            bool isInvalidQuery = Regex.IsMatch(SearchTB.Text, "[^a-zA-Z0-9]");
            if (isInvalidQuery)
            {
                ErrorLabelOrders.Text = "Invalid characters in search";

            }
            else
            {
                
                ViewState["SearchQuery"] = SearchTB.Text;
                GetOrders();
            }


        }

        protected void FilterOrdersByStatusBTN_CLICK(object sender, EventArgs e)
        {
            string filterValue = FilterStatusDDL.SelectedValue;
            if (IsValidFilter(filterValue))
            {
                ViewState["FilterQuery"] = filterValue;
                // showing current filter to user 
                FilterInfoLabel.Text = "Applied filter : " + FilterStatusDDL.SelectedValue;
                GetOrders();
            }
            else
            {
                Response.Redirect("ErrorPage.aspx?error=Invalid Filter Applied");
            }




        }

        private bool IsValidFilter(string filterValue)
        {
            HashSet<string> validFilterValues = new HashSet<string> {
            "pending",
            "confirmed",
            "prepared",
            "delivered",
            "cancelled"
            };
            return validFilterValues.Contains(filterValue);
        }

        protected void ClearFilterBTN_CLICK(object sender, EventArgs e)
        {
            ViewState["SearchQuery"] = null;
            ViewState["FilterQuery"] = null;
            ViewState["SortExpression"] = null;
            DateTextBox.Text = "";
            SearchTB.Text = "";
            FilterInfoLabel.Text = "";
            FilterStatusDDL.SelectedValue = "pending";
            GetOrders();
        }

        protected void OpenOrderDetailsBTN_CLICK(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string orderId = btn.CommandArgument;
            Response.Redirect("OrderDetails.aspx?orderId=" + orderId);
        }


        protected void OrdersGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateStatus")
            {
                //buton from where update status command originated
                Button btn = (Button)e.CommandSource;

                //row in which the command originated
                GridViewRow row = (GridViewRow)btn.NamingContainer;

                //order to be updated
                int orderId = Convert.ToInt32(e.CommandArgument);

                // value of new status that need to be updated in order
                DropDownList ddl = (DropDownList)row.FindControl("StatusUpdateDDL");
                string newStatus = ddl.SelectedValue;
                string currentStatus = row.Cells[5].Text;
                if (newStatus == currentStatus)
                {
                    ErrorLabelOrders.Text = "Please change the status before hiting update at OrderId #" + orderId;
                    return;
                }




                try
                {
                    //method to update order
                    UpdateOrderStatus(orderId, newStatus);

                    // refetch orders
                    GetOrders();

                    ErrorLabelOrders.Text = $"Order #{orderId} status updated to '{newStatus}'.";
                    ErrorLabelOrders.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    ErrorLabelOrders.Text = "Error updating order status: " + ex.Message;
                    ErrorLabelOrders.ForeColor = System.Drawing.Color.Red;
                }

            }
        }

        private void UpdateOrderStatus(int orderId, string status)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

            string query = "update AppOrder set status = @newStatus, updatedAt = @UpdatedAt where id = @orderId;";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@newStatus", SqlDbType.VarChar, 100).Value = status;
                    cmd.Parameters.Add("@orderId", SqlDbType.Int).Value = orderId;
                    cmd.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = DateTime.Now;

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result != 1)
                    {
                        throw new Exception("Order not updated");
                    }



                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[Error at UpdateOrderStatus : Page -> Orders ]: " + ex);
                    throw;
                }
            }
        }
    }
}