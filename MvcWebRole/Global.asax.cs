using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcWebRole
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /**
             * Blog Routes
             */
            routes.MapRoute("Blog_Main", "Blog", new { controller = "BlogArticle", action = "Index" });
            routes.MapRoute("Blog_Article_Details", "Blog/Article/{articleId}", new { controller = "BlogArticle", action = "Details" }, new { articleId = @"\d+" });

            /**
             * Admin Routes
             */
            routes.MapRoute("Admin", "Admin/", new { controller = "AdminMain", action = "Index" });

            /**
             * Default Routes
             */
            routes.MapRoute("Default", // Route name 
                            "{controller}/{action}/{id}", // URL with parameters 
                            new { controller = "BlogArticle", action = "Index", id = "" } // Parameter defaults 
            ); 
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}