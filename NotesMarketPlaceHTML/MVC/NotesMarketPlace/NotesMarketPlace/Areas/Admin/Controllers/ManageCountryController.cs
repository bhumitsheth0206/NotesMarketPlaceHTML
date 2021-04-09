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
    public class ManageCountryController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/ManageCountry

        public ActionResult ManageCountry(string search, int? page, string sortBy)
        {
            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortCountryName = sortBy == "CountryName" ? "CountryName Desc" : "CountryName";
            ViewBag.SortCountryCode = sortBy == "CountryCode" ? "CountryCode Desc" : "CountryCode";
            ViewBag.SortAddedBy = sortBy == "AddedBy" ? "AddedBy Desc" : "AddedBy";
            ViewBag.SortActive = sortBy == "IsActive" ? "IsActive Desc" : "IsActive";


            var filterCountryName = dobj.AddCountry.Where(x => x.CountryName.Contains(search) || search == null);
            var filterAddedBy = dobj.AddCountry.Where(x => x.Users.FirstName.Contains(search));
            var filterCountryCode = dobj.AddCountry.Where(x => x.CountryCode.Contains(search));
            var filterDateAdded = dobj.AddCountry.Where(x => x.CreatedDate.ToString().Contains(search));
            var filterAddedByLast = dobj.AddCountry.Where(x => x.Users.LastName.Contains(search));
            var filterActive = dobj.AddCountry.Where(x => x.IsActive.ToString().Contains(search));
            var filtered = filterCountryName.Union(filterAddedBy).Union(filterCountryCode).Union(filterAddedByLast).Union(filterDateAdded).Union(filterActive);
            var table__entry = filtered.Where(x => x.CreatedBy == obj.ID).ToList().AsQueryable();

            switch (sortBy)
            {
                case "Date Desc":
                    table__entry = table__entry.OrderByDescending(x => x.CreatedDate);
                    break;
                case "CountryName":
                    table__entry = table__entry.OrderBy(x => x.CountryName);
                    break;
                case "CountryName Desc":
                    table__entry = table__entry.OrderByDescending(x => x.CountryName);
                    break;
                case "CountryCode":
                    table__entry = table__entry.OrderBy(x => x.CountryCode);
                    break;
                case "CountryCode Desc":
                    table__entry = table__entry.OrderByDescending(x => x.CountryCode);
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
            AddCountry addCountry = dobj.AddCountry.Where(x => x.ID == ID).FirstOrDefault();
            addCountry.IsActive = false;
            dobj.SaveChanges();
            return RedirectToAction("ManageCountry", "ManageCountry");
        }

    }
}