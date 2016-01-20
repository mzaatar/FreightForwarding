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
    public partial class CommodityController : Controller
    {
        //zaatar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateByNameOnly(string cname)
        {
            Commodity c = null;
            try
            {
                c = new Commodity()
                {
                    CommodityName = cname
                };
                db.Commodity.Add(c);
                db.SaveChanges();
            }
            catch
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json(c, JsonRequestBehavior.AllowGet);
        }

    }
}
