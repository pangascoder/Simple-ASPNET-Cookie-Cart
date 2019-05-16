using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Add_To_Cart_v1.demo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        CookieCartParser cookieCartParser;
        DataTable dtblProducts;

        protected void Page_Load(object sender, EventArgs e)
        {
            cookieCartParser = new CookieCartParser();

            if (!IsPostBack)
            {
                PopulateData();
                
                LVProducts.DataSource = dtblProducts;
                LVProducts.DataBind();
            }
        }

        private void PopulateData()
        {
            dtblProducts = new DataTable();
            dtblProducts.Columns.Add("product_id");
            dtblProducts.Columns.Add("product_name");
            dtblProducts.Columns.Add("product_price");

            DataRow paper = dtblProducts.NewRow();
            paper["product_id"] = 1;
            paper["product_name"] = "Paper";
            paper["product_price"] = 6.00;
            dtblProducts.Rows.Add(paper);

            DataRow pen = dtblProducts.NewRow();
            pen["product_id"] = 2;
            pen["product_name"] = "Pen";
            pen["product_price"] = 4.80;
            dtblProducts.Rows.Add(pen);

            DataRow pencil = dtblProducts.NewRow();
            pencil["product_id"] = 3;
            pencil["product_name"] = "Pencil";
            pencil["product_price"] = 3.20;
            dtblProducts.Rows.Add(pencil);

        }

        protected void LVProducts_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch(e.CommandName)
            {
                case "Add to Cart":
                    var cookieCart = Request.Cookies["CookieCart"];
                    string itemID = e.CommandArgument.ToString();
                    if(cookieCart == null)
                    {
                        cookieCart = new HttpCookie("CookieCart");
                        cookieCart.Value = "";
                        cookieCart.Expires = DateTime.Now.AddDays(30);
                        cookieCart.Value = cookieCartParser.Add(itemID, cookieCart.Value);
                        Response.Cookies.Add(cookieCart);
                    } else
                    {
                        cookieCart.Value = cookieCartParser.Add(itemID, cookieCart.Value);
                        Response.Cookies["CookieCart"].Value = cookieCart.Value;
                    }

                    Response.Redirect("Products.aspx");
                    break;
            }
        }
    }
}