using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace NotesMarketPlace.Controllers
{
    [Authorize]
    public class BuyerRequestController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        [Route("BuyerRequest")]
        // GET: BuyerRequest
        public ActionResult BuyerRequest(string search,int? page, string sortBy)
        {
            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortNoteTitle = sortBy == "Title" ? "Title Desc" : "Title";
            ViewBag.SortCategory = sortBy == "Category" ? "Category Desc" : "Category";

            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            var filterTitle = dobj.NoteDetails.Where(x => x.NoteTitle.Contains(search) || search == null);
            var filterCategory = dobj.NoteDetails.Include(x => x.AddCategory).Where(x => x.AddCategory.CategoryName.Contains(search));

            var filtereddata = filterTitle.Union(filterCategory);

            var table__entry = filtereddata.Where(x => x.UID == obj.ID ).ToList().AsQueryable();

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


            return View(table__entry.ToPagedList(page ?? 1, 10));
        }
    }
}