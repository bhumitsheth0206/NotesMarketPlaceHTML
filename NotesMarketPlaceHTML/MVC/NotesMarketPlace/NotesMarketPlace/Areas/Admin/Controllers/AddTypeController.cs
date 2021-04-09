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
    public class AddTypeController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        // GET: Admin/AddType
        public ActionResult AddType(int? ID)
        {
            if (ID != null)
            {
                var dropdownitems = dobj.AddType.Where(x => x.ID == ID).Select(x =>
                 new addtype()
                 {
                     ID = x.ID,
                     Type = x.Type,
                     Description = x.Description
                 }

                ).FirstOrDefault();
                return View(dropdownitems);
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddType(addtype model)
        {
            var emailid = User.Identity.Name.ToString();
            Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();
           
            if(ModelState.IsValid)
            {
                if (model.ID == 0)
                {
                    AddType addType = new AddType();
                    addType.ID = model.ID;
                    addType.Type = model.Type;
                    addType.Description = model.Description;
                    addType.CreatedDate = DateTime.Now;
                    addType.CreatedBy = obj.ID;
                    addType.IsActive = true;

                    dobj.AddType.Add(addType);
                    dobj.SaveChanges();
                }

                else
                {
                    AddType addType = dobj.AddType.Where(x => x.ID == model.ID).FirstOrDefault();
                    addType.Type = model.Type;
                    addType.Description = model.Description;
                    addType.ModifiedDate = DateTime.Now;
                    addType.ModifiedBy = obj.ID;
                    addType.IsActive = true;

                    dobj.Entry(addType).State = EntityState.Modified;
                    dobj.SaveChanges();
                }
                    
            }

            return View();
        }
    }
}