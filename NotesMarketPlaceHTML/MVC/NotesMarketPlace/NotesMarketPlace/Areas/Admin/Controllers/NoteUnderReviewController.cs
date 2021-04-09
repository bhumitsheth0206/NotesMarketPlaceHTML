using NotesMarketPlace.Areas.Admin.Data;
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
    public class NoteUnderReviewController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/NoteUnderReview
        public ActionResult NoteUnderReview(string Seller, string search, int? page, string sortBy)
        {
            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            var seller_name = dobj.NoteDetails.Select(x => x.Users.FirstName).Distinct().ToList();
      
            ViewBag.Seller = new SelectList(seller_name);

            ViewBag.SortAddedDate = string.IsNullOrEmpty(sortBy) ? "Date Desc" : "";
            ViewBag.SortTitle = sortBy == "Title" ? "Title Desc" : "Title";
            ViewBag.SortCategory = sortBy == "Category" ? "Category Desc" : "Category";
            ViewBag.SortSeller = sortBy == "Seller" ? "Seller Desc" : "Seller";
            ViewBag.SortStatus = sortBy == "Status" ? "Status Desc" : "Status";

            var filterTitle = dobj.NoteDetails.Where(x => x.NoteTitle.Contains(search) || search == null);
            var filterCategory = dobj.NoteDetails.Where(x => x.AddCategory.CategoryName.Contains(search));
            var filterSeller = dobj.NoteDetails.Where(x => x.Users.FirstName.Contains(search));
            var filterDateAdded = dobj.NoteDetails.Where(x => x.CreatedDate.ToString().Contains(search));
            var filterStatus = dobj.NoteDetails.Where(x => x.ReferenceData.Datavalue.Contains(search));
            var filtered = filterTitle.Union(filterCategory).Union(filterSeller).Union(filterStatus).Union(filterDateAdded);
            var table__entry = filtered.Where(x => x.Users.FirstName.Contains(Seller) || Seller== null).ToList().AsQueryable();

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
                case "Seller":
                    table__entry = table__entry.OrderBy(x => x.Users.FirstName);
                    break;
                case "Seller Desc":
                    table__entry = table__entry.OrderByDescending(x => x.Users.FirstName);
                    break;
                case "Status":
                    table__entry = table__entry.OrderBy(x => x.ReferenceData.Datavalue);
                    break;
                case "Status Desc":
                    table__entry = table__entry.OrderByDescending(x => x.ReferenceData.Datavalue);
                    break;
                default:
                    table__entry = table__entry.OrderBy(x => x.CreatedDate);
                    break;
            }

            return View(table__entry.ToPagedList(page ?? 1, 5));
        }

        [HttpGet]
        public ActionResult NoteInReview(int ID)
        {
            NoteDetails ndetails = dobj.NoteDetails.Where(x => x.ID == ID).FirstOrDefault();

            var statusvalue = "In Review";
            ReferenceData refdata = dobj.ReferenceData.Where(x => x.RefCategory == "NotesStatus" && x.Datavalue == statusvalue && x.IsActive == true).FirstOrDefault();
            ndetails.Status = refdata.ID;
            dobj.SaveChanges();

            return RedirectToAction("NoteUnderReview", "NoteUnderReview");
        }

        [HttpGet]
        public ActionResult NoteApprove(int ID)
        {
            NoteDetails ndetails = dobj.NoteDetails.Where(x => x.ID == ID).FirstOrDefault();
            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            var statusvalue = "Published";
            ReferenceData refdata = dobj.ReferenceData.Where(x => x.RefCategory == "NotesStatus" && x.Datavalue == statusvalue && x.IsActive == true).FirstOrDefault();
            ndetails.Status = refdata.ID;
            ndetails.PublishedDate = DateTime.Now;
            ndetails.ActionedBy = obj.ID;
            dobj.SaveChanges();

            return RedirectToAction("NoteUnderReview", "NoteUnderReview");
        }

        [HttpPost]
        public ActionResult Notes_Rejected(int ID, string Remark)
        {
            NoteDetails ndetails = dobj.NoteDetails.Where(x => x.ID == ID).FirstOrDefault();
            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

            var statusvalue = "Rejected";
            ReferenceData refdata = dobj.ReferenceData.Where(x => x.RefCategory == "NotesStatus" && x.Datavalue == statusvalue && x.IsActive == true).FirstOrDefault();
            ndetails.Status = refdata.ID;
            ndetails.Remark = Remark;
            ndetails.ModifiedDate = DateTime.Now;
            ndetails.ActionedBy = obj.ID;
            dobj.SaveChanges();

            return RedirectToAction("NoteUnderReview", "NoteUnderReview");
        }

        [HttpGet]
        public ActionResult NoteDetails(int? ID)
        {
            NoteDetails ndetail = dobj.NoteDetails.Where(x => x.ID == ID).FirstOrDefault();

            return View(ndetail);
        }

        [HttpPost]
        public ActionResult Download_Note(NoteDetails model)
        {
            SellerNotesAttachments seller_notes = dobj.SellerNotesAttachments.Where(x => x.NoteID == model.ID).FirstOrDefault();
            NoteDetails ndetail = dobj.NoteDetails.Where(x => x.ID == model.ID).FirstOrDefault();

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename =" + ndetail.NoteTitle + ".pdf");
            Response.TransmitFile(seller_notes.FilePath);
            Response.End();


            return RedirectToAction("NoteUnderReview", "NoteUnderReview");
        }

        public ActionResult DownloadNotes(int? id)
        {
            NoteDetails ndetail = dobj.NoteDetails.Where(x => x.ID == id).FirstOrDefault();
            SellerNotesAttachments seller_notes = dobj.SellerNotesAttachments.Where(x => x.NoteID == id).FirstOrDefault();

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename =" + ndetail.NoteTitle + ".pdf");
            Response.TransmitFile(seller_notes.FilePath);
            Response.End();

            return RedirectToAction("NoteUnderReview", "NoteUnderReview");
        }

        public ActionResult DeleteReview(int ID)
        {
            NoteReview review = dobj.NoteReview.Where(x => x.ID == ID).FirstOrDefault();
            dobj.NoteReview.Remove(review);
            dobj.SaveChanges();

            return RedirectToAction("NoteDetails", "NoteUnderReview", new { ID = review.NoteID});
        }
    }
}