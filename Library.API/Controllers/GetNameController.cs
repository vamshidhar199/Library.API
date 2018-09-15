using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.API.Controllers
{
    public class GetNameController : ApiController
    {
        DBLibEntities db = new DBLibEntities();
        public string Get(string sid, string password)
        {
            string name = db.Validate_User(sid,password).FirstOrDefault();
            return name;
        }
    }
}
