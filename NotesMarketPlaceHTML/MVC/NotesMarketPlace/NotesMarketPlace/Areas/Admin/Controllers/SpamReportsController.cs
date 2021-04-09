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

    public class SpamReportsController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/SpamReports
        public ActionResult SpamReports(string search, int? page, string sortBy)
        {
            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortTitle = sortBy == "Title" ? "Title Desc" : "Title";
            ViewBag.SortCategory = sortBy == "Category" ? "Category Desc" : "Category";
            ViewBag.SortReportedBy = sortBy == "ReportedBy" ? "ReportedBy Desc" : "ReportedBy";
            ViewBag.SortRemark = sortBy == "Remark" ? "Remark Desc" : "Remark";

            var filterTitle = dobj.SpamReports.Where(x => x.NoteDetails.NoteTitle.Contains(search) || search == null);
            var filterCategory = dobj.SpamReports.Where(x => x.Downloads.Category.Contains(search));
            var filterReportedBy = dobj.SpamReports.Where(x => x.Users.FirstName.Contains(search));
            var filterDateAdded = dobj.SpamReports.Where(x => x.CreatedDate.ToString().Contains(search));
            var filterRemark = dobj.SpamReports.Where(x => x.Remark.Contains(search));
            var filtered = filterTitle.Union(filterCategory).Union(filterReportedBy).Union(filterDateAdded).Union(filterRemark);
            var table__entry = filtered.ToList().AsQueryable();

            switch (sortBy)
            {
                case "Date Desc":
                    table__entry = table__entry.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Title":
                    table__entry = table__entry.OrderBy(x => x.NoteDetails.NoteTitle);
                    break;
                case "Title Desc":
                    table__entry = table__entry.OrderByDescending(x => x.NoteDetails.NoteTitle);
                    break;
                case "Category":
                    table__entry = table__entry.OrderBy(x => x.Downloads.Category);
                    break;
                case "Category Desc":
                    table__entry = table__entry.OrderByDescending(x => x.Downloads.Category);
                    break;
                case "ReportedBy":
                    table__entry = table__entry.OrderBy(x => x.Users.FirstName);
                    break;
                case "ReportedBy Desc":
                    table__entry = table__entry.OrderByDescending(x => x.Users.FirstName);
                    break;
                case "Remark":
                    table__entry = table__entry.OrderBy(x => x.Remark);
                    break;
                case "Remark Desc":
                    table__entry = table__entry.OrderByDescending(x => x.Remark);
                    break;
                default:
                    table__entry = table__entry.OrderBy(x => x.CreatedDate);
                    break;
            }

            return View(table__entry.ToPagedList(page ?? 1, 5));
        }

        public ActionResult DeleteRecord(int id)
        {
            SpamReports spam = dobj.SpamReports.Where(x => x.ID == id).FirstOrDefault();
            dobj.SpamReports.Remove(spam);
            dobj.SaveChanges();

            return RedirectToAction("SpamReports", "SpamReports");
        }
    }
}