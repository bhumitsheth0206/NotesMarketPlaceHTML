using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class changepassword
    {
        [Required(ErrorMessage = "Password is required")]
        public string oldpassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string newpassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Compare("newpassword", ErrorMessage = "Password doesn't gets matched")]
        public string confirmpassword { get; set; }
    }
}