using NotesMarketPlace.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin, superadmin")]

    public class DashboardController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/Dashboard
        public ActionResult Dashboard(string MonthDropdown, string search, int? page, string sortBy)
        {
            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            ViewBag.NotesUnderReview = dobj.NoteDetails.Where(x => x.Status == 2 || x.Status == 3).Count();

            var date = System.DateTime.Now.AddDays(-7);
            ViewBag.NoOfDownloadedNotes = dobj.Downloads.Where(x => x.AttachmentDownloadedDate >= date).Count();
            ViewBag.NoOfRegistration = dobj.Users.Where(x => x.CreatedDate >= date).Count();

            List<SelectListItem> month_dropdown = new List<SelectListItem>();
            for (int i = 0; i < 6; i++)
            {
                var previous = DateTime.Now.AddMonths(-i);
                month_dropdown.Add(new SelectListItem()
                {
                    Text = previous.Date.ToString("MMMM"),
                    Value = previous.Month.ToString()
                });
            }

            ViewBag.MonthDropdown = month_dropdown;

            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortTitle = sortBy == "Title" ? "Title Desc" : "Title";
            ViewBag.SortCategory = sortBy == "Category" ? "Category Desc" : "Category";
            ViewBag.SortSeller = sortBy == "Seller" ? "Seller Desc" : "Seller";
            ViewBag.SortSelltype = sortBy == "Selltype" ? "Selltype Desc" : "Selltype";
            ViewBag.SortSellprice = sortBy == "Sellprice" ? "Sellprice Desc" : "Sellprice";
            ViewBag.AttachmentSize = sortBy == "AttachmentSize" ? "AttachmentSize Desc" : "AttachmentSize";

            var filterTitle = dobj.NoteDetails.Where(x => x.NoteTitle.Contains(search) || search == null);
            var filterCategory = dobj.NoteDetails.Where(x => x.AddCategory.CategoryName.Contains(search));
            var filterSellPrice = dobj.NoteDetails.Where(x => x.SellPrice.ToString().Contains(search));
            var filterSeller = dobj.NoteDetails.Where(x => x.Users1.FirstName.Contains(search));
            var filterDateAdded = dobj.NoteDetails.Where(x => x.PublishedDate.ToString().Contains(search));
            var filtered = filterTitle.Union(filterCategory).Union(filterSeller).Union(filterDateAdded);
            var table__entry = filtered.Where(x => (x.PublishedDate.Value.Month.ToString() == MonthDropdown || MonthDropdown == null) && x.Status == 4).ToList().AsQueryable();

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
                    table__entry = table__entry.OrderBy(x => x.AddCategory.CategoryName);
                    break;
                case "Category Desc":
                    table__entry = table__entry.OrderByDescending(x => x.AddCategory.CategoryName);
                    break;
                case "Seller":
                    table__entry = table__entry.OrderBy(x => x.Users.FirstName);
                    break;
                case "Seller Desc":
                    table__entry = table__entry.OrderByDescending(x => x.Users.FirstName);
                    break;
                case "Selltype":
                    table__entry = table__entry.OrderBy(x => x.IsPaid);
                    break;
                case "Selltype Desc":
                    table__entry = table__entry.OrderByDescending(x => x.IsPaid);
                    break;
                case "Sellprice":
                    table__entry = table__entry.OrderBy(x => x.SellPrice);
                    break;
                case "Sellprice Desc":
                    table__entry = table__entry.OrderByDescending(x => x.SellPrice);
                    break;
                case "AttachmentSize":
                    table__entry = table__entry.OrderBy(x => x.IsPaid);
                    break;
                case "AttachmentSize Desc":
                    table__entry = table__entry.OrderByDescending(x => x.IsPaid);
                    break;

                default:
                    table__entry = table__entry.OrderBy(x => x.CreatedDate);
                    break;
            }

            return View(table__entry.ToPagedList(page ?? 1, 5));
        }

        [HttpPost]
        public ActionResult Notes_Unpublish(int ID, string Remark)
        {
            NoteDetails ndetails = dobj.NoteDetails.Where(x => x.ID == ID).FirstOrDefault();

            var statusvalue = "Removed";
            ReferenceData refdata = dobj.ReferenceData.Where(x => x.RefCategory == "NotesStatus" && x.Datavalue == statusvalue && x.IsActive == true).FirstOrDefault();
            ndetails.Status = refdata.ID;
            ndetails.Remark = Remark;

            dobj.SaveChanges();

            var fromEmail = new MailAddress("bhumitsheth02@gmail.com", "Notes-MarketPlace"); //Email of Company
            var toEmail = new MailAddress(ndetails.Users.EmailID); //Seller EmailAddress
            var fromEmailPassword = "261999@Bhumit"; // Replace with actual password
            string subject = "Sorry! We need to remove your notes from our portal.";

            string body = "Hello," + ndetails.Users.FirstName +
                "<br/><br/>We want to inform you that, your note " + ndetails.NoteTitle + " has been removed from the portal." +
                " <br/>Please find our remarks as below-" +
                "<br/>" + ndetails.Remark +
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