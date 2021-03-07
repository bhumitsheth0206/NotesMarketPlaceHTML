using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class ContactUs
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "FullName is required")]
        [RegularExpression(@"^([A-Za-z]+)$", ErrorMessage = "FullName is invalid")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "EmailAddress is required")]
        [EmailAddress(ErrorMessage = "EmailAddress is invalid")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Comments are required")]
        public string Comments { get; set; }
        
    }
}