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

            return View("~/Views/Blog/Index.cshtml");
        }

        public ActionResult Details()
        {
            return View("~/Views/Blog/Details.cshtml");
        }
    }
}
