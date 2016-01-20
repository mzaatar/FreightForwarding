using FFSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFSolution.Controllers
{
    public partial class CustomerController : Controller
    {
        //zaatar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateByNameOnly(string cname)
        {
            Customer c = null;
            try
            {
                c = new Customer()
                {
                    CustomerName = cname
                };
                db.Customer.Add(c);
                db.SaveChanges();
            }
            catch
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json(c, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        // GET: /Tran/DetailsByBLNumber/5
        public ActionResult DetailsByCustomerName(string cname)
        {
            if (cname == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            var x = db.Customer.Where(c => c.CustomerName.ToLower().Trim() == cname.ToLower().Trim()).FirstOrDefault();
            if (x == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            return Json(x.CustomerID, JsonRequestBehavior.AllowGet);
        }
	}
}