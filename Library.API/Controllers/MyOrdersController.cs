using Library.API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace Library.API.Controllers
{
    public class MyOrdersController : ApiController
    {
        DBLibEntities db = new DBLibEntities();

        [HttpPut]
        public void PlaceOrder([FromBody]List<StudentOrder> myOrders)
        {
            //var orders= Content.ReadAsAsync<Exception>().Result;
            //var orders = Request.Content.ReadAsAsync<List<MyOrder>>().Result;
            //List<StudentOrder> mydata = JsonConvert.DeserializeObject<List<StudentOrder>>(myOrders);
            try
            {
                StudentOrder order = new StudentOrder();
                List<StudentOrder> orderList = new List<StudentOrder>();
                foreach (var item in myOrders)
                {
                    order.StudentId = item.StudentId;
                    order.BookName = item.BookName;
                    order.BookAuthor = item.BookAuthor;
                    order.OrderDt = item.OrderDt;
                    order.TimeSlot = item.TimeSlot;
                    order.ActiveOrder = item.ActiveOrder;
                    orderList.Add(order);
                    db.StudentOrders.Add(order);
                    db.SaveChanges();
                }
                
                //return true;
            }
            catch (Exception ex)
            {
                //return false;
            }
        }

    }
}
