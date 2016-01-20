using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FFSolution.Models;

namespace FFSolution.Controllers
{
    public partial class SalesManController : Controller
    {
        [HttpGet]
        // GET: /SalesMan/Details
        public ActionResult DetailsForSalesMan()
        {
            // sales man ID accosiated with this login
            SalesMan salesman = db.SalesMan.Where(x => x.UserName == this.HttpContext.User.Identity.Name).FirstOrDefault();
            if (salesman == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View("Details",salesman);
        }

    }
}
