using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Add_To_Cart_v1.demo
{
    public partial class AddToCartDemo : System.Web.UI.MasterPage
    {
        CookieCartParser cookieCartParser;

        protected void Page_Load(object sender, EventArgs e)
        {
            cookieCartParser = new CookieCartParser();

            if(!IsPostBack)
            {
                var cookieCart = Request.Cookies["CookieCart"];

                if(cookieCart != null)
                {
                    LblItemNumberinCart.Text = cookieCartParser.GetNumberOfItems(cookieCart.Value).ToString();
                } else
                {
                    LblItemNumberinCart.Text = "0";
                }
            }
        }

    }
}