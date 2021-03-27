using NotesMarketPlace.Context;
using NotesMarketPlace.Helper;
using NotesMarketPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Controllers
{
    [Authorize]
    public class ChangePasswordController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        [Route("ChangePassword")]
        // GET: ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ChangePassword")]

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
                    return RedirectToAction("ChangePassword","ChangePassword");
                }

                var newpasswd = EncryptPassword.EncMD5(model.newpassword);
                obj.Password = newpasswd;

                dobj.SaveChanges();

            }

            return RedirectToAction("Login", "Login");
        }
    }
}