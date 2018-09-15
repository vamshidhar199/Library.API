using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.API.Controllers
{
    public class GetOrdersController : ApiController
    {
        DBLibEntities db = new DBLibEntities();

        public IEnumerable<StudentOrder> Get(string sid)
        {
            return db.StudentOrders;
        }
    }
}
