using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcWebRole.Controllers.Administration
{
    public class AdminMainController : Controller
    {

        public ActionResult Index()
        {
            return View("~/Views/Administration/AdminMain.cshtml");
        }

    }
}
