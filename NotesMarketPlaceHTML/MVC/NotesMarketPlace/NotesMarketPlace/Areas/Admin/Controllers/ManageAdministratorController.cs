using NotesMarketPlace.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Areas.Admin.Controllers
{
    [Authorize(Roles = "superadmin")]
    public class ManageAdministratorController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/ManageAdministrator
        public ActionResult ManageAdministrator(string search, int? page, string sortBy)
        {

            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortFirstName = sortBy == "FirstName" ? "FirstName Desc" : "FirstName";
            ViewBag.SortLastName = sortBy == "LastName" ? "LastName Desc" : "LastName";
            ViewBag.SortEmail = sortBy == "Email" ? "Email Desc" : "Email";
            ViewBag.SortPhone = sortBy == "Phone" ? "Phone Desc" : "Phone";
            ViewBag.SortActive = sortBy == "IsActive" ? "IsActive Desc" : "IsActive";


            var filterFirstName = dobj.Users.Where(x => x.FirstName.Contains(search) || search == null);
            var filterLastName = dobj.Users.Where(x => x.LastName.Contains(search));
            var filterEmail = dobj.Users.Where(x => x.EmailID.Contains(search));
            var filterDateAdded = dobj.Users.Where(x => x.CreatedDate.ToString().Contains(search));
            var filterPhone = dobj.Users.Where(x => x.PhoneNumber.Contains(search));
            var filterActive = dobj.Users.Where(x => x.IsActive.ToString().Contains(search));
            var filtered = filterFirstName.Union(filterLastName).Union(filterEmail).Union(filterPhone).Union(filterDateAdded).Union(filterActive);
            var table__entry = filtered.Where(x => x.UserRoles.Name.ToLower() == "admin").ToList().AsQueryable();

            switch (sortBy)
            {
                case "Date Desc":
                    table__entry = table__entry.OrderByDescending(x => x.CreatedDate);
                    break;
                case "FirstName":
                    table__entry = table__entry.OrderBy(x => x.FirstName);
                    break;
                case "FirstName Desc":
                    table__entry = table__entry.OrderByDescending(x => x.FirstName);
                    break;
                case "LastName":
                    table__entry = table__entry.OrderBy(x => x.LastName);
                    break;
                case "LastName Desc":
                    table__entry = table__entry.OrderByDescending(x => x.LastName);
                    break;
                case "Email":
                    table__entry = table__entry.OrderBy(x => x.EmailID);
                    break;
                case "Email Desc":
                    table__entry = table__entry.OrderByDescending(x => x.EmailID);
                    break;
                case "Phone":
                    table__entry = table__entry.OrderBy(x => x.PhoneNumber);
                    break;
                case "Phone Desc":
                    table__entry = table__entry.OrderByDescending(x => x.PhoneNumber);
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
            Users obj = dobj.Users.Where(x => x.ID == ID).FirstOrDefault();
            obj.IsActive = false;
            dobj.SaveChanges();
            return RedirectToAction("ManageAdministrator", "ManageAdministrator");
        }
    }
}