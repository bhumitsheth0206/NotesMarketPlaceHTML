using NotesMarketPlace.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class dashboard
    {
        public  NoteDetails NoteDetails { get; set; }

        public  ReferenceData ReferenceData { get; set; }

        public  AddCategory AddCategory { get; set; }
    }
}