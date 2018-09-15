using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Web.UI.Models
{
    public class StudentProfile
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string Department { get; set; }
        public string Gender { get; set; }
        public int YOJ { get; set; }
        public List<MyOrders> ActiveOrders { get; set; }
        public List<MyOrders> OrderHistory { get; set; }

    }

   

}