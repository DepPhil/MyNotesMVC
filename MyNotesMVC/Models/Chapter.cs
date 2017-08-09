using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNotesMVC.Models
{
    public class Chapter
    {
        public string Id { get; set; }
        public string Name { get; set; }        
        public List<Section> Sections { get; set; }

        public Chapter()
        {
            this.Sections = new List<Section>();
        }
    }
}