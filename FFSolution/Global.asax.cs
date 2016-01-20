using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FFSolution
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Application_EndRequest(Object sender, EventArgs e)
        {
            //HttpApplication context = (HttpApplication)sender;
            //if (context.Response.StatusCode == 302 &&
            //    context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            //{
            //    context.Response.ClearContent();
            //    context.Response.Clear();
            //    context.Response.StatusCode = 401;
            //}
            ////HttpApplication context = (HttpApplication)sender;
            ////context.Response.SuppressFormsAuthenticationRedirect = false;
            ////if (HttpContext.Current.Response.Status.StartsWith("302"))
            ////{
            ////    HttpContext.Current.Response.ClearContent();
            ////    Response.Redirect("~/Views/Error/error401.html");
            ////}
        }
    }
}
