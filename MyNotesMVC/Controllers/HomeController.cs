using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyNotesMVC.Models;
using System.Xml.Linq;

namespace MyNotesMVC.Controllers
{
    
    public class HomeController : Controller
    {
        
        public PartialViewResult Index()
        {
            Book book = new Book("GsPaper1");
            
            return PartialView("~/Views/Home/Partial Views/_EditBook.cshtml", book);
        }

        public ActionResult Notes()
        {

            ViewModel vm = new ViewModel();
            vm.Article = new Article("Article1");
            vm.Book = new Book("GsPaper1");

            return View(vm);
        }

        public ActionResult Edit()
        {
            return View();
        }

        public PartialViewResult GetArticleWithLinks(string articleId)
        {
            Article article = new Article(articleId);
            return PartialView("~/Views/Home/PartialViews/_ArticleWithLinks.cshtml", article);
        }

        public PartialViewResult GetArticle(string articleId)
        {
            Article article = new Article(articleId);
            return PartialView("~/Views/Home/PartialViews/_Article.cshtml", article);
        }
    }
}