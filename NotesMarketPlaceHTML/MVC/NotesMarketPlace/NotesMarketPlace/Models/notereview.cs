using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class notereview
    {
        public int ID { get; set; }
        public int NoteID { get; set; }
        public int AgainstDownloadID { get; set; }
        public int ReviewedByID { get; set; }
        public decimal Rate { get; set; }
        [Required(ErrorMessage ="This field is required")]
        public string Comment { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual Downloads Downloads { get; set; }
        public virtual NoteDetails NoteDetails { get; set; }
        public virtual Users Users { get; set; }

    }
}