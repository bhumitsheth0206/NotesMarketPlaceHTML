using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Models
{
    public class addnotes
    {
        public int ID { get; set; }

        public int UID { get; set; }

        public string Status { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int CategoryID { get; set; }

        public Nullable<int> TypeID { get; set; }

        public Nullable<int> CountryID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string NoteTitle { get; set; }
        
        public HttpPostedFileBase DisplayPicture { get; set; }
        
        public string NumberOfPages { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }

        public string University { get; set; }
        
        public string InstitutionName { get; set; }
        
        public string Course { get; set; }
        
        public string CourseCode { get; set; }
        
        public string Professor { get; set; }
        
        [Required(ErrorMessage ="This field is required")]
        public bool IsPaid { get; set; }
        
        public decimal SellPrice { get; set; }
        
        public HttpPostedFileBase NotesPreview { get; set; }
        
        public string Action { get; set; }
        
        public string Remark { get; set; }
        
        public Nullable<System.DateTime> CreatedDate { get; set; }
        
        public Nullable<int> CreatedBy { get; set; }
        
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        
        public Nullable<int> ModifiedBy { get; set; }
        
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public HttpPostedFileBase[] UploadNotes { get; set; }

        public List<AddCategory> Category { get; set; }

        public List<AddCountry> Country { get; set; }

        public List<AddType> Type { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

    }
}