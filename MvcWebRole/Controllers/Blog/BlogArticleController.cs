using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcWebRole.Controllers.Blog
{
    public class BlogArticleController : Controller
    {

        public ActionResult Index()
        {
            return View("~/Views/Blog/Index.cshtml");
        }

        public ActionResult Details()
        {
            return View("~/Views/Blog/Details.cshtml");
        }
    }
}
