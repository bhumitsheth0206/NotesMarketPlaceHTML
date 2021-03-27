using NotesMarketPlace.Context;
using NotesMarketPlace.Models;
using NotesMarketPlace.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Notes_MarketPlace.Controllers
{
    public class LoginController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        [Route("Login")]
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LoginUser(Login model)
        {
            if(ModelState.IsValid)
            {
                var password = EncryptPassword.EncMD5(model.Password);

                bool isValid = dobj.Users.Any(x => x.EmailID == model.EmailID && x.Password == password);

                if (isValid)
                {
                    Users obj = dobj.Users.Where(x => x.EmailID == model.EmailID && x.Password == password).FirstOrDefault();

                    if(obj.IsEmailVerified)
                    {
                        FormsAuthentication.SetAuthCookie(model.EmailID , model.RememberMe);
                        
                        if(dobj.UserProfile.Any(x => x.UID == obj.ID))
                        {
                            return RedirectToAction("NoteSearch", "NoteSearch");
                        }

                        return RedirectToAction("UserProfile", "UserProfile");
                    }
                    TempData["ErrorMsg"] = "Email Address is not  yet verified";
                    return RedirectToAction("Login", "Login");
                }
                TempData["ErrorMsg"] = "Invalid username and password";
                return RedirectToAction("Login", "Login");
            }
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Logout()
        { 
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Login");
        }

    }
}