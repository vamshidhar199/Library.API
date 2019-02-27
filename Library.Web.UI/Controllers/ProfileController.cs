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
    public class ProfileController : Controller
    {
        string Baseurl = "http://localhost:56181/";
        // GET: Profil

        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            string studID = Session["UserId"].ToString();
            StudentProfile profile = new StudentProfile();

            ViewBag.id = Session["UserId"];
            ViewBag.name = Session["Username"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/GetStudentDetails?sid=" + studID);

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api  
                    var res = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    profile = JsonConvert.DeserializeObject<StudentProfile>(res);
                    
                    if (profile != null)
                    {
                        ViewBag.error = false;
                        List<MyOrders> myOrders = await GetOrders(studID);
                        if (myOrders != null)
                        {
                            profile.ActiveOrders = myOrders.Where(o => o.ActiveOrder == 1).Select(o => o).ToList();
                            profile.OrderHistory=myOrders.Where(o => o.ActiveOrder == 0).Select(o => o).ToList();
                        }
                        else
                        {
                            profile.ActiveOrders = null;
                            profile.OrderHistory = null;
                        }
                        return View(profile);
                    }
                    else
                    {
                        ViewBag.error = true;
                        ViewBag.Message = "No details found";
                        return View();
                    }
                }
                return View();
            }

        }
        public async System.Threading.Tasks.Task<List<MyOrders>> GetOrders(string studID)
        {
            List<MyOrders> myOrders = new List<MyOrders>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/GetOrders?sid=" + studID);
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api  
                    var res = Res.Content.ReadAsStringAsync().Result;
                    myOrders = JsonConvert.DeserializeObject<List<MyOrders>>(res);
                    if (myOrders != null)
                    {
                        return myOrders;
                    }
                    else
                        return null;
                }
                else return null;
            }
        }
    }
}