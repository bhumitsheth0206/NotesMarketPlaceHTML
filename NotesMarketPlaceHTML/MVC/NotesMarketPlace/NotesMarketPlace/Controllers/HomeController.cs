using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace Notes_MarketPlace.Controllers
{
    public class HomeController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        
        // GET: Home
        public ActionResult Home()
        {
            return View();
        }

        [Route("FAQ")]
        public ActionResult FAQ()
        {
            return View();
        }

        [Route("Contactus")]
        [HttpGet]
        public ActionResult Contactus()
        {
            var emailid = User.Identity.Name.ToString();
            
            if(User.Identity.IsAuthenticated)
            {
                contactus user = dobj.Users.Where(x => x.EmailID == emailid).Select(x =>
               new contactus
               {
                   FullName = x.FirstName + " " + x.LastName,
                   EmailID = x.EmailID
               }
                ).FirstOrDefault();

                return View(user);

            }

            return View();
        }

        [Route("Contactus")]
        [HttpPost]
        public ActionResult Contactus(contactus model)
        {
            if (ModelState.IsValid)
            {
                ManageSystemConfiguration manage = dobj.ManageSystemConfiguration.FirstOrDefault();

                var fromEmail = new MailAddress(manage.SupportEmail, "Notes-MarketPlace");
                var toEmail = new MailAddress(manage.EmailAddress_es); //Email of admin
                var fromEmailPassword = "******"; // Replace with actual password
                string subject = model.FullName + " - Query";

                string body = "Hello," +
                    "<br/><br/>" + model.Comments +
                    "<br/><br/>Regards,<br/>" + model.FullName;

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
            return View();
        }

        
    }
}