using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Net.Mail;
using System.Net;

namespace NotesMarketPlace.Controllers
{
    [Authorize]
    public class BuyerRequestController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        [Route("BuyerRequest")]
        // GET: BuyerRequest
        public ActionResult BuyerRequest(string search,int? page, string sortBy)
        {
            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortNoteTitle = sortBy == "Title" ? "Title Desc" : "Title";
            ViewBag.SortCategory = sortBy == "Category" ? "Category Desc" : "Category";

            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();
            NoteDetails noteID = dobj.NoteDetails.Where(x => x.UID == obj.ID).FirstOrDefault();

            var filterTitle = dobj.Downloads.Where(x => x.NoteTitle.Contains(search) || search == null);
            var filterCategory = dobj.Downloads.Where(x => x.Category.Contains(search));

            var filtereddata = filterTitle.Union(filterCategory);

            var table__entry = filtereddata.Where(x => x.Seller == noteID.UID ).ToList().AsQueryable();

            switch (sortBy)
            {
                case "Date Desc":
                    table__entry = table__entry.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Title":
                    table__entry = table__entry.OrderBy(x => x.NoteTitle);
                    break;
                case "Title Desc":
                    table__entry = table__entry.OrderByDescending(x => x.NoteTitle);
                    break;
                case "Category":
                    table__entry = table__entry.OrderBy(x => x.Category);
                    break;
                case "Category Desc":
                    table__entry = table__entry.OrderByDescending(x => x.Category);
                    break;
                default:
                    table__entry = table__entry.OrderBy(x => x.CreatedDate);
                    break;
            }
            return View(table__entry.ToPagedList(page ?? 1, 10));
        }

        [HttpGet]
        public ActionResult Allow_Download(int? ID)
        {
            Downloads downloads = dobj.Downloads.Where(x => x.ID == ID).FirstOrDefault();
            downloads.IsSellerHasAllowedDownload = true;

            var NoteID = downloads.NoteID;

            NoteDetails ndetail = dobj.NoteDetails.Where(x => x.ID == NoteID).FirstOrDefault();
            

            SellerNotesAttachments sellernotes = dobj.SellerNotesAttachments.Where(x => x.NoteID == NoteID).FirstOrDefault();
            downloads.AttachmentPath = sellernotes.FilePath;
            dobj.SaveChanges();

            var fromEmail = new MailAddress(""); //Email of Company
            var toEmail = new MailAddress(downloads.Users1.EmailID); //Buyer EmailAddress
            var fromEmailPassword = "********"; // Replace with actual password
            string subject = downloads.Users.FirstName + " - Allows you to download a note";

            string body = "Hello," + downloads.Users1.FirstName +
                "<br/><br/>We would like to inform you that,"+ downloads.Users.FirstName + " Allows you to download a note." +
                " Please login and see My Download tabs to download particular note." +
                "<br/><br/>Regards," +
                "<br/>Notes Marketplace";

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

            return RedirectToAction("Dashboard", "Dashboard");
        }


    }
}