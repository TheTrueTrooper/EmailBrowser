using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EmailBrowser.Models
{
    public class Login
    {
        [DisplayName("User Name"), Required(ErrorMessage = "This field is required")]
        public string UserName { get; set; }
        [DisplayName("Password"), PasswordPropertyText, Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
    }
}