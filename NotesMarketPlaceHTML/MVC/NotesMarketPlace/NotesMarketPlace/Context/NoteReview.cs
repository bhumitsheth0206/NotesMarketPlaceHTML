//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NotesMarketPlace.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class NoteReview
    {
        public int ID { get; set; }
        public int NoteID { get; set; }
        public int AgainstDownloadID { get; set; }
        public int ReviewedByID { get; set; }
        public decimal Rate { get; set; }
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
