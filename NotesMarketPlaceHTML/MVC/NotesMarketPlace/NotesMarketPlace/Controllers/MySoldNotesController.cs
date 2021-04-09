using NotesMarketPlace.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Controllers
{
    [Authorize(Roles = "member")]
    public class MySoldNotesController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        [Route("MySoldNotes")]
        // GET: MySoldNotes
        public ActionResult MySoldNotes(string search, int? page, string sortBy)
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

            var table__entry = filtereddata.Where(x => x.Seller == noteID.UID).ToList().AsQueryable();

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

            if (downloads.IsAttachmentDownloaded == true)
            {
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename =" + ndetail.NoteTitle + ".pdf");
                Response.TransmitFile(seller_notes.FilePath);
                Response.End();
            }

            return View();
        }

    }
}