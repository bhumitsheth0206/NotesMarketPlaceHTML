using NotesMarketPlace.Areas.Admin.Data;
using NotesMarketPlace.Context;
using NotesMarketPlace.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Areas.Admin.Controllers
{
    public class ChangePasswordController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(changepassword model)
        {
            if (ModelState.IsValid)
            {
                var emailid = User.Identity.Name.ToString();
                Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();
                var password = EncryptPassword.EncMD5(model.oldpassword);

                if (obj.Password != password)
                {
                    ModelState.AddModelError("oldpassword", "Password doesn't match with original password");
                    return RedirectToAction("ChangePassword", "ChangePassword");
                }

                var newpasswd = EncryptPassword.EncMD5(model.newpassword);
                obj.Password = newpasswd;

                dobj.SaveChanges();

            }

            return RedirectToAction("Login", "Login");
        }
    }
}