using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFSolution.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int id = Commons.SalesManLogic.GetSalesManID(HttpContext.User.Identity.Name);
            ViewBag.SalesID = id;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}