using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class contactus
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }

        public string EmailID { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Comments are required")]
        public string Comments { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

    }
}