using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqlDatabase;
using SqlDatabase.Model;

namespace MvcWebRole.Controllers
{
    public class testController : Controller
    {
        //
        // GET: /test/

        public ActionResult Index()
        {
            DatabaseInterface DB = new DatabaseInterface();
            DB.GetEntities().AddToUsers(new User() {
                Mail = "mail@server.domain",
                Password = "P@ssw0rd",
                Username = "Username",
                CreatedAt = DateTime.Now
            });
            DB.GetEntities().SaveChanges();
            DB.GetEntities().AddToArticles(new Article() {
                Title = "Owned Gauthier!",
                Content = "This is an article",
                CreatedAt = DateTime.Now,
                Published = true,
                AllowComments = true,
                Highlight = true,
                Slug = "owned-gauthier-",
                User = (from e in DB.GetEntities().Users select e).FirstOrDefault()
            });
            DB.GetEntities().SaveChanges();
            ViewBag.Articles = DB.GetHighlightedArticles();
            return View();
        }

    }
}
