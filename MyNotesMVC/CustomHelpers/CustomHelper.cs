using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyNotesMVC.Models;
using System.Text;

namespace MyNotesMVC.CustomHelpers
{
    public class CustomHelper
    {
        static private string m_classBookView, m_classRelatedView, m_classArticle;

        static CustomHelper()
        {
            m_classBookView = "list-group-item list-group-item-info linked-article";
        }

       

        public static IHtmlString ActionLinkHidden(string id)
        {
            string idString = "hidden" + id;
            TagBuilder tb = new TagBuilder("a");
            tb.MergeAttribute("class", "list-group-item list-group-item-danger");
            tb.MergeAttribute("hidden", null);
            tb.GenerateId(idString);
            
            tb.SetInnerText("Add Chapter");
           
            return new MvcHtmlString(tb.ToString());
        }

        //public static IHtmlString ActionLinkList(List<LinkedArticle>linkedArticles, string classId)
        //{
        //    StringBuilder strBuilder = new StringBuilder();
        //    foreach (var linkedArticle in linkedArticles){
        //        TagBuilder tb = new TagBuilder("a");
        //        tb.MergeAttribute("class", $"list-group-item list-group-item-info linked-article {0} " extraClasscla );
        //        tb.MergeAttribute("href", "");
        //        tb.GenerateId(id);
        //        tb.SetInnerText(innerText);
        //    }

            

        //    return new MvcHtmlString(tb.ToString());
        //}
    }
}