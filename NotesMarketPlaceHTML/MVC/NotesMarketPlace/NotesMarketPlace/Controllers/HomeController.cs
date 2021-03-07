using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

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

        [Route("SearchNotes")]
        public ActionResult SearchNotes()
        {
            return View();
        }

        [Route("SellNotes")]
        public ActionResult SellNotes()
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
            return View();
        }

        [HttpPost]
        public ActionResult AddContact(ContactUs model)
        {
            if (ModelState.IsValid)
            {
                ContactUs contact = new ContactUs();
                contact.FullName = model.FullName;
                contact.EmailID = model.EmailID;
                contact.Subject = model.Subject;
                contact.Comments = model.Comments;
                contact.CreatedDate = DateTime.Now;

                dobj.ContactUs.Add(contact);
                dobj.SaveChanges();
                SendEmailToAdmin(contact);
                return RedirectToAction("Contactus");
            }
            return View();
        }

        [NonAction]
        public void SendEmailToAdmin(ContactUs obj)
        {
            var fromEmail = new MailAddress("bhumitsheth02@gmail.com"); //Email of Company
            var toEmail = new MailAddress("shethbhumit02@gmail.com"); //Email of admin
            var fromEmailPassword = "// Replace with actual password"; // Replace with actual password
            string subject = obj.FullName + " - Query";

            string body = "Hello," +
                "<br/><br/>" + obj.Comments +
                "<br/><br/>Regards," + obj.FullName;

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
    }
}