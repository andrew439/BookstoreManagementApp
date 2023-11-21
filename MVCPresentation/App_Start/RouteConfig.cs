using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCPresentation
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: null,
            //    url: "Page{page}",
            //    defaults: new { Controller = "Books", action = "List" }
            //);

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(null, "",
                    new
                    {
                        controller = "Home",
                        action = "Index",
                        category = (string)null,
                        page = 1
                    }
                );

            routes.MapRoute(null, "Page{page}",
                    new { controller = "Books", action = "List", category = (string)null },
                    new { page = @"\d+" }
                );

            routes.MapRoute(null, "{category}",
                    new { controller = "Books", action = "List", page = 1 }
                );

            routes.MapRoute(null, "{category}/Page{page}",
                    new { controller = "Books", action = "List" },
                    new { page = @"\d+" }
                );

            routes.MapRoute(null, "{controller}/{action}");

        }
    }
}
