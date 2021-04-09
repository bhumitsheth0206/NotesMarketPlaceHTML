using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Areas.Admin.Data
{
    public class addadmin
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [RegularExpression(@"^([A-Za-z]+[^\s])$", ErrorMessage = "FirstName is invalid")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [RegularExpression(@"^([A-Za-z]+[^\s])$", ErrorMessage = "LastName is invalid")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "EmailAddress is required")]
        [EmailAddress()]
        public string EmailID { get; set; }


        public string PhoneNumber { get; set; }

        public string PhoneNumber_CountryCode { get; set; }

        public List<AddCountry> Country { get; set; }
    }
}