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

    public class RejectedNotesController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/RejectedNotes
        public ActionResult RejectedNotes(string Seller, string search, int? page, string sortBy)
        {
            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            var seller_name = dobj.NoteDetails.Select(x => x.Users.FirstName).Distinct().ToList();

            ViewBag.Seller = new SelectList(seller_name);

            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortTitle = sortBy == "Title" ? "Title Desc" : "Title";
            ViewBag.SortCategory = sortBy == "Category" ? "Category Desc" : "Category";
            ViewBag.SortSeller = sortBy == "Seller" ? "Seller Desc" : "Seller";
            ViewBag.SortRejectedBy = sortBy == "RejectedBy" ? "RejectedBy Desc" : "RejectedBy";
            ViewBag.SortRemark = sortBy == "Remark" ? "Remark Desc" : "Remark";

            var filterTitle = dobj.NoteDetails.Where(x => x.NoteTitle.Contains(search) || search == null);
            var filterCategory = dobj.NoteDetails.Where(x => x.AddCategory.CategoryName.Contains(search));
            var filterSeller = dobj.NoteDetails.Where(x => x.Users.FirstName.Contains(search));
            var filterDateAdded = dobj.NoteDetails.Where(x => x.ModifiedDate.ToString().Contains(search));
            var filterRejectedBy = dobj.NoteDetails.Where(x => x.ReferenceData.Datavalue.Contains(search));
            var filterRemark = dobj.NoteDetails.Where(x => x.Remark.Contains(search));
            var filtered = filterTitle.Union(filterCategory).Union(filterSeller).Union(filterRejectedBy).Union(filterDateAdded).Union(filterRemark);
            var table__entry = filtered.Where(x => x.Users.FirstName.Contains(Seller) || Seller == null).ToList().AsQueryable();

            switch (sortBy)
            {
                case "Date Desc":
                    table__entry = table__entry.OrderByDescending(x => x.ModifiedDate);
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
                case "RejectedBy":
                    table__entry = table__entry.OrderBy(x => x.Users.FirstName);
                    break;
                case "RejectedBy Desc":
                    table__entry = table__entry.OrderByDescending(x => x.Users.FirstName);
                    break;
                case "Remark":
                    table__entry = table__entry.OrderBy(x => x.Remark);
                    break;
                case "Remark Desc":
                    table__entry = table__entry.OrderByDescending(x => x.Remark);
                    break;
                default:
                    table__entry = table__entry.OrderBy(x => x.ModifiedDate);
                    break;
            }

            return View(table__entry.ToPagedList(page ?? 1, 5));
        }

        [HttpGet]
        public ActionResult NoteApprove(int ID)
        {
            NoteDetails ndetails = dobj.NoteDetails.Where(x => x.ID == ID).FirstOrDefault();

            var statusvalue = "Published";
            ReferenceData refdata = dobj.ReferenceData.Where(x => x.RefCategory == "NotesStatus" && x.Datavalue == statusvalue && x.IsActive == true).FirstOrDefault();
            ndetails.Status = refdata.ID;
            ndetails.PublishedDate = DateTime.Now;
            dobj.SaveChanges();

            return RedirectToAction("RejectedNotes", "RejectedNotes");
        }
    }
}