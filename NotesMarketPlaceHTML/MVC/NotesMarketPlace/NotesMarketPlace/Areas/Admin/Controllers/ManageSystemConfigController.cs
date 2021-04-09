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
    [Authorize(Roles = "superadmin")]
    public class ManageSystemConfigController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/ManageSystemConfig
        public ActionResult ManageSystemConfig(int? ID)
        {
            if (ID != null)
            {
                var dropdownitems = dobj.ManageSystemConfiguration.Where(x => x.ID == ID).Select(x =>
                 new managesystemconfig()
                 {
                     ID = x.ID,
                     SupportEmail = x.SupportEmail,
                     SupportContactNumber = x.SupportContactNumber,
                     EmailAddress_es = x.EmailAddress_es,
                     FacebookURL = x.FacebookURL,
                     TwitterURL = x.TwitterURL,
                     LinkedInURL = x.LinkedInURL
                 }

                ).FirstOrDefault();
                return View(dropdownitems);
            }
            return View();
        }

        [HttpPost]
        public ActionResult ManageSystemConfig(managesystemconfig model)
        {
            if (ModelState.IsValid)
            {
                var emailid = User.Identity.Name.ToString();
                Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

                if(model.ID == 0)
                {
                    ManageSystemConfiguration manage = new ManageSystemConfiguration();
                    manage.ID = model.ID;
                    manage.SupportEmail = model.SupportEmail;
                    manage.SupportContactNumber = model.SupportContactNumber;
                    manage.EmailAddress_es = model.EmailAddress_es;
                    manage.FacebookURL = model.FacebookURL;
                    manage.TwitterURL = model.TwitterURL;
                    manage.LinkedInURL = model.LinkedInURL;
                    manage.CreatedBy = obj.ID;
                    manage.CreatedDate = DateTime.Now;
                    manage.IsActive = true;

                    
                    if(model.DefaultNoteDisplayImage != null && model.DefaultNoteDisplayImage.ContentLength > 0)
                    {
                        var displayimagename = "NP_" + DateTime.Now.ToString("dd-MM-yy").Replace(':', '-').Replace(' ', '_') + Path.GetExtension(model.DefaultNoteDisplayImage.FileName);
                        var ImageSavePath = Path.Combine(Server.MapPath("/DefaultImg/" + "/") + displayimagename);
                        model.DefaultNoteDisplayImage.SaveAs(ImageSavePath);
                        manage.DefaultNoteDisplayImage = Path.Combine(("/DefaultImg/" + "/"), displayimagename);
                        dobj.SaveChanges();
                    }

                    if (model.DefaultProfilePicture != null && model.DefaultProfilePicture.ContentLength > 0)
                    {
                        var displayimagename = "DP_" + DateTime.Now.ToString("dd-MM-yy").Replace(':', '-').Replace(' ', '_') + Path.GetExtension(model.DefaultProfilePicture.FileName);
                        var ImageSavePath = Path.Combine(Server.MapPath("/DefaultImg/" + "/") + displayimagename);
                        model.DefaultProfilePicture.SaveAs(ImageSavePath);
                        manage.DefaultProfilePicture = Path.Combine(("/DefaultImg/" + "/"), displayimagename);
                        dobj.SaveChanges();
                    }

                    dobj.ManageSystemConfiguration.Add(manage);
                    dobj.SaveChanges();

                }

                else
                {
                    ManageSystemConfiguration manage = dobj.ManageSystemConfiguration.Where(x => x.ID == model.ID).FirstOrDefault();
                    manage.SupportEmail = model.SupportEmail;
                    manage.SupportContactNumber = model.SupportContactNumber;
                    manage.EmailAddress_es = model.EmailAddress_es;
                    manage.FacebookURL = model.FacebookURL;
                    manage.TwitterURL = model.TwitterURL;
                    manage.LinkedInURL = model.LinkedInURL;
                    manage.ModifiedBy = obj.ID;
                    manage.ModifiedDate = DateTime.Now;
                    manage.IsActive = true;

                   

                    if (model.DefaultNoteDisplayImage != null && model.DefaultNoteDisplayImage.ContentLength > 0)
                    {
                        var displayimagename = "NP_" + DateTime.Now.ToString("dd-MM-yy").Replace(':', '-').Replace(' ', '_') + Path.GetExtension(model.DefaultNoteDisplayImage.FileName);
                        var ImageSavePath = Path.Combine(Server.MapPath("/DefaultImg/" + "/") + displayimagename);
                        model.DefaultNoteDisplayImage.SaveAs(ImageSavePath);
                        manage.DefaultNoteDisplayImage = Path.Combine(("/DefaultImg/" + "/"), displayimagename);
                        dobj.SaveChanges();
                    }

                    if (model.DefaultProfilePicture != null && model.DefaultProfilePicture.ContentLength > 0)
                    {
                        var displayimagename = "DP_" + DateTime.Now.ToString("dd-MM-yy").Replace(':', '-').Replace(' ', '_') + Path.GetExtension(model.DefaultProfilePicture.FileName);
                        var ImageSavePath = Path.Combine(Server.MapPath("/DefaultImg/" + "/") + displayimagename);
                        model.DefaultProfilePicture.SaveAs(ImageSavePath);
                        manage.DefaultProfilePicture = Path.Combine(("/DefaultImg/" + "/"), displayimagename);
                        dobj.SaveChanges();
                    }

                    dobj.Entry(manage).State = EntityState.Modified;
                    dobj.SaveChanges();
                }
            }
            return View();
        }
    }
}