using NotesMarketPlace.Context;
using NotesMarketPlace.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Controllers
{
    [Authorize(Roles = "member")]
    public class UserProfileController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        [HttpGet]
        [Route("UserProfile")]
        // GET: UserProfile
        public ActionResult UserProfile(int? ID)
        {

            var emailid = User.Identity.Name.ToString();

            var countries = dobj.AddCountry.ToList();

            userprofile user = dobj.Users.Where(x => x.EmailID == emailid).Select(x => 
               new userprofile
               {
                   FirstName = x.FirstName,
                   LastName = x.LastName,
                   EmailID = x.EmailID,
               }
            ).FirstOrDefault();



            if (ID != null)
            {
                user = dobj.UserProfile.Where(x => x.ID == ID).Select(x =>
                  new userprofile()
                  {
                      ID = x.ID,
                      DateOfBirth = x.DateOfBirth,
                      Gender = x.Gender,
                      PhoneNumber_CountryCode = x.PhoneNumber_CountryCode,
                      PhoneNumber = x.PhoneNumber,
                      AddressLine_1 = x.AddressLine_1,
                      AddressLine_2 = x.AddressLine_2,
                      City = x.City,
                      State = x.State,
                      ZipCode = x.ZipCode,
                      Country = x.Country,
                      University = x.University,
                      College = x.College,
                      FirstName = user.FirstName,
                      LastName = user.LastName,
                      EmailID = user.EmailID
                  }
                ).FirstOrDefault();
            }

            user.Countries = countries;

            return View(user);
        }

        [HttpPost]
        [Route("UserProfile")]
        public ActionResult UserProfile(userprofile model)
        {
            if (ModelState.IsValid)
            {
                var emailid = User.Identity.Name.ToString();
                Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();
                string path = Path.Combine(Server.MapPath("~/Members"), obj.ID.ToString());

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (model.ProfilePicture != null && model.ProfilePicture.ContentLength>10485760)
                {
                    TempData["Profile_Picture"] = "Profile-Picture upto 10MB is only allowed";
                    return RedirectToAction("UserProfile", "UserProfile");
                }

                if (model.ID == 0)
                {
                    UserProfile profile = new UserProfile();
                    profile.UID = obj.ID;
                    profile.DateOfBirth = model.DateOfBirth;
                    profile.Gender = model.Gender;
                    profile.PhoneNumber_CountryCode = model.PhoneNumber_CountryCode;
                    profile.PhoneNumber = model.PhoneNumber;
                    profile.AddressLine_1 = model.AddressLine_1;
                    profile.AddressLine_2 = model.AddressLine_2;
                    profile.City = model.City;
                    profile.State = model.State;
                    profile.ZipCode = model.ZipCode;
                    profile.Country = model.Country;
                    profile.University = model.University;
                    profile.College = model.College;
                    profile.SubmittedBy = obj.ID;
                    profile.SubmittedDate = DateTime.Now;

                    dobj.UserProfile.Add(profile);
                    dobj.SaveChanges();

                    if (model.ProfilePicture != null && model.ProfilePicture.ContentLength > 0)
                    {
                        var displayimagename = "DP_" + DateTime.Now.ToString("dd-MM-yy").Replace(':', '-').Replace(' ', '_') + Path.GetExtension(model.ProfilePicture.FileName);
                        var ImageSavePath = Path.Combine(Server.MapPath("/Members/" + obj.ID + "/") + displayimagename);
                        model.ProfilePicture.SaveAs(ImageSavePath);
                        profile.ProfilePicture = Path.Combine(("/Members/" + obj.ID + "/"), displayimagename);
                        dobj.SaveChanges();
                    }
                }
                else
                {
                    UserProfile profile = dobj.UserProfile.Where(x => x.ID == model.ID).FirstOrDefault();
                    profile.UID = obj.ID;
                    profile.DateOfBirth = model.DateOfBirth;
                    profile.Gender = model.Gender;
                    profile.PhoneNumber_CountryCode = model.PhoneNumber_CountryCode;
                    profile.PhoneNumber = model.PhoneNumber;
                    profile.AddressLine_1 = model.AddressLine_1;
                    profile.AddressLine_2 = model.AddressLine_2;
                    profile.City = model.City;
                    profile.State = model.State;
                    profile.ZipCode = model.ZipCode;
                    profile.Country = model.Country;
                    profile.University = model.University;
                    profile.College = model.College;
                    profile.ModifiedBy = obj.ID;
                    profile.ModifiedDate = DateTime.Now;

                    dobj.Entry(profile).State = EntityState.Modified;
                    dobj.SaveChanges();

                    if (model.ProfilePicture != null && model.ProfilePicture.ContentLength > 0)
                    {
                        var displayimagename = "DP_" + DateTime.Now.ToString("dd-MM-yy").Replace(':', '-').Replace(' ', '_') + Path.GetExtension(model.ProfilePicture.FileName);
                        var ImageSavePath = Path.Combine(Server.MapPath("/Members/" + obj.ID + "/") + displayimagename);
                        model.ProfilePicture.SaveAs(ImageSavePath);
                        profile.ProfilePicture = Path.Combine(("/Members/" + obj.ID + "/"), displayimagename);
                        dobj.SaveChanges();
                    }
                }
                

                
            }
               
            return RedirectToAction("Home","Home");
        }

    }
}