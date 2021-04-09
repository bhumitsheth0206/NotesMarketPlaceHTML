using NotesMarketPlace.Areas.Admin.Data;
using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin, superadmin")]
    public class AddCountryController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/AddCountry
        public ActionResult AddCountry(int? ID)
        {
            if (ID != null)
            {
                var dropdownitems = dobj.AddCountry.Where(x => x.ID == ID).Select(x =>
                 new addcountry()
                 {
                     ID = x.ID,
                     CountryName = x.CountryName,
                     CountryCode = x.CountryCode
                 }

                ).FirstOrDefault();
                return View(dropdownitems);
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddCountry(addcountry model)
        {
            if(ModelState.IsValid)
            {
                var emailid = User.Identity.Name.ToString();
                Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();
               
                if (model.ID == 0)
                {
                    AddCountry addCountry = new AddCountry();
                    addCountry.ID = model.ID;
                    addCountry.CountryName = model.CountryName;
                    addCountry.CountryCode = model.CountryCode;
                    addCountry.CreatedDate = DateTime.Now;
                    addCountry.CreatedBy = obj.ID;
                    addCountry.IsActive = true;

                    dobj.AddCountry.Add(addCountry);
                    dobj.SaveChanges();
                }

                else
                {
                    AddCountry addCountry = dobj.AddCountry.Where(x => x.ID == model.ID).FirstOrDefault();
                    addCountry.CountryName = model.CountryName;
                    addCountry.CountryCode = model.CountryCode;
                    addCountry.ModifiedDate = DateTime.Now;
                    addCountry.ModifiedBy = obj.ID;
                    addCountry.IsActive = true;

                    dobj.Entry(addCountry).State = EntityState.Modified;
                    dobj.SaveChanges();
                }
                    
            }

            return View();
        }
    }
}