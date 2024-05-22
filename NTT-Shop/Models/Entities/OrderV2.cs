using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTT_Shop.Models.Entities
{
    public class OrderV2
    {
        public int idOrder { get; set; }
        public DateTime dateTime { get; set; }
        public OrderStatus orderStatus { get; set; }
        public decimal totalPrice { get; set; }
        public int idUser { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
        public OrderV2() { }
        public OrderV2(int idOrder, DateTime dateTime, OrderStatus orderStatus, decimal totalPrice, int idUser, List<OrderDetail> orderDetails)
        {
            this.idOrder = idOrder;
            this.dateTime = dateTime;
            this.orderStatus = orderStatus;
            this.totalPrice = totalPrice;
            this.idUser = idUser;
            this.orderDetails = orderDetails;
        }
    }
}