using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class signup
    {

        public int ID { get; set; }

        public int RoleID { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [RegularExpression(@"^([A-Za-z]+[^\s])$", ErrorMessage = "FirstName is invalid")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [RegularExpression(@"^([A-Za-z]+[^\s])$", ErrorMessage = "LastName is invalid")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "EmailAddress is required")]
        [EmailAddress()]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!% *#?&])[A-Za-z\d@$!%#?&]{6,24}$", ErrorMessage = "Password is invalid")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Compare("Password", ErrorMessage = "Password doesn't gets matched")]
        public string ConfirmPassword { get; set; }

       
    }
}