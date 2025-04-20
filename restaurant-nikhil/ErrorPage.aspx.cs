using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace restaurant_nikhil
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string errorMessage = Request.QueryString["error"].ToString();
            ErrorMessageLabel.Text = errorMessage;
        }
    }
}