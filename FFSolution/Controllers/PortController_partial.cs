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
    public partial class PortController : Controller
    {
        //// GET: /Port/Details/5
        // [HttpGet]
        //public ActionResult ListByCountryID(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    List<Port> ports = db.Port.Where(x => x.CountryID == id).ToList();
        //    if (ports == null)
        //    {
        //        return Json("0", JsonRequestBehavior.AllowGet);
        //    }

        //    ViewBag.PortList = ports;
        //    return Json(ports, JsonRequestBehavior.AllowGet);
        //}


        //zaatar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateByNameOnly(string pname, int pcountry)
        {
            Port p = null;
            try
            {
                p = new Port()
                {
                    PortName = pname,
                    CountryID = pcountry
                };
                db.Port.Add(p);
                db.SaveChanges();
            }
            catch
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json(p, JsonRequestBehavior.AllowGet);
        }
      
    }
}
