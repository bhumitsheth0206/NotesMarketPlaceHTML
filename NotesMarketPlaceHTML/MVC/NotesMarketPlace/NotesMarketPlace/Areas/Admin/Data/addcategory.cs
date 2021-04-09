using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Areas.Admin.Data
{
    public class addcategory
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }
    }
}