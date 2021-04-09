using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Areas.Admin.Data
{
    public class updateprofile
    {
        public int ID { get; set; }

        public int UID { get; set; }

        public string SecondaryEmail { get; set; }

        public string PhoneNumber_CountryCode { get; set; }

        public string PhoneNumber { get; set; }

        public HttpPostedFileBase ProfilePicture { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailID { get; set; }

        public List<AddCountry> Countries { get; set; }

    }
}