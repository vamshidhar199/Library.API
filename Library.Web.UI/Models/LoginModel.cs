using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Web.UI.Models
{
    public class LoginModel
    {        
        public string StudentId { get; set; }
        public string Password { get; set; }
    }
}