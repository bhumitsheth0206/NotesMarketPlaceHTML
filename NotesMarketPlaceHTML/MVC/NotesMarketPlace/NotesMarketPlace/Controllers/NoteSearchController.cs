using NotesMarketPlace.Context;
using NotesMarketPlace.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Controllers
{
    
    public class NoteSearchController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        [Route("NoteSearch")]
        // GET: NoteSearch
        public ActionResult NoteSearch(string Search_keyword, string Type, string Category, string University, string Courses, string Country, int? page)
        {
            var country = dobj.AddCountry.ToList();
            var category = dobj.AddCategory.ToList();
            var type = dobj.AddType.ToList();
            var university = dobj.NoteDetails.Where(x => x.InstitutionName != null).Select(x => x.InstitutionName).Distinct().ToList();
            var course = dobj.NoteDetails.Where(x => x.Course != null).Select(x => x.Course).Distinct().ToList();
            var note = dobj.NoteDetails.Where(x => x.NoteTitle.Contains(Search_keyword) || Search_keyword==null).ToList().AsQueryable();
            var dropdownitems = new notesearch()
            {
                Country = country,
                Category = category,
                Type = type,
                University = university,
                Courses = course,
                Note = note.Where(x => x.TypeID.ToString() == Type || String.IsNullOrEmpty(Type) && x.CategoryID.ToString() == Category || String.IsNullOrEmpty(Category) && x.InstitutionName == University || String.IsNullOrEmpty(University) && x.Course == Courses || String.IsNullOrEmpty(Courses) && x.CountryID.ToString() == Country || String.IsNullOrEmpty(Country)).ToPagedList(page ?? 1, 6)

            };
            ViewBag.notecount = dropdownitems.Note.Count();

            return View(dropdownitems);
        }

        
        [HttpGet]
        [Route("NoteDetails")]
        public ActionResult NoteDetails(int? ID)
        {
            NoteDetails ndetail = dobj.NoteDetails.Where(x => x.ID == ID).FirstOrDefault();
        
            return View(ndetail);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Download_Note(NoteDetails model)
        {
            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            SellerNotesAttachments seller_notes = dobj.SellerNotesAttachments.Where(x => x.NoteID == model.ID).FirstOrDefault();
            NoteDetails ndetail = dobj.NoteDetails.Where(x => x.ID == model.ID).FirstOrDefault();
            Users seller_email = dobj.Users.Where(x => x.ID == ndetail.UID).FirstOrDefault();
            Downloads downloads = new Downloads();
            if (ModelState.IsValid)
            {

                if (ndetail.IsPaid == false)
                {
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename =" + ndetail.NoteTitle + ".pdf");
                    Response.TransmitFile(seller_notes.FilePath);
                    Response.End();
                    downloads.IsAttachmentDownloaded = true;
                }

                else
                {
                    var fromEmail = new MailAddress(""); //Email of Company
                    var toEmail = new MailAddress(seller_email.EmailID); //Seller Email
                    var fromEmailPassword = "*******"; // Replace with actual password
                    string subject = obj.FirstName + "- wants to purchase your notes";

                    string body = "Hello," + seller_email.FirstName +
                        "<br/><br/>We would like to inform you that, "+  obj.FirstName  + " wants to purchase your notes. Please see " +
                        "Buyer Requests tab and allow download access to Buyer if you have received the payment from " +
                        "him." +
                        "<br/><br/><br/>Regards," +
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

                }
                if(ndetail.UID == obj.ID)
                {
                    TempData["ErrorMsg"] = "Email Address is not  yet verified";
                }
                else
                {
                    downloads.NoteID = ndetail.ID;
                    downloads.Seller = ndetail.UID;
                    downloads.Downloader = obj.ID;

                    if (ndetail.IsPaid.Equals(false))
                    {
                        downloads.IsSellerHasAllowedDownload = true;
                        downloads.AttachmentPath = seller_notes.FilePath;
                        downloads.IsAttachmentDownloaded = true;
                        downloads.AttachmentDownloadedDate = DateTime.Now;
                        downloads.IsPaid = ndetail.IsPaid;
                    }
                    else
                    {
                        downloads.IsSellerHasAllowedDownload = false;
                        downloads.AttachmentPath = null;
                        downloads.IsAttachmentDownloaded = false;
                        downloads.AttachmentDownloadedDate = null;
                        downloads.IsPaid = ndetail.IsPaid;
                    }

                    downloads.PurchasedPrice = ndetail.SellPrice;
                    downloads.NoteTitle = ndetail.NoteTitle;
                    downloads.Category = ndetail.AddCategory.CategoryName;
                    downloads.CreatedDate = DateTime.Now;

                    dobj.Downloads.Add(downloads);
                    dobj.SaveChanges();

                }
            }
                
            return RedirectToAction("NoteDetails", "NoteSearch", new { ID = ndetail.ID });
        }

        
    }
}
