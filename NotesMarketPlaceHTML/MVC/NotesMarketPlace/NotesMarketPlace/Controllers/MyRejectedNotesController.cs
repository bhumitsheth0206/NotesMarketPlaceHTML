using NotesMarketPlace.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Controllers
{
    [Authorize(Roles = "member")]
    public class MyRejectedNotesController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        [Route("MyRejectedNotes")]

        // GET: MyRejectedNotes
        public ActionResult MyRejectedNotes(string search, int? page, string sortBy)
        {
           
            ViewBag.SortTitle = sortBy == "Title" ? "Title Desc" : "Title";
            ViewBag.SortCategory = sortBy == "Category" ? "Category Desc" : "Category";
            ViewBag.SortRemark = sortBy == "Remark" ? "Remark Desc" : "Remark";

            var filterTitle = dobj.NoteDetails.Where(x => x.NoteTitle.Contains(search) || search == null);
            var filterCategory = dobj.NoteDetails.Where(x => x.AddCategory.CategoryName.Contains(search));
            var filterRemark = dobj.NoteDetails.Where(x => x.Remark.Contains(search));
            var filtered = filterTitle.Union(filterCategory).Union(filterRemark);
            var table__entry = filtered.Where(x => x.Status == 5).ToList().AsQueryable();

            switch (sortBy)
            {
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
                case "Remark":
                    table__entry = table__entry.OrderBy(x => x.Remark);
                    break;
                case "Remark Desc":
                    table__entry = table__entry.OrderByDescending(x => x.Remark);
                    break;
            }

            return View(table__entry.ToPagedList(page ?? 1, 10));
        }

        public ActionResult DownloadNotes(int? id)
        {
            NoteDetails ndetail = dobj.NoteDetails.Where(x => x.ID == id).FirstOrDefault();
            SellerNotesAttachments seller_notes = dobj.SellerNotesAttachments.Where(x => x.NoteID == id).FirstOrDefault();

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename =" + ndetail.NoteTitle + ".pdf");
            Response.TransmitFile(seller_notes.FilePath);
            Response.End();

            return RedirectToAction("MyRejectedNotes", "MyRejectedNotes");
        }

    }
}