using API_NTT_SHOP.Models;
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
    public partial class ProductsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] != null)
            {
                if (!IsPostBack)
                {
                    List<Product> products = GetAllProducts();
                    ProductListView.DataSource = products;
                    ProductListView.DataBind();

                }
        }
            else Response.Redirect("http://localhost:63664/WebForms/LoginForm.aspx");
        }

        protected void btnAddCart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idProduct = int.Parse(btn.CommandArgument.ToString());
            Product item = GetProduct(idProduct);
            List<Product> products = Session["Cart"] as List<Product>;
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
        private List<Product> GetAllProducts()
        {
            List<Product> list = new List<Product>();
            List<Product> listaDefinitiva = new List<Product>();

            string url = @"https://localhost:7204/api/Product/getAllProducts/";
            string currentLanguage = Session["session-language"].ToString();
            url = url + currentLanguage;

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject jsonObject = JObject.Parse(result);
                    JArray jsonArray = jsonObject["products"].ToObject<JArray>();
                    list = jsonArray.ToObject<List<Product>>();
                    
                    foreach (Product producto in list)
                    {
                        if(producto.descriptions.Count > 0) listaDefinitiva.Add(producto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return listaDefinitiva;
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

        protected void ProductListView_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager pager = ProductListView.FindControl("DataPager1") as DataPager;

            pager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);

            List<Product> products = GetAllProducts();
            ProductListView.DataSource = products;
            ProductListView.DataBind();
        }
    }
}