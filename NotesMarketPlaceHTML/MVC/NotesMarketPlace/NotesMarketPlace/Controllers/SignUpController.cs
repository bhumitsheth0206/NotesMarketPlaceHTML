using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Helper;

namespace Notes_MarketPlace.Controllers
{
    public class SignUpController : Controller
    {

        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        [Route("SignUp")]
        // GET: SignUp

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        
        
        [HttpPost]
        public ActionResult AddUser(Users model)
        {
            if (ModelState.IsValid)
            {
                UserRoles role = dobj.UserRoles.Where(x => x.Name.ToLower() == "member").FirstOrDefault();
                Users obj = new Users();
           
                obj.FirstName = model.FirstName;
                obj.LastName = model.LastName;
                obj.EmailID = model.EmailID;
                obj.RoleID = role.ID;
                obj.Password = EncryptPassword.EncMD5(model.Password);
                obj.IsActive = true;
                obj.CreatedDate = DateTime.Now;
                obj.IsEmailVerified = false;

                dobj.Users.Add(obj);
                dobj.SaveChanges();
                TempData["Success"] = "Your account has been successfully created";
                SendVerificationLinkEmail(obj.EmailID);
                return RedirectToAction("SignUp");
            }
            return View();
        }

        public void SendVerificationLinkEmail(string EmailID)
        {
            var user = dobj.Users.Where(a => a.EmailID == EmailID).FirstOrDefault();
            var verifyUrl = "/EmailVerify?emailid=" + user.EmailID ;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            ManageSystemConfiguration manage = dobj.ManageSystemConfiguration.FirstOrDefault();

            var fromEmail = new MailAddress(manage.SupportEmail, "Notes-MarketPlace");
            var toEmail = new MailAddress(EmailID);
            var fromEmailPassword = "******"; // Replace with actual password
            string subject = "Notes MarketPlace - Email Verification";

            string body = "<br/><br/>We are excited to tell you that your Notes-MarketPlace account is" +
                " successfully created. Please click on the below link to verify your account" +
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

        [Route("EmailVerify")]
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