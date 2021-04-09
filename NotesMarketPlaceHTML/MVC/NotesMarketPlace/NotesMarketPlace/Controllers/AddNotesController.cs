using NotesMarketPlace.Context;
using NotesMarketPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;

namespace Notes_MarketPlace.Controllers
{

    [Authorize(Roles = "member")]
    public class AddNotesController : Controller
    {
        NotesMarketPlaceEntities dobj = new NotesMarketPlaceEntities();

        [Route("Addnotes")]
        // GET: AddNotes

        public ActionResult AddNotes(int? ID)
        {
            // Assigning values to dropdown from database

            var country = dobj.AddCountry.Where(x => x.IsActive == true).ToList();
            var category = dobj.AddCategory.Where(x => x.IsActive == true).ToList();
            var type = dobj.AddType.Where(x => x.IsActive == true).ToList();
            var dropdownitems = new addnotes()
            {
                Country = country,
                Category = category,
                Type = type
            };

            if (ID != null)
            {
                dropdownitems = dobj.NoteDetails.Where(x => x.ID == ID).Select(x =>
                  new addnotes()
                  {
                      ID = x.ID,
                      NoteTitle = x.NoteTitle,
                      Status = x.Status,
                      CategoryID = x.CategoryID,
                      TypeID = x.TypeID,
                      NumberOfPages = x.NumberOfPages,
                      Description = x.Description,
                      CountryID = x.CountryID,
                      InstitutionName = x.InstitutionName,
                      IsPaid = x.IsPaid,
                      Course = x.Course,
                      CourseCode = x.CourseCode,
                      Professor = x.Professor,
                      SellPrice = x.SellPrice
                  }
                ).FirstOrDefault();
            }

            dropdownitems.Country = country;
            dropdownitems.Category = category;
            dropdownitems.Type = type;


            return View(dropdownitems);
        }

        [HttpPost]
        [Route("Addnotes")]
        public ActionResult AddNotes(addnotes model, string addnote_btn)
        {

            if (ModelState.IsValid)
            {
                if ((model.IsPaid == true) && (model.NotesPreview == null))
                {
                    ModelState.AddModelError("NotesPreview", "NotesPreview Required");

                    var country = dobj.AddCountry.Where(x => x.IsActive == true).ToList();
                    var category = dobj.AddCategory.Where(x => x.IsActive == true).ToList();
                    var type = dobj.AddType.Where(x => x.IsActive == true).ToList();
                    var dropdownitems = new addnotes()
                    {
                        Country = country,
                        Category = category,
                        Type = type
                    };

                    return View(dropdownitems);


                }

                var emailid = User.Identity.Name.ToString();
                Users obj = dobj.Users.Where(x => x.EmailID == emailid).FirstOrDefault();
                string path = Path.Combine(Server.MapPath("~/Members"), obj.ID.ToString());

                //Checking for directory

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (model.ID > 0)
                {
                    NoteDetails details = dobj.NoteDetails.Where(x => x.ID == model.ID).FirstOrDefault();
                    if (details.Status == 5)
                    {
                        details.Status = 1;
                        dobj.SaveChanges();

                        model.ID = 0;
                    }
                }

                var statusvalue = "Draft";
                ReferenceData refdata = dobj.ReferenceData.Where(x => x.RefCategory == "NotesStatus" && x.Datavalue == statusvalue && x.IsActive == true).FirstOrDefault();

                var statusvalue_new = "Submitted For Review";
                ReferenceData refdata_new = dobj.ReferenceData.Where(x => x.RefCategory == "NotesStatus" && x.Datavalue == statusvalue_new && x.IsActive == true).FirstOrDefault();

                //Saving into database
                if (model.ID == 0)
                {
                    NoteDetails ndetails = new NoteDetails();
                    ndetails.ID = model.ID;
                    ndetails.UID = obj.ID;
                    if (addnote_btn == "1")
                    {
                        ndetails.Status = refdata.ID;
                    }
                    else
                    {
                        ndetails.Status = refdata_new.ID;

                        ManageSystemConfiguration manage = dobj.ManageSystemConfiguration.FirstOrDefault();

                        var fromEmail = new MailAddress(manage.SupportEmail, "Notes-MarketPlace"); //Email of Company
                        var toEmail = new MailAddress(manage.EmailAddress_es); //Email of admin
                        var fromEmailPassword = "********"; // Replace with actual password
                        string subject = obj.FirstName + " - sent his note for review";

                        string body = "Hello Admins," +
                             "<br/><br/>We want to inform you that," + obj.FirstName + " sent his note" +
                             model.NoteTitle + " for review. Please look at the notes and take required actions. " +
                             "<br/><br/>Regards," +
                             "<br/>Notes MarketPlace";

                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
                        };

                        using (var message = new MailMessage(fromEmail, toEmail)
                        {
                            Subject = subject,
                            Body = body,
                            IsBodyHtml = true
                        })
                            smtp.Send(message);
                    }
                    ndetails.NoteTitle = model.NoteTitle;
                    ndetails.CategoryID = model.CategoryID;
                    ndetails.Description = model.Description;
                    ndetails.IsPaid = model.IsPaid;
                    ndetails.TypeID = model.TypeID;
                    ndetails.NumberOfPages = model.NumberOfPages;
                    ndetails.InstitutionName = model.InstitutionName;
                    ndetails.CountryID = model.CountryID;
                    ndetails.Course = model.Course;
                    ndetails.CourseCode = model.CourseCode;
                    ndetails.Professor = model.Professor;
                    ndetails.SellPrice = model.SellPrice;
                    ndetails.CreatedDate = DateTime.Now;
                    ndetails.IsActive = true;
                    ndetails.CreatedBy = obj.ID;
                    dobj.NoteDetails.Add(ndetails);
                    dobj.SaveChanges();


                    //Acquiring NoteID

                    var NoteID = ndetails.ID;
                    string finalpath = Path.Combine(Server.MapPath("~/Members/" + obj.ID), NoteID.ToString());

                    if (!Directory.Exists(finalpath))
                    {
                        Directory.CreateDirectory(finalpath);
                    }

                    //Display Picture
                    if (model.DisplayPicture != null && model.DisplayPicture.ContentLength > 0)
                    {
                        var displayimagename = "DP_" + DateTime.Now.ToString("dd-MM-yy").Replace(':', '-').Replace(' ', '_') + Path.GetExtension(model.DisplayPicture.FileName);
                        var ImageSavePath = Path.Combine(Server.MapPath("/Members/" + obj.ID + "/" + ndetails.ID + "/") + displayimagename);
                        model.DisplayPicture.SaveAs(ImageSavePath);
                        ndetails.DisplayPicture = Path.Combine(("/Members/" + obj.ID + "/" + ndetails.ID + "/"), displayimagename);
                        dobj.SaveChanges();
                    }

                    //Notes Preview
                    if (model.NotesPreview != null && model.NotesPreview.ContentLength > 0)
                    {
                        var notespreviewname = "Preview_" + DateTime.Now.ToString("dd-MM-yy").Replace(':', '-').Replace(' ', '_') + Path.GetExtension(model.NotesPreview.FileName);
                        var PreviewSavePath = Path.Combine(Server.MapPath("/Members/" + obj.ID + "/" + ndetails.ID + "/") + notespreviewname);
                        model.NotesPreview.SaveAs(PreviewSavePath);
                        ndetails.NotesPreview = Path.Combine(("/Members/" + obj.ID + "/" + ndetails.ID + "/"), notespreviewname);
                        dobj.SaveChanges();
                    }

                    //Upload Notes
                    //Seller Notes Attachment
                    SellerNotesAttachments sellnotesattach = new SellerNotesAttachments();
                    sellnotesattach.NoteID = NoteID;
                    sellnotesattach.IsActive = true;
                    sellnotesattach.CreatedBy = obj.ID;
                    sellnotesattach.CreatedDate = DateTime.Now;

                    string storeUploadPath = Path.Combine(finalpath, "Attachment");

                    if (!Directory.Exists(storeUploadPath))
                    {
                        Directory.CreateDirectory(storeUploadPath);
                    }

                    //Upload Notes
                    int count = 1;
                    var uploadfilepath = "";
                    var uploadfilename = "";

                    foreach (HttpPostedFileBase file in model.UploadNotes)
                    {
                        string _FileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        _FileName = "Attachment" + count + "_" + DateTime.Now.ToString("dd-MM-yy") + extension;
                        string final = Path.Combine(storeUploadPath, _FileName);
                        file.SaveAs(final);
                        uploadfilename += _FileName + ";";
                        uploadfilepath += Path.Combine(("/Members/" + obj.ID + "/" + NoteID + "/Attachment/"), _FileName);
                        count++;

                    }

                    sellnotesattach.FileName = uploadfilename;
                    sellnotesattach.FilePath = uploadfilepath;
                    dobj.SellerNotesAttachments.Add(sellnotesattach);
                    dobj.SaveChanges();
                    return RedirectToAction("Dashboard", "Dashboard");

                }

                //Editing Notes
                else
                {
                    NoteDetails ndetails = dobj.NoteDetails.Where(x => x.ID == model.ID).FirstOrDefault();
                    if (addnote_btn == "1")
                    {
                        ndetails.Status = refdata.ID;
                    }
                    else
                    {
                        ndetails.Status = refdata_new.ID;

                        ManageSystemConfiguration manage = dobj.ManageSystemConfiguration.FirstOrDefault();

                        var fromEmail = new MailAddress(manage.SupportEmail, "Notes-MarketPlace"); //Email of Company
                        var toEmail = new MailAddress(manage.EmailAddress_es); //Email of admin
                        var fromEmailPassword = "*******"; // Replace with actual password
                        string subject = obj.FirstName + " - sent his note for review";

                        string body = "Hello Admins," +
                             "<br/><br/>We want to inform you that," + obj.FirstName + " sent his note" +
                             model.NoteTitle + " for review. Please look at the notes and take required actions. " +
                             "<br/><br/>Regards," +
                             "<br/>Notes MarketPlace";

                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
                        };

                        using (var message = new MailMessage(fromEmail, toEmail)
                        {
                            Subject = subject,
                            Body = body,
                            IsBodyHtml = true
                        })
                            smtp.Send(message);
                    }
                    ndetails.NoteTitle = model.NoteTitle;
                    ndetails.CategoryID = model.CategoryID;
                    ndetails.Description = model.Description;
                    ndetails.IsPaid = model.IsPaid;
                    ndetails.TypeID = model.TypeID;
                    ndetails.NumberOfPages = model.NumberOfPages;
                    ndetails.InstitutionName = model.InstitutionName;
                    ndetails.CountryID = model.CountryID;
                    ndetails.Course = model.Course;
                    ndetails.CourseCode = model.CourseCode;
                    ndetails.Professor = model.Professor;
                    ndetails.SellPrice = model.SellPrice;
                    ndetails.ModifiedDate = DateTime.Now;
                    ndetails.IsActive = true;
                    ndetails.ModifiedBy = obj.ID;
                    dobj.Entry(ndetails).State = EntityState.Modified;
                    dobj.SaveChanges();

                    //Acquiring NoteID

                    var NoteID = ndetails.ID;
                    string finalpath = Path.Combine(Server.MapPath("~/Members/" + obj.ID), NoteID.ToString());
                    Directory.Delete(finalpath, true);

                    if (!Directory.Exists(finalpath))
                    {
                        Directory.CreateDirectory(finalpath);
                    }

                    //Display Picture
                    if (model.DisplayPicture != null && model.DisplayPicture.ContentLength > 0)
                    {
                        var displayimagename = "DP_" + DateTime.Now.ToString("dd-MM-yy").Replace(':', '-').Replace(' ', '_') + Path.GetExtension(model.DisplayPicture.FileName);
                        var ImageSavePath = Path.Combine(Server.MapPath("/Members/" + obj.ID + "/" + ndetails.ID + "/") + displayimagename);
                        model.DisplayPicture.SaveAs(ImageSavePath);
                        ndetails.DisplayPicture = Path.Combine(("/Members/" + obj.ID + "/" + ndetails.ID + "/"), displayimagename);
                        dobj.SaveChanges();
                    }

                    //Notes Preview
                    if (model.NotesPreview != null && model.NotesPreview.ContentLength > 0)
                    {
                        var notespreviewname = "Preview_" + DateTime.Now.ToString("dd-MM-yy").Replace(':', '-').Replace(' ', '_') + Path.GetExtension(model.NotesPreview.FileName);
                        var PreviewSavePath = Path.Combine(Server.MapPath("/Members/" + obj.ID + "/" + ndetails.ID + "/") + notespreviewname);
                        model.NotesPreview.SaveAs(PreviewSavePath);
                        ndetails.NotesPreview = Path.Combine(("/Members/" + obj.ID + "/" + ndetails.ID + "/"), notespreviewname);
                        dobj.SaveChanges();
                    }

                    //Upload Notes
                    //Seller Notes Attachment
                    SellerNotesAttachments sellnotesattach = dobj.SellerNotesAttachments.Where(x => x.NoteID == NoteID).FirstOrDefault();
                    sellnotesattach.ModifiedDate = DateTime.Now;

                    string storeUploadPath = Path.Combine(finalpath, "Attachment");

                    if (!Directory.Exists(storeUploadPath))
                    {
                        Directory.CreateDirectory(storeUploadPath);
                    }

                    //Upload Notes
                    int count = 1;
                    var uploadfilepath = "";
                    var uploadfilename = "";

                    foreach (HttpPostedFileBase file in model.UploadNotes)
                    {
                        string _FileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        _FileName = "Attachment" + count + "_" + DateTime.Now.ToString("dd-MM-yy") + extension;
                        string final = Path.Combine(storeUploadPath, _FileName);
                        file.SaveAs(final);
                        uploadfilename += _FileName + ";";
                        uploadfilepath += Path.Combine(("/Members/" + obj.ID + "/" + NoteID + "/Attachment/"), _FileName);
                        count++;

                    }

                    sellnotesattach.FileName = uploadfilename;
                    sellnotesattach.FilePath = uploadfilepath;
                    dobj.Entry(sellnotesattach).State = EntityState.Modified;
                    dobj.SaveChanges();
                    return RedirectToAction("Dashboard", "Dashboard");
                }

            }
            return RedirectToAction("Dashboard", "Dashboard");
        }


    }
}