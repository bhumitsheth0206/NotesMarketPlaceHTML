using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Areas.Admin.Data
{
    public class managesystemconfig
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string SupportEmail { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string SupportContactNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string EmailAddress_es { get; set; }

        public string FacebookURL { get; set; }

        public string TwitterURL { get; set; }

        public string LinkedInURL { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public HttpPostedFileBase DefaultNoteDisplayImage { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public HttpPostedFileBase DefaultProfilePicture { get; set; }
    }
}