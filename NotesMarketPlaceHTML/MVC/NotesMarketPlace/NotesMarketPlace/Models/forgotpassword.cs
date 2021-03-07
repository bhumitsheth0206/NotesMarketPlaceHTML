using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class forgotpassword
    {
        [Required(ErrorMessage = "EmailAddress is required")]
        [EmailAddress()]
        public string EmailID { get; set; }
    }
}