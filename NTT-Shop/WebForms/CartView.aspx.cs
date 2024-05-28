using API_NTT_SHOP.Models;
using Newtonsoft.Json;
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
    public partial class CartView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] != null)
            {
                if (!IsPostBack)
                {
                    if (Session["Cart"] is null)
                    {
                        NoProducts.Visible = true;
                        FinButton.Visible = false;
                    }
                    else
                    {
                        List<Product> products = Session["Cart"] as List<Product>;
                        if (products.Count == 0)
                        {
                            NoProducts.Visible = true;
                            FinButton.Visible = false;
                        }
                        else
                        {
                            NoProducts.Visible = false;
                            FinButton.Visible = true;
                            RepeaterDescID.DataSource = FillCart();
                            RepeaterDescID.DataBind();
                        }
                    }
                }
            }
            else Response.Redirect("http://localhost:63664/WebForms/LoginForm.aspx");
        }
        protected void btnRemoveCart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idProduct = int.Parse(btn.CommandArgument.ToString());
            List<Product> products = Session["Cart"] as List<Product>;
            List<int> itemsToRemove = new List<int>();
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].idProduct == idProduct) itemsToRemove.Add(i);
            }
            for (int i = itemsToRemove.Count; i > 0 ; i--)
            {
                products.RemoveAt(itemsToRemove[i-1]);
            }
            Session["Cart"] = products;
            Response.Redirect("http://localhost:63664/WebForms/CartView.aspx");
        }
        private List<Cart> FillCart() 
        {
            List<Cart> cartList = new List<Cart>();
            List<Product> products = Session["Cart"] as List<Product>;
            bool added = false;

            foreach (Product product in products) 
            {
                if (cartList.Count == 0)
                {
                    Cart cart = new Cart();
                    cart.quantity = 1;
                    cart.product = product;
                    cart.totalPrice = product.rates[0].price;
                    cartList.Add(cart);
                }
                else
                {
                    for (int i = 0; i < cartList.Count; i++)
                    {
                        if (cartList[i].product.idProduct == product.idProduct)
                        {
                            cartList[i].quantity++;
                            cartList[i].totalPrice = cartList[i].totalPrice + product.rates[0].price;
                            added = true;
                        }
                    }
                    if (!added)
                    {
                        Cart cart = new Cart();
                        cart.quantity = 1;
                        cart.product = product;
                        cart.totalPrice = product.rates[0].price;
                        cartList.Add(cart);
                    }
                    added = false;
                }
            }
            return cartList;
        }
        private sbyte InsertOrder(List<Cart> cart)
        {
            sbyte result = -1;
            string url = @"https://localhost:7204/api/Order/InsertOrder";
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            decimal totalPrice = 0;
            int idUser = int.Parse(Session["session-id"].ToString());

            foreach (Cart item in cart)
            {
                OrderDetail orderDetail = new OrderDetail(0, item.product.idProduct, item.totalPrice, item.quantity, item.product);
                orderDetails.Add(orderDetail);
                totalPrice += item.totalPrice;
            }

            var orderData = new { order = new Order(0, DateTime.Now, 1, totalPrice, idUser, orderDetails) };
            string json = JsonConvert.SerializeObject(orderData);

            HttpWebResponse httpResponse = null;

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                result = 1;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("409")) result = 2;
                else if (ex.Message.Contains("400")) result = 0;
            }
            return result;
        }

        protected void Compra_Click(object sender, EventArgs e)
        {
            List<Cart> cartList = FillCart();
            sbyte result = InsertOrder(cartList);

            string script = "alert(\"Error\");";
            switch (result)
            {
                case 0:
                    script = "alert(\"Formato de datos incorrectos\");";
                    break;
                case 1:
                    script = "alert(\"Compra finalizada\");";
                    foreach(var cart in cartList)
                    {
                        var stock = cart.product.stock -cart.quantity;
                        cart.product.stock = stock;
                        UpdateProduct(cart.product);
                    }
                    Session["Cart"] = null;
                    Response.Redirect("http://localhost:63664/WebForms/CartView.aspx");
                    break;
                case 2:
                    script = "alert(\"Conflicto\");";
                    break;
                default:
                    break;
            }
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }

        private sbyte UpdateProduct(Product product)
        {
            sbyte result = -1;
            string url = @"https://localhost:7204/api/Product/updateProduct";
            var productData = new { product = product};
            string json = JsonConvert.SerializeObject(productData);

            HttpWebResponse httpResponse = null;

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "PUT";

                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                result = 1;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("404")) result = 2;
                else if (ex.Message.Contains("400")) result = 0;
            }
            return result;
        }
    }
}