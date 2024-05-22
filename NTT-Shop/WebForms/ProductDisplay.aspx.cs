using Newtonsoft.Json.Linq;
using NTT_Shop.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTT_Shop.WebForms
{
    public partial class ProductDisplay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] != null) 
            {
                int id = int.Parse(Request.QueryString["id"].ToString());
                Product product = GetProduct(id);
                ProductNameID.Text = product.descriptions[0].title;
                ProdPriceID.Text = product.rates[0].price.ToString();
                RepeaterDescID.DataSource = product.descriptions;
                RepeaterDescID.DataBind();
            }
            else Response.Redirect("http://localhost:63664/WebForms/LoginForm.aspx");
        }
        protected void btnAddCart_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"].ToString());
            Product item = GetProduct(id);
            int quantity = int.Parse(DDnumber.SelectedItem.Text);
            List<Product> products = Session["Cart"] as List<Product>;
            for (int i = 0; i < quantity; i++)
            {
                if (products is null)
                {
                    products = new List<Product>();
                    products.Add(item);
                    Session["Cart"] = products;
                }
                else
                {
                    products.Add(item);
                    Session["Cart"] = products;
                }
            }
            string script = "alert(\"Productos añadidos al carrito\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
            Response.Redirect("http://localhost:63664/WebForms/ProductsPage.aspx");
        }
        private Product GetProduct(int id)
        {
            Product product;

            string url = @"https://localhost:7204/api/Product/getProduct/";
            string currentLanguage = Session["session-language"].ToString();
            url = url + id + "/" + currentLanguage;

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject jsonObject = JObject.Parse(result);
                    product = jsonObject["product"].ToObject<Product>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return product;
        }
    }
}