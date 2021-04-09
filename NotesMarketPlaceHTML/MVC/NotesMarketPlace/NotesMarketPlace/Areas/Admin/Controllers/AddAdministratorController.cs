using NotesMarketPlace.Areas.Admin.Data;
using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Helper;
using System.Net.Mail;
using System.Net;
using System.Data.Entity;

namespace NotesMarketPlace.Areas.Admin.Controllers
{
    [Authorize(Roles = "superadmin" )]
    public class AddAdministratorController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();


        // GET: Admin/AddAdministrator
        public ActionResult AddAdministrator(int? ID)
        {
            var country = dobj.AddCountry.ToList();

            var dropdownitems = new addadmin()
            {
                Country = country
            };

            if (ID != null)
            {
                dropdownitems = dobj.Users.Where(x => x.ID == ID).Select(x =>
                 new addadmin()
                 {
                     ID = x.ID,
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     EmailID = x.EmailID,
                     PhoneNumber_CountryCode = x.PhoneNumber_CountryCode,
                     PhoneNumber = x.PhoneNumber
                 }

                ).FirstOrDefault();
            }

            dropdownitems.Country = country;

            return View(dropdownitems);
        }

        [HttpPost]
        public ActionResult AddAdministrator(addadmin model)
        {
            if (ModelState.IsValid)
            {
                UserRoles role = dobj.UserRoles.Where(x => x.Name.ToLower() == "admin").FirstOrDefault();
               

                if (model.ID == 0)
                {
                    Users obj = new Users();
                    obj.ID = model.ID;
                    obj.FirstName = model.FirstName;
                    obj.LastName = model.LastName;
                    obj.EmailID = model.EmailID;
                    obj.RoleID = role.ID;
                    obj.IsActive = true;
                    obj.CreatedDate = DateTime.Now;
                    obj.PhoneNumber_CountryCode = model.PhoneNumber_CountryCode;
                    obj.PhoneNumber = model.PhoneNumber;
                    obj.Password = EncryptPassword.EncMD5("admin@123");
                    obj.IsEmailVerified = false;

                    dobj.Users.Add(obj);
                    dobj.SaveChanges();
                    SendVerificationLinkEmail(obj.EmailID);
                    return RedirectToAction("AddAdministrator", "AddAdministrator");
                }

                else
                {
                    Users obj = dobj.Users.Where(x => x.ID == model.ID).FirstOrDefault();
                    obj.FirstName = model.FirstName;
                    obj.LastName = model.LastName;
                    obj.EmailID = model.EmailID;
                    obj.RoleID = role.ID;
                    obj.IsActive = true;
                    obj.ModifiedDate = DateTime.Now;
                    obj.PhoneNumber_CountryCode = model.PhoneNumber_CountryCode;
                    obj.PhoneNumber = model.PhoneNumber;

                    dobj.Entry(obj).State = EntityState.Modified;
                    dobj.SaveChanges();
                }
                    
            }
            return RedirectToAction("AddAdministrator", "AddAdministrator");
        }

        public void SendVerificationLinkEmail(string EmailID)
        {
            var user = dobj.Users.Where(a => a.EmailID == EmailID).FirstOrDefault();
            var verifyUrl = "/EmailVerify?emailid=" + user.EmailID;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            ManageSystemConfiguration manage = dobj.ManageSystemConfiguration.FirstOrDefault();

            var fromEmail = new MailAddress(manage.SupportEmail, "Notes-MarketPlace");
            var toEmail = new MailAddress(EmailID);
            var fromEmailPassword = "*********"; // Replace with actual password
            string subject = "Notes MarketPlace - Email Verification";

            string body = "<br/><br/>We are excited to tell you that you are an Admin of  Notes-MarketPlace and your" +
                " username is registered email address and  password is admin@123. Please click on the below link to "+
                "verify your account" +
                " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        public ActionResult EmailVerify(string emailid)
        {
            var user = dobj.Users.Where(a => a.EmailID == emailid).FirstOrDefault();
            TempData["UserName"] = user.FirstName;
            return View(user);
        }

        [HttpGet]
        public ActionResult VerifyAccount(string emailid)
        {
            var user = dobj.Users.Where(a => a.EmailID == emailid).FirstOrDefault();
            if (user != null)
            {
                user.IsEmailVerified = true;
                user.IsActive = true;
                dobj.SaveChanges();
            }
            return RedirectToAction("Login", "Login");
        }



    }
}