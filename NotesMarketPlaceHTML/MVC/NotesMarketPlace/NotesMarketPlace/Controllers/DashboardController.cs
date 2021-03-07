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

namespace NotesMarketPlace.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();
        
        [Route("Dashboard")]
        // GET: Dashboard
        [HttpGet]
        public ActionResult Dashboard(string search, int? page,string sortBy)
        {
            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortNoteTitle = sortBy == "Title" ? "Title Desc" : "Title";
            ViewBag.SortCategory = sortBy == "Category" ? "Category Desc" : "Category";

            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();
            var filterTitle = dobj.NoteDetails.Where(x => x.NoteTitle.Contains(search) || search==null);
            var filterStatus = dobj.NoteDetails.Include(x => x.ReferenceData).Where(x => x.ReferenceData.Value.Contains(search));
            var filterCategory = dobj.NoteDetails.Include(x => x.AddCategory).Where(x => x.AddCategory.CategoryName.Contains(search));
            var filtered = filterTitle.Union(filterStatus).Union(filterCategory);
            var table__entry = filtered.Where(x => x.UID == obj.ID && (x.Status == 4 || x.Status == 5 || x.Status == 6)).Include(x => x.ReferenceData).ToList().AsQueryable();

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
                default:
                    table__entry = table__entry.OrderBy(x => x.CreatedDate);
                    break;
            }

            return View(table__entry.ToPagedList(page ?? 1,5));
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