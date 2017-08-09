using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Text;

namespace MyNotesMVC.Models
{
   
    public class Article
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public List<LinkedArticle> LinkedArticles { get; set; }
        public static XElement articles;

        public Article(string id)
        {
            this.LinkedArticles = new List<LinkedArticle>();
            this.InitializeArticle(id);
        }

        static Article()
        {
            articles= XDocument.Load(@"C:\Users\Amandeep Singh\documents\visual studio 2017\Projects\MyNotesMVC\MyNotesMVC\Xml\Articles.xml").Element("Articles");
        }

        private void InitializeArticle(string id)
        {
           
            XElement article = articles.Elements("Article").Where(e => e.Attribute("Id").Value == id).SingleOrDefault();
            if (article == null) return;
            this.Id = article.Attribute("Id").Value;
            this.Name = article.Element("Name").Value;
            this.Image = article.Element("Image").Value;
            this.Description = article.Element("Description").Value;

            // Replace the <a> element in the content with required content.
            this.Content = article.Element("Content").ToString(); // Returns the content including <content> tags and html.
            this.Content = getContent(this.Content);
            foreach (var link in article.Element("LinkedArticles").Elements("LinkedArticle"))
            {
                LinkedArticle linkedArticle = new LinkedArticle();
                linkedArticle.Id = link.Value;
                XElement linkArticle = articles.Elements("Article").Where(e => e.Attribute("Id").Value == linkedArticle.Id).SingleOrDefault();
                linkedArticle.Name = linkArticle.Element("Name").Value;
                
                this.LinkedArticles.Add(linkedArticle);
            }
        }

        private static string getContent(string contentString)
        {
            // Remove <content> tags.
            int startIndex = contentString.IndexOf("<p>");
            if (startIndex == -1) return contentString;
            int endIndex = contentString.LastIndexOf("</p>");
            if (endIndex == -1) return contentString;
            contentString = contentString.Substring(startIndex, endIndex - startIndex + 4);

            int mainIndex = 0;
            while(true)
            {
                mainIndex = contentString.IndexOf("id=", mainIndex);
                if (mainIndex == -1) break;
                int firstQuote = contentString.IndexOf("\"", mainIndex);
                int secondQuote = contentString.IndexOf("\"", firstQuote + 1);
                string articleId = contentString.Substring(firstQuote + 1, secondQuote - firstQuote - 1);
                contentString = contentString.Insert(secondQuote + 1, getArticleInfo(articleId));
                mainIndex++;
            }

            return contentString;
        }

        private static string getArticleInfo(string articleId="Article1")
        {
           
            StringBuilder mainString = new StringBuilder(" title=\"<span style = 'font-size: 16px'>");
            XElement article = articles.Elements("Article").Where(e => e.Attribute("Id").Value == articleId).SingleOrDefault();
            string articleName = article.Element("Name").Value;
            mainString.Append(articleName);
            mainString.Append("</span>\" data-content = \"<span style='font-size: 16px'>");
            string articleDescription = article.Element("Description").Value;
            mainString.Append(articleDescription);
            mainString.Append("</span> <img src=\'");
            string articleImage = article.Element("Image").Value;
            mainString.Append(articleImage);
            mainString.Append("\' style=\'width:100px;height:100px'\"");

            return mainString.ToString();
        }
    }

   
}