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
    public class ManageTypeController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/ManageType
        public ActionResult ManageType(string search, int? page, string sortBy)
        {
            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortType = sortBy == "Type" ? "Type Desc" : "Type";
            ViewBag.SortDescription = sortBy == "Description" ? "Description Desc" : "Description";
            ViewBag.SortAddedBy = sortBy == "AddedBy" ? "AddedBy Desc" : "AddedBy";
            ViewBag.SortActive = sortBy == "IsActive" ? "IsActive Desc" : "IsActive";


            var filterType = dobj.AddType.Where(x => x.Type.Contains(search) || search == null);
            var filterAddedBy = dobj.AddType.Where(x => x.Users.FirstName.Contains(search));
            var filterDescription = dobj.AddType.Where(x => x.Description.Contains(search));
            var filterDateAdded = dobj.AddType.Where(x => x.CreatedDate.ToString().Contains(search));
            var filterAddedByLast = dobj.AddType.Where(x => x.Users.LastName.Contains(search));
            var filterActive = dobj.AddType.Where(x => x.IsActive.ToString().Contains(search));
            var filtered = filterType.Union(filterAddedBy).Union(filterDescription).Union(filterAddedByLast).Union(filterDateAdded).Union(filterActive);
            var table__entry = filtered.Where(x => x.CreatedBy == obj.ID).ToList().AsQueryable();

            switch (sortBy)
            {
                case "Date Desc":
                    table__entry = table__entry.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Type":
                    table__entry = table__entry.OrderBy(x => x.Type);
                    break;
                case "Type Desc":
                    table__entry = table__entry.OrderByDescending(x => x.Type);
                    break;
                case "Description":
                    table__entry = table__entry.OrderBy(x => x.Description);
                    break;
                case "Description Desc":
                    table__entry = table__entry.OrderByDescending(x => x.Description);
                    break;
                case "AddedBy":
                    table__entry = table__entry.OrderBy(x => x.Users.FirstName + x.Users.LastName);
                    break;
                case "AddedBy Desc":
                    table__entry = table__entry.OrderByDescending(x => x.Users.FirstName + x.Users.LastName);
                    break;
                case "IsActive":
                    table__entry = table__entry.OrderBy(x => x.IsActive);
                    break;
                case "IsActive Desc":
                    table__entry = table__entry.OrderByDescending(x => x.IsActive);
                    break;
                default:
                    table__entry = table__entry.OrderBy(x => x.CreatedDate);
                    break;
            }

            return View(table__entry.ToPagedList(page ?? 1, 5));
        }

        public ActionResult DeleteRecord(int ID)
        {
            AddType addType = dobj.AddType.Where(x => x.ID == ID).FirstOrDefault();
            addType.IsActive = false;
            dobj.SaveChanges();
            return RedirectToAction("ManageType", "ManageType");
        }
    }
}