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

    public class MembersController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/Members
        public ActionResult Members(string search, int? page, string sortBy)
        {
            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            var seller_name = dobj.NoteDetails.Select(x => x.Users.FirstName).Distinct().ToList();

            ViewBag.Seller = new SelectList(seller_name);

            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortFirstName = sortBy == "FirstName" ? "FirstName Desc" : "FirstName";
            ViewBag.SortLastName = sortBy == "LastName" ? "LastName Desc" : "LastName";
            ViewBag.SortEmail = sortBy == "Email" ? "Email Desc" : "Email";
           
            var filterFirstName = dobj.Users.Where(x => x.FirstName.Contains(search) || search == null);
            var filterLastName = dobj.Users.Where(x => x.LastName.Contains(search));
            var filterEmail = dobj.Users.Where(x => x.EmailID.Contains(search));
            var filterDateAdded = dobj.Users.Where(x => x.CreatedDate.ToString().Contains(search));
            var filtered = filterFirstName.Union(filterLastName).Union(filterEmail).Union(filterDateAdded);
            var table__entry = filtered.Where(x => x.RoleID == 3 && x.IsActive == true).ToList().AsQueryable();

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
                default:
                    table__entry = table__entry.OrderBy(x => x.CreatedDate);
                    break;
            }

            return View(table__entry.ToPagedList(page ?? 1, 5));
        }

        public ActionResult MemberDetails(int ID, string search, int? page, string sortBy)
        {
             ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
             ViewBag.SortTitle = sortBy == "Title" ? "Title Desc" : "Title";
             ViewBag.SortCategory = sortBy == "Category" ? "Category Desc" : "Category";
             ViewBag.SortStatus = sortBy == "Status" ? "Status Desc" : "Status";

             var filterTitle = dobj.NoteDetails.Where(x => x.NoteTitle.Contains(search) || search == null);
             var filterCategory = dobj.NoteDetails.Where(x => x.AddCategory.CategoryName.Contains(search));
             var filterStatus = dobj.NoteDetails.Where(x => x.ReferenceData.Datavalue.Contains(search));
             var filterDateAdded = dobj.NoteDetails.Where(x => x.Users.CreatedDate.ToString().Contains(search));
             var filtered = filterTitle.Union(filterCategory).Union(filterStatus).Union(filterDateAdded);
             var table__entry = filtered.Where(x => x.UID == ID).ToList().AsQueryable();

             switch (sortBy)
             {
                 case "Date Desc":
                     table__entry = table__entry.OrderByDescending(x => x.Users.CreatedDate);
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
                 case "Status":
                     table__entry = table__entry.OrderBy(x => x.Users.FirstName);
                     break;
                 case "Status Desc":
                     table__entry = table__entry.OrderByDescending(x => x.Users.FirstName);
                     break;
                 default:
                     table__entry = table__entry.OrderBy(x => x.Users.CreatedDate);
                     break;
             }
            
            UserProfile profile = dobj.UserProfile.Where(x => x.UID == ID).FirstOrDefault();
            ViewBag.Note = table__entry.ToPagedList(page ?? 1, 5);

            return View(profile);
        }

        public ActionResult DeleteUser(int ID)
        {
            Users obj = dobj.Users.Where(x => x.ID == ID).FirstOrDefault();
            List<NoteDetails> note = dobj.NoteDetails.Where(x => x.UID == ID).ToList();
            foreach(var item in note)
            {
                item.Status = 6;
            }
            obj.IsActive = false;

            dobj.SaveChanges();
            return RedirectToAction("Members", "Members");
        }
    }
}