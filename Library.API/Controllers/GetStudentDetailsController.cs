using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library.API.Controllers
{
    public class GetStudentDetailsController : ApiController
    {
        DBLibEntities db = new DBLibEntities();

        public StudentDetail Get(string sid)
        {
            return db.StudentDetails.Where(s => s.StudentId == sid).Select(s => s).FirstOrDefault();
        }
    }
}
