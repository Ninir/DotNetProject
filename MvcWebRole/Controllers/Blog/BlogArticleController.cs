using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqlDatabase.Model;

namespace MvcWebRole.Controllers.Blog
{
    public class BlogArticleController : BaseController
    {
        public BlogArticleController() : base(true) { }

        public ActionResult Index()
        {
            List<Article> FrontPageArticles = _database.GetHighlightedArticles();
            List<Article> SomeArticles = (from a in _database.Articles where a.Title.StartsWith("A") select a).ToList();
            List<Article> SameArticles = _database.Articles.Where(a => a.Title.StartsWith("A")).ToList();

            List<Comment> AllComments = new List<Comment>();
            SomeArticles.ForEach(a => AllComments.AddRange(a.Comments));


            return View("~/Views/Blog/Index.cshtml");
        }

        public ActionResult Details()
        {
            return View("~/Views/Blog/Details.cshtml");
        }
    }
}
