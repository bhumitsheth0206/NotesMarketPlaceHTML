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
    public class ManageCategoryController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/ManageCategory
        public ActionResult ManageCategory(string search, int? page, string sortBy)
        {
            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortCategory = sortBy == "Category" ? "Category Desc" : "Category";
            ViewBag.SortDescription = sortBy == "Description" ? "Description Desc" : "Description";
            ViewBag.SortAddedBy = sortBy == "AddedBy" ? "AddedBy Desc" : "AddedBy";
            ViewBag.SortActive = sortBy == "IsActive" ? "IsActive Desc" : "IsActive";


            var filterCategory = dobj.AddCategory.Where(x => x.CategoryName.Contains(search) || search == null);
            var filterAddedBy = dobj.AddCategory.Where(x => x.Users.FirstName.Contains(search));
            var filterDescription = dobj.AddCategory.Where(x => x.Description.Contains(search));
            var filterDateAdded = dobj.AddCategory.Where(x => x.CreatedDate.ToString().Contains(search));
            var filterAddedByLast = dobj.AddCategory.Where(x => x.Users.LastName.Contains(search));
            var filterActive = dobj.AddCategory.Where(x => x.IsActive.ToString().Contains(search));
            var filtered = filterCategory.Union(filterAddedBy).Union(filterDescription).Union(filterAddedByLast).Union(filterDateAdded).Union(filterActive);
            var table__entry = filtered.Where(x => x.CreatedBy == obj.ID).ToList().AsQueryable();

            switch (sortBy)
            {
                case "Date Desc":
                    table__entry = table__entry.OrderByDescending(x => x.CreatedDate);
                    break;
                case "Category":
                    table__entry = table__entry.OrderBy(x => x.CategoryName);
                    break;
                case "Category Desc":
                    table__entry = table__entry.OrderByDescending(x => x.CategoryName);
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
            AddCategory addCategory = dobj.AddCategory.Where(x => x.ID == ID).FirstOrDefault();
            addCategory.IsActive = false;
            dobj.SaveChanges();
            return RedirectToAction("ManageCategory","ManageCategory");
        }

    }
}