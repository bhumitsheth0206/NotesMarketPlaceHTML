using NotesMarketPlace.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class notesearch
    {
        public List<AddCategory> Category { get; set; }

        public List<AddCountry> Country { get; set; }

        public List<AddType> Type { get; set; }

        public List<string> University { get; set; }

        public List<string> Courses { get; set; }

        public IPagedList<NoteDetails> Note { get; set; }
    }
}