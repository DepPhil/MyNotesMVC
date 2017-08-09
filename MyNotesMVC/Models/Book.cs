using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MyNotesMVC.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Chapter> Chapters { get; set; }

        private static XElement books;
        private static XElement articles;

        public Book(string name)
        {
            this.Chapters = new List<Chapter>();
            this.InitializeBook(name);
        }

        static Book()
        {
            books = XDocument.Load(@"C:\Users\Amandeep Singh\documents\visual studio 2017\Projects\MyNotesMVC\MyNotesMVC\Xml\Books.xml").Element("Books");
            articles = XDocument.Load(@"C:\Users\Amandeep Singh\documents\visual studio 2017\Projects\MyNotesMVC\MyNotesMVC\Xml\Articles.xml").Element("Articles");
        }

        private void InitializeBook(string name)
        {
            XElement book = books.Elements("Book").Where(e => e.Attribute("Name").Value == name).FirstOrDefault();
            if (book == null) return;
            this.Id = book.Attribute("Id").Value;
            this.Name = name;
            foreach (var ch in book.Elements("Chapter"))
            {
                Chapter chapter = new Chapter();
                chapter.Id = ch.Attribute("Id").Value;
                chapter.Name = ch.Attribute("Name").Value;
                foreach (var sec in ch.Elements("Section"))
                {
                    Section section = new Section();
                    section.Id = sec.Attribute("Id").Value;
                    section.Name = sec.Attribute("Name").Value;
                    foreach (var link in sec.Elements("LinkedArticle"))
                    {
                        LinkedArticle linkedArticle = new LinkedArticle();
                        linkedArticle.Id = link.Value;
                        XElement article = articles.Elements("Article").Where(e => e.Attribute("Id").Value == linkedArticle.Id).SingleOrDefault();
                        linkedArticle.Name = article.Element("Name").Value;
                        section.LinkedArticles.Add(linkedArticle);
                    }
                    chapter.Sections.Add(section);
                }

                this.Chapters.Add(chapter);
            }
        }


    }
}