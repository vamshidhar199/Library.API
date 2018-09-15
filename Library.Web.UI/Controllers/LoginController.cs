using Library.Web.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Library.Web.UI.Controllers
{
    
    public class LoginController : Controller
    {
        public string StudentName = "";
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public async System.Threading.Tasks.Task<ActionResult> Index(LoginModel user)
        {
            string Baseurl = "http://localhost:56181/";
            
            
            if (user.StudentId!=null || user.Password!=null)
            {
                string studentId = user.StudentId.ToString();
                string password = user.Password.ToString();
                
           
           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                    HttpResponseMessage Res = await client.GetAsync("api/GetName?sid="+ studentId + "&password="+password);
                    Session["UserId"] = StudentName;
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api  
                    var res = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    StudentName = JsonConvert.DeserializeObject<string>(res);
                    if (StudentName != "-1")
                    {
                            Session["UserId"] = studentId;
                            Session["Username"] = StudentName;
                            ViewBag.invalidUser = false;
                    }
                    else
                    {
                        ViewBag.invalidUser = true;
                        ViewBag.Message = "Login credentials are incorrect!";
                        return View();
                    }
                }
               
            }
            }
            else
            {
                ViewBag.invalidUser = true;
                ViewBag.Message = "Username / password are required!";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return Redirect("/Login/Index/" );
        }
    }
}