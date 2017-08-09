using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNotesMVC.Models
{
    public class Section
    {
        public string Id { get; set; }
        public string Name { get; set; }       
        public List<LinkedArticle> LinkedArticles { get; set; }
        
        public Section()
        {
            this.LinkedArticles = new List<LinkedArticle>();
        }
    }
}