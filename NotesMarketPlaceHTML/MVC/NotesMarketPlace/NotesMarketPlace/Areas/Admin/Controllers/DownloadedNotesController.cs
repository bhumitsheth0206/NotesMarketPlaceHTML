using NotesMarketPlace.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin, superadmin")]

    public class DownloadedNotesController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/DownloadedNotes
        public ActionResult DownloadedNotes(string Note_Title, string Seller, string Buyer, string search, int? page, string sortBy)
        {
            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            var note_title = dobj.Downloads.Select(x => x.NoteTitle).Distinct().ToList();
            ViewBag.Note_Title = new SelectList(note_title);

            var seller = dobj.Downloads.Select(x => x.Users.FirstName).Distinct().ToList();
            ViewBag.Seller = new SelectList(seller);

            var buyer = dobj.Downloads.Select(x => x.Users1.FirstName).Distinct().ToList();
            ViewBag.Buyer = new SelectList(buyer);

            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortTitle = sortBy == "Title" ? "Title Desc" : "Title";
            ViewBag.SortCategory = sortBy == "Category" ? "Category Desc" : "Category";
            ViewBag.SortSeller = sortBy == "Seller" ? "Seller Desc" : "Seller";
            ViewBag.SortBuyer = sortBy == "Buyer" ? "Buyer Desc" : "Buyer";
            ViewBag.SortSelltype = sortBy == "Selltype" ? "Selltype Desc" : "Selltype";
            ViewBag.SortSellprice = sortBy == "Sellprice" ? "Sellprice Desc" : "Sellprice";

            var filterTitle = dobj.Downloads.Where(x => x.NoteTitle.Contains(search) || search == null);
            var filterCategory = dobj.Downloads.Where(x => x.Category.Contains(search));
            var filterSeller = dobj.Downloads.Where(x => x.Users.FirstName.Contains(search) || x.Users.LastName.Contains(search));
            var filterBuyer = dobj.Downloads.Where(x => x.Users1.FirstName.Contains(search) || x.Users1.LastName.Contains(search));
            var filterDateAdded = dobj.Downloads.Where(x => x.AttachmentDownloadedDate.ToString().Contains(search));
            var filtered = filterTitle.Union(filterCategory).Union(filterSeller).Union(filterDateAdded).Union(filterBuyer);
            var table__entry = filtered.Where(  x => ( x.NoteTitle.Contains(Note_Title) || Note_Title == null) && (x.Users.FirstName.Contains(Seller) || Seller == null) && (x.Users1.FirstName.Contains(Buyer) || Buyer == null)).ToList().AsQueryable();

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
                case "Seller":
                    table__entry = table__entry.OrderBy(x => x.Users.FirstName);
                    break;
                case "Seller Desc":
                    table__entry = table__entry.OrderByDescending(x => x.Users.FirstName);
                    break;
                case "Buyer":
                    table__entry = table__entry.OrderBy(x => x.Users1.FirstName);
                    break;
                case "Buyer Desc":
                    table__entry = table__entry.OrderByDescending(x => x.Users1.FirstName);
                    break;
                case "Selltype":
                    table__entry = table__entry.OrderBy(x => x.IsPaid);
                    break;
                case "Selltype Desc":
                    table__entry = table__entry.OrderByDescending(x => x.IsPaid);
                    break;
                case "Sellprice":
                    table__entry = table__entry.OrderBy(x => x.PurchasedPrice);
                    break;
                case "Sellprice Desc":
                    table__entry = table__entry.OrderByDescending(x => x.PurchasedPrice);
                    break;
                default:
                    table__entry = table__entry.OrderBy(x => x.CreatedDate);
                    break;
            }

            return View(table__entry.ToPagedList(page ?? 1, 5));
        }
    }
}