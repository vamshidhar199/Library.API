using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Library.Web.UI.Models
{
    public class MyOrders
    {
        public string StudentId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public System.DateTime OrderDt { get; set; }
        public string TimeSlot { get; set; }
        public int ActiveOrder { get; set; }
    }
}