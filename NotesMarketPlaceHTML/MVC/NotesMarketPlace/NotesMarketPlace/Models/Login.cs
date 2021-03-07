using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class Login
    {
        [Required(ErrorMessage = "EmailAddress is required")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}