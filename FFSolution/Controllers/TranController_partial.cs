using FFSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace FFSolution.Controllers
{
    public partial class TranController : Controller
    {
        [HttpGet]
        // GET: /Tran/DetailsByBLNumber/5
        public ActionResult DetailsByBLNumber(string blnumber)
        {
            if (blnumber == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            Tran tran = db.Tran.Where(s => s.BLNo == blnumber).FirstOrDefault();
            if (tran == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            return Json(tran.TranID, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult IndexBySalesManID()
        {
            SalesMan salesman = db.SalesMan.Where(x => x.UserName == this.HttpContext.User.Identity.Name).FirstOrDefault();
            if (salesman == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tran = db.Tran.Where(x => x.SalesManID.Value == salesman.SalesManID)
                 .Include(t => t.Customer)
                .Include(t => t.Customer1);
            return View("Index",tran.ToList());
        }


        [HttpGet]
        public ActionResult GetPendingTrans()
        {
            var WF = new BusinessLogic.FFWorkFlow();
            IEnumerable<KeyValuePair<int, string>> trans = new List<KeyValuePair<int,string>>();


            if (User.IsInRole("administrators") || User.IsInRole("sales"))
                trans = trans.Union(WF.GetTranForSales());

            if (User.IsInRole("administrators") || User.IsInRole("operation"))
                trans = trans.Union(WF.GetTranForOperations());

            if (User.IsInRole("administrators") || User.IsInRole("accounting"))
                trans = trans.Union(WF.GetTranForAccounting());

            if (User.IsInRole("administrators") || User.IsInRole("customersservice") || User.IsInRole("accounting"))
                trans = trans.Union(WF.GetTranToBePaid());

            if (User.IsInRole("administrators") || User.IsInRole("customersservice") || User.IsInRole("operation"))
                trans = trans.Union(WF.GetTranETA());

            if (trans == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            return Json(trans, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult FFWFMoveToNextStep(int tranID, int statusFrom, int statusTo )
        {
            var WF = new BusinessLogic.FFWorkFlow();
            var result = WF.UpdateTranStatus(tranID, statusFrom, statusTo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
	}
}