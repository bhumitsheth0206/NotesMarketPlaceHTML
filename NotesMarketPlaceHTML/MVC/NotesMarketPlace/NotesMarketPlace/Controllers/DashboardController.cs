using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    [Authorize(Roles = "member")]
    public class DashboardController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();
        
        [Route("Dashboard")]
        // GET: Dashboard
        [HttpGet]
        public ActionResult Dashboard(string search, int? page,string sortBy)
        {
            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            ViewBag.myDownloads = dobj.Downloads.Where(x => x.IsSellerHasAllowedDownload == true && x.Users1.EmailID == emailid).Count();
            ViewBag.myBuyerRequest = dobj.Downloads.Where(x => x.IsSellerHasAllowedDownload == false && x.Users.EmailID == emailid).Count();
            ViewBag.mySoldNotes = dobj.Downloads.Where(x => x.IsAttachmentDownloaded == true && x.Users.EmailID == emailid).Count();
            ViewBag.notePrice = dobj.Downloads.Where(x => x.IsSellerHasAllowedDownload == true && x.Users.EmailID == emailid).Sum(x => x.PurchasedPrice);
            ViewBag.myRejected = dobj.NoteDetails.Where(x => x.Status == 5).Count();

            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortNoteTitle = sortBy == "Title" ? "Title Desc" : "Title";
            ViewBag.SortCategory = sortBy == "Category" ? "Category Desc" : "Category";

            List<NoteDetails> note = dobj.NoteDetails.Where(x => x.UID == obj.ID && x.IsActive == true).ToList();
            List<AddCategory> category = dobj.AddCategory.ToList();
            List<ReferenceData> reference = dobj.ReferenceData.Where(x => x.RefCategory == "NotesStatus" && (x.Value == "Draft" || x.Value == "Submitted For Review" || x.Value == "In Review")).ToList();

            var table__entry = from n in note
                             join c in category on n.CategoryID equals c.ID into table1
                             from c in table1.ToList()
                             join r in reference on n.Status equals r.ID into table2
                             from r in table2.ToList()
                             where (r.Value != "Published")
                             select new dashboard
                             {
                                 AddCategory = c,
                                 ReferenceData = r,
                                 NoteDetails = n
                             };

            if(!String.IsNullOrEmpty(search))
            {
                table__entry = table__entry.Where(x => x.NoteDetails.NoteTitle.Contains(search) || x.AddCategory.CategoryName.Contains(search) || x.ReferenceData.Value.Contains(search)).ToList().AsQueryable();
            }

            switch (sortBy)
            {
                case "Date Desc":
                    table__entry = table__entry.OrderByDescending(x => x.NoteDetails.CreatedDate);
                    break;
                case "Title":
                    table__entry = table__entry.OrderBy(x => x.NoteDetails.NoteTitle);
                    break;
                case "Title Desc":
                    table__entry = table__entry.OrderByDescending(x => x.NoteDetails.NoteTitle);
                    break;
                case "Category":
                    table__entry = table__entry.OrderBy(x => x.AddCategory.CategoryName);
                    break;
                case "Category Desc":
                    table__entry = table__entry.OrderByDescending(x => x.AddCategory.CategoryName);
                    break;
                default:
                    table__entry = table__entry.OrderBy(x => x.NoteDetails.CreatedDate);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            ViewBag.table_entry = table__entry.ToPagedList(pageNumber, pageSize);

            //Published Notes Table
            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortNoteTitle = sortBy == "Title" ? "Title Desc" : "Title";
            ViewBag.SortCategory = sortBy == "Category" ? "Category Desc" : "Category";

            List<NoteDetails> notes = dobj.NoteDetails.Where(x => x.UID == obj.ID && x.IsActive == true).ToList();
            List<AddCategory> categories = dobj.AddCategory.ToList();
            List<ReferenceData> references = dobj.ReferenceData.Where(x => x.RefCategory == "NotesStatus" && (x.Value == "Published")).ToList();

            var published__entry = from n in notes
                               join c in categories on n.CategoryID equals c.ID into table1
                               from c in table1.ToList()
                               join r in references on n.Status equals r.ID into table2
                               from r in table2.ToList()
                               where (r.Value == "Published")
                               select new dashboard
                               {
                                   AddCategory = c,
                                   ReferenceData = r,
                                   NoteDetails = n
                               };

            if (!String.IsNullOrEmpty(search))
            {
                published__entry = published__entry.Where(x => x.NoteDetails.NoteTitle.Contains(search) || x.AddCategory.CategoryName.Contains(search) || x.ReferenceData.Value.Contains(search)).ToList().AsQueryable();
            }

            switch (sortBy)
            {
                case "Date Desc":
                    published__entry = published__entry.OrderByDescending(x => x.NoteDetails.PublishedDate);
                    break;
                case "Title":
                    published__entry = published__entry.OrderBy(x => x.NoteDetails.NoteTitle);
                    break;
                case "Title Desc":
                    published__entry = published__entry.OrderByDescending(x => x.NoteDetails.NoteTitle);
                    break;
                case "Category":
                    published__entry = published__entry.OrderBy(x => x.AddCategory.CategoryName);
                    break;
                case "Category Desc":
                    published__entry = published__entry.OrderByDescending(x => x.AddCategory.CategoryName);
                    break;
                default:
                    published__entry = published__entry.OrderBy(x => x.NoteDetails.PublishedDate);
                    break;
            }

            int pagesize = 5;
            int pagenumber = (page ?? 1);

            ViewBag.published_entry = published__entry.ToPagedList(pagenumber, pagesize);
            
            return View();
        }

        public ActionResult DeleteRecord(int id)
        {
            SellerNotesAttachments sellernote = dobj.SellerNotesAttachments.Where(x => x.NoteID == id).FirstOrDefault();
            NoteDetails note = dobj.NoteDetails.Where(x => x.ID == id).FirstOrDefault();
            string folderpath = Server.MapPath("~/Members/" + note.UID + "/" + id);
            Directory.Delete(folderpath, true);

            dobj.SellerNotesAttachments.Remove(sellernote);
            dobj.NoteDetails.Remove(note);
            dobj.SaveChanges();

            return RedirectToAction("Dashboard");
        }
        
    }
}