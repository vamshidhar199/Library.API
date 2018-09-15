using Library.Web.UI.Models;
using Library.Web.UI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Library.Web.UI.Controllers
{
    public class BookDisplayController : Controller
    {
        ConsumeAPI api = new ConsumeAPI();
        IEnumerable<BookDetail> books;
        List<BookDetail> addedToCart = new List<BookDetail>();
        
        // GET: BookDisplay
        public async System.Threading.Tasks.Task<ActionResult> Index(string id)
        {
            ViewBag.Dept = id;
            if(id != null)
            id = id.ToUpper();
            
            books = await api.GetBooks();
            Session["Books"] = books;
            books = books.Where(d => d.Dept == id).Select(d=>d).ToList<BookDetail>();
            ViewBag.status = TempData["status"];
            if (ViewBag.status==true)
                ViewBag.Message = TempData["Message"];  
           
                    
            return View(books);
        }
        public ActionResult Single(string id,string author)
        {
            BookDetail book = new BookDetail();
            books = (IEnumerable<BookDetail>)Session["Books"];
            book = books.Where(b => b.BookaName.Trim() == id.Trim() && b.BookAuthor.Trim()==author.Trim()).Select(b => b).FirstOrDefault();
            Session["currentBook"] = book;
            return View(book);
        }


        [HttpPost]
        public ActionResult Single()
        {
           // List<BookDetail> addedtocart = new List<BookDetail>();
            //addedtocart = (List<BookDetail>)Session["cart"];
            BookDetail book;
            book = (BookDetail)Session["currentBook"];

            if (Session["cart"] == null)
            {
                List<BookDetail> addedtocart = new List<BookDetail>();
                addedtocart.Add((BookDetail)Session["currentBook"]);
                Session["cart"] = addedtocart;
                Session["cartcount"] = addedtocart.Count();
            }
            else
            {
                List<BookDetail> addedtocart = new List<BookDetail>();
                addedtocart = (List<BookDetail>)Session["cart"];
                if (addedtocart.Count() >= 3)
                {
                    TempData["Message"] = "You can take only 3 books at a time.";
                    TempData["Status"] = true;
                }
                else
                {
                    if (addedtocart.Exists(b => b.BookaName == book.BookaName && b.BookAuthor == book.BookAuthor))
                    {
                        TempData["Message"] = "This book is already added to cart.";
                        TempData["Status"] = true;
                    }
                    else
                    {
                        addedtocart.Add(book);
                        TempData["Status"] = false;
                    }
                    Session["cart"] = addedtocart;
                    Session["cartcount"] = addedtocart.Count();
                }
            }
            return Redirect("/BookDisplay/Index/"+book.Dept);
            //return Redirect("/BookDisplay/Single?id="+book.BookaName+"&author="+book.BookAuthor);
          
        }
    }
}