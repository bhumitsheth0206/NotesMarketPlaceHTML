using NotesMarketPlace.Areas.Admin.Data;
using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin, superadmin")]
    public class UpdateProfileController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/UpdateProfile
        public ActionResult UpdateProfile(int? ID)
        {
            var emailid = User.Identity.Name.ToString();

            var countries = dobj.AddCountry.ToList();

            updateprofile user = dobj.Users.Where(x => x.EmailID == emailid).Select(x =>
               new updateprofile
               {
                   FirstName = x.FirstName,
                   LastName = x.LastName,
                   EmailID = x.EmailID,
               }
            ).FirstOrDefault();



            if (ID != null)
            {
                user = dobj.UserProfile.Where(x => x.ID == ID).Select(x =>
                  new updateprofile()
                  {
                      ID = x.ID,
                      SecondaryEmail = x.SecondaryEmail,
                      PhoneNumber_CountryCode = x.PhoneNumber_CountryCode,
                      PhoneNumber = x.PhoneNumber,
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
        public ActionResult UpdateProfile(updateprofile model)
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

                if (model.ID == 0)
                {
                    UserProfile profile = new UserProfile();
                    profile.ID = model.ID;
                    profile.UID = obj.ID;
                    profile.SecondaryEmail = model.SecondaryEmail;
                    profile.PhoneNumber_CountryCode = model.PhoneNumber_CountryCode;
                    profile.PhoneNumber = model.PhoneNumber;
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
                    profile.SecondaryEmail = model.SecondaryEmail;
                    profile.PhoneNumber_CountryCode = model.PhoneNumber_CountryCode;
                    profile.PhoneNumber = model.PhoneNumber;
                    profile.ModifiedBy = obj.ID;
                    profile.ModifiedDate = DateTime.Now;
                    obj.FirstName = model.FirstName;
                    obj.LastName = model.LastName;

                    dobj.Entry(profile).State = EntityState.Modified;
                    dobj.Entry(obj).State = EntityState.Modified;
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

            return RedirectToAction("UpdateProfile", "UpdateProfile");

        }
    }
}