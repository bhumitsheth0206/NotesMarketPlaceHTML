using NotesMarketPlace.Areas.Admin.Data;
using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin, superadmin")]
    public class AddCategoryController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/AddCategory
        public ActionResult AddCategory(int? ID)
        {
           
            if (ID != null)
            {
                var dropdownitems = dobj.AddCategory.Where(x => x.ID == ID).Select(x =>
                 new addcategory()
                 {
                     ID = x.ID,
                     CategoryName = x.CategoryName,
                     Description = x.Description
                 }
                
                ).FirstOrDefault();
                return View(dropdownitems);
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(addcategory model)
        {            
           if(ModelState.IsValid)
            {
                var emailid = User.Identity.Name.ToString();
                Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();

                AddCategory addCategory = new AddCategory();

                if (model.ID == 0)
                {

                    addCategory.ID = model.ID;
                    addCategory.CategoryName = model.CategoryName;
                    addCategory.Description = model.Description;
                    addCategory.CreatedDate = DateTime.Now;
                    addCategory.CreatedBy = obj.ID;
                    addCategory.IsActive = true;

                    dobj.AddCategory.Add(addCategory);
                    dobj.SaveChanges();
                }

                else
                {
                    addCategory = dobj.AddCategory.Where(x => x.ID == model.ID).FirstOrDefault();
                    addCategory.CategoryName = model.CategoryName;
                    addCategory.Description = model.Description;
                    addCategory.ModifiedDate = DateTime.Now;
                    addCategory.ModifiedBy = obj.ID;
                    addCategory.IsActive = true;

                    dobj.Entry(addCategory).State = EntityState.Modified;
                    dobj.SaveChanges();
                }

                
            }

            return View();
        }
    }
}