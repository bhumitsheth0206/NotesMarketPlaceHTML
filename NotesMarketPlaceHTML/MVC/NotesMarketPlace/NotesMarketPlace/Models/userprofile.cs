using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class userprofile
    {
        public int ID { get; set; }

        public string SecondaryEmail { get; set; }

        public Nullable<System.DateTime> DateOfBirth { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string PhoneNumber_CountryCode { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string PhoneNumber { get; set; }

        public HttpPostedFileBase ProfilePicture { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string AddressLine_1 { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string AddressLine_2 { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Country { get; set; }

        public string University { get; set; }

        public string College { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^([A-Za-z]+[^\s])$", ErrorMessage = "FirstName is invalid")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^([A-Za-z]+[^\s])$", ErrorMessage = "LastName is invalid")]
        public string LastName { get; set; }

        
        public string EmailID { get; set; }

        public List<AddCountry> Countries { get; set; }
    }
}