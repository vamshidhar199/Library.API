using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Web.UI.Models
{
    public class BookDetail
    {
        public string BookaName { get; set; }
        public string BookSubTitle { get; set; }
        public string BookAuthor { get; set; }
        public string BookImg { get; set; }
        public string BookDesc { get; set; }
        public int BookQty { get; set; }
        public string BookCat { get; set; }
        public string Dept { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public System.DateTime ModifiedDt { get; set; }
    }
}