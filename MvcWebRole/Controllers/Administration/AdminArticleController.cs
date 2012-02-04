using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqlDatabase;

namespace MvcWebRole.Controllers.Administration
{
    public class AdminArticleController : Controller
    {

        public ActionResult Index()
        {
            return View("~/Views/Administration/Article/Index.cshtml");
        }

        public ActionResult Add()
        {
            return View("~/Views/Administration/Article/Add.cshtml");
        }

    }
}
