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
    public partial class MyOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] != null)
            {
                if (!IsPostBack)
                {
                    List<OrderV2> orders = GetAllOrdersV2();
                    OrdersRepeater.DataSource = orders;
                    OrdersRepeater.DataBind();
                }
            }
            else Response.Redirect("http://localhost:63664/WebForms/LoginForm.aspx");
        }
        private List<OrderV2> GetAllOrdersV2() 
        {
            List<OrderV2> ordersV2 = new List<OrderV2>();
            List<Order> list = GetAllOrders();
            List<OrderStatus> listStatus = new List<OrderStatus>();
            string url = @"https://localhost:7204/api/Order/getAllOrderStatus";

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject jsonObject = JObject.Parse(result);
                    JArray jsonArray = jsonObject["orderStatus"].ToObject<JArray>();
                    listStatus = jsonArray.ToObject<List<OrderStatus>>();
                }
                //De cada ordern, se mira que ID de orderStatus tiene, y se selecciona nombre correspondiente
                foreach (Order itemOrder in list)
                {
                    OrderV2 orderV2 = new OrderV2();
                    orderV2.idOrder = itemOrder.idOrder;
                    orderV2.dateTime = itemOrder.dateTime;
                    orderV2.totalPrice = itemOrder.totalPrice;
                    orderV2.idUser = itemOrder.idUser;
                    orderV2.orderDetails = itemOrder.orderDetails;
                    foreach (OrderStatus itemStatus in listStatus)
                    {
                        if (itemStatus.orderStatusId == itemOrder.orderStatus)
                        {
                            OrderStatus orderStatus = new OrderStatus();
                            orderStatus.orderStatusId = itemStatus.orderStatusId;
                            orderStatus.orderStatusName = itemStatus.orderStatusName;
                            orderV2.orderStatus = orderStatus;
                        }
                    }
                    ordersV2.Add(orderV2);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return ordersV2;

        }
        private List<Order> GetAllOrders() 
        {
            List<Order> list = new List<Order>();
            List<Order> userList = new List<Order>();
            string url = @"https://localhost:7204/api/Order/getAllOrders";

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject jsonObject = JObject.Parse(result);
                    JArray jsonArray = jsonObject["orders"].ToObject<JArray>();
                    list = jsonArray.ToObject<List<Order>>();
                }
                int idUser = int.Parse(Session["session-id"].ToString());
                foreach (Order item in list) 
                {
                    if(item.idUser == idUser) userList.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return userList;
        }
    }
}