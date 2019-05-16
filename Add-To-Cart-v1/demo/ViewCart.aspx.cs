using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Add_To_Cart_v1.demo
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        CookieCartParser cookieCartParser;
        Dictionary<string, string> dictionaryCart;
        DataTable dtblProducts;
        DataTable dtblCart;

        protected void Page_Load(object sender, EventArgs e)
        {
            cookieCartParser = new CookieCartParser();

            var cookieCart = Request.Cookies["CookieCart"];
            
            if(cookieCart != null)
            {
                dictionaryCart = cookieCartParser.ToDictionary(cookieCart.Value);

                CreateDataTableCart();

                if(dtblCart.Rows.Count > 0)
                {
                    GVCart.DataSource = dtblCart;
                    GVCart.DataBind();
                }
            }
        }

        private void CreateDataTableCart()
        {
            PopulateData();

            dtblCart = new DataTable();

            dtblCart.Columns.Add("product_id");
            dtblCart.Columns.Add("product_name");
            dtblCart.Columns.Add("product_price");
            dtblCart.Columns.Add("product_quantity");
            dtblCart.Columns.Add("product_item_total");

            foreach(KeyValuePair<string, string> item in dictionaryCart)
            {
                DataRow[] selectedRow = dtblProducts.Select("product_id = " + item.Key);

                DataRow row = dtblCart.NewRow();
                row["product_id"] = int.Parse(item.Key);
                row["product_name"] = selectedRow[0]["product_name"];
                row["product_price"] = selectedRow[0]["product_price"];
                row["product_quantity"] = item.Value;
                row["product_item_total"] = double.Parse(row["product_price"].ToString()) * int.Parse(item.Value);

                dtblCart.Rows.Add(row);
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

        protected void GVCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch(e.CommandName)
            {
                case "Remove From Cart":
                    string itemID = e.CommandArgument.ToString();
                    var cookieCart = Request.Cookies["CookieCart"];
                    cookieCart.Value = cookieCartParser.Remove(itemID, cookieCart.Value);
                    Response.Cookies["CookieCart"].Value = cookieCart.Value;

                    Response.Redirect("ViewCart.aspx");
                    break;
            }
        }
    }
}