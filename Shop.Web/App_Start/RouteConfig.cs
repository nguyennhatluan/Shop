using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });


            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { "Shop.Web.Controllers" }

            );
            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
                namespaces: new string[] { "Shop.Web.Controllers" }

            );
            routes.MapRoute(
                name: "ProductByName",
                url: "Product/GetListProductByName/{keyword}",
                defaults: new { controller = "Product", action = "GetListProductByName", keyword = UrlParameter.Optional },
                namespaces: new string[] { "Shop.Web.Controllers" }
            );
            routes.MapRoute(
                name: "ProductByTag",
                url: "Product/Tag/{tagId}",
                defaults: new { controller = "Product", action = "GetListProductByTag", tagId = UrlParameter.Optional },
                namespaces: new string[] { "Shop.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Product",
                url: "product-detail/{alias}/{id}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new string[] { "Shop.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Product Category",
                url: "product-category/{alias}/{id}",
                //day-chuyen.pc-16.html
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new string[] { "Shop.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Shop.Web.Controllers" }
            );
        }
    }
}
