using NotesMarketPlace.Context;
using NotesMarketPlace.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Controllers
{
    [Authorize(Roles = "member")]
    public class MyDownloadsController : Controller
    {

        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        [Route("MyDownloads")]
        // GET: MyDownloads
        public ActionResult MyDownloads(string search, int? page, string sortBy)
        {
            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortNoteTitle = sortBy == "Title" ? "Title Desc" : "Title";
            ViewBag.SortCategory = sortBy == "Category" ? "Category Desc" : "Category";

            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            var filterTitle = dobj.Downloads.Where(x => x.NoteTitle.Contains(search) || search == null);
            var filterCategory = dobj.Downloads.Where(x => x.Category.Contains(search));

            var filtereddata = filterTitle.Union(filterCategory);

            var table__entry = filtereddata.Where(x => x.Downloader == obj.ID).ToList().AsQueryable();

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

        public ActionResult DownloadNotes(int? id)
        {
            Downloads downloads = dobj.Downloads.Where(x => x.ID == id).FirstOrDefault();
            var NoteID = downloads.NoteID;
            NoteDetails ndetail = dobj.NoteDetails.Where(x => x.ID == NoteID).FirstOrDefault();
            SellerNotesAttachments seller_notes = dobj.SellerNotesAttachments.Where(x => x.NoteID == NoteID).FirstOrDefault();

            if(downloads.IsSellerHasAllowedDownload == true)
            {
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename =" + ndetail.NoteTitle + ".pdf");
                Response.TransmitFile(seller_notes.FilePath);
                Response.End();
                downloads.IsAttachmentDownloaded = true;
                downloads.AttachmentDownloadedDate = DateTime.Now;
                dobj.SaveChanges();
            }

            return RedirectToAction("MyDownloads", "MyDownloads");
        }

        
        [HttpPost]
        public ActionResult Rate_Notes(int ID, string Comment, int Rate)
        {

            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();
            Downloads downloads = dobj.Downloads.Where(x => x.NoteID == ID && x.Downloader == obj.ID).FirstOrDefault();

            NoteReview note_review = new NoteReview();
            note_review.NoteID = ID;
            note_review.AgainstDownloadID = downloads.ID;
            note_review.ReviewedByID = obj.ID;
            note_review.Rate = Rate;
            note_review.Comment = Comment;
            note_review.CreatedDate = DateTime.Now;
            note_review.IsActive = true;

            dobj.NoteReview.Add(note_review);
            dobj.SaveChanges();

            return RedirectToAction("MyDownloads","MyDownloads");
        }

        [HttpPost]
        public ActionResult Mark_Inappropriate(int ID, string Remark)
        {
            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();
            Downloads downloads = dobj.Downloads.Where(x => x.NoteID == ID && x.Downloader == obj.ID).FirstOrDefault();
            ViewBag.note_title = downloads.NoteTitle;

            SpamReports spam_reports = new SpamReports();
            spam_reports.NoteID = ID;
            spam_reports.AgainstDownloadID = downloads.ID;
            spam_reports.ReportedBy = obj.ID;
            spam_reports.Remark = Remark;
            spam_reports.CreatedDate = DateTime.Now;

            dobj.SpamReports.Add(spam_reports);
            dobj.SaveChanges();

            ManageSystemConfiguration manage = dobj.ManageSystemConfiguration.FirstOrDefault();

            var fromEmail = new MailAddress(manage.SupportEmail, "Notes-MarketPlace"); //Email of Company
            var toEmail = new MailAddress(manage.EmailAddress_es);//Email of Admin
            var fromEmailPassword = "*********"; // Replace with actual password
            string subject = obj.FirstName + " Reported an issue for " + downloads.NoteTitle;

            string body = "Hello, Admins" +
                "<br/><br/>We want to inform you that, "+ obj.FirstName + "  Reported an issue for "+downloads.Users.FirstName + "’s Note with " +
                "title " + downloads.NoteTitle +". Please look at the notes and take required actions." +
               
                "<br/><br/><br/>Regards," +
                "<br/>Notes Marketplace.";

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


            return RedirectToAction("MyDownloads", "MyDownloads");
        }

    }
}