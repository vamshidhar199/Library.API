using Library.Web.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Library.Web.UI.Controllers
{
    public class CartController : Controller
    {
        string Baseurl = "http://localhost:56181/";
        // GET: Cart
        public ActionResult Index()
     {

            if (Convert.ToInt16(Session["cartcount"]) == 0)
            {

                ViewBag.Status = true;
                ViewBag.Message = "cart empty";
                return View();
            }
            else
            {
                IEnumerable<BookDetail> booksInCart = (IEnumerable<BookDetail>)Session["cart"];
                return View(booksInCart);
            }

        }

        public ActionResult Delete(string id, string author)
        {

            List<BookDetail> booksInCart = (List<BookDetail>)Session["cart"];
            BookDetail removeBook = new BookDetail();
            removeBook = booksInCart.Where(b => b.BookaName.Trim() == id.Trim() && b.BookAuthor.Trim() == author.Trim()).Select(b => b).FirstOrDefault();
            booksInCart.Remove(removeBook);
            Session["cart"] = booksInCart;
            Session["cartcount"] = booksInCart.Count();
            return Redirect("/Cart/Index/");

        }

        public async System.Threading.Tasks.Task<ActionResult> PlaceOrder()
        {

            List<BookDetail> placedOrders = (List<BookDetail>)Session["cart"];
            List<MyOrders> myOrders = new List<MyOrders>();
            MyOrders order = new MyOrders();
            foreach (var item in placedOrders)
            {
                order.StudentId = Session["UserId"].ToString();
                order.BookName = item.BookaName;
                order.BookAuthor = item.BookAuthor;
                order.OrderDt = DateTime.Now;
                order.TimeSlot = Request.Form["ddlTimeSlot"].ToString();
                order.ActiveOrder = 1;
                myOrders.Add(order);

            }
            var data = JsonConvert.SerializeObject(myOrders);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PutAsync("api/MyOrders/PlaceOrder", httpContent);
                if (Res.IsSuccessStatusCode)
                {
                    var res = Res.Content.ReadAsStringAsync().Result;
                    ViewBag.OrderStatus = true;
                    Session["cart"] = null;
                    Session["cartcount"] = null;
                    return Redirect("/Success/Index");
                  
                }
                else
                    ViewBag.OrderStatus = false;

                //string timeSlot = Request.Form["ddlTimeSlot"].ToString();
                return Redirect("/Cart/Index/");

            }

        }
    }
}
