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
    public class ProfitController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        [HttpGet]
        public ActionResult CustomerProfitDetails(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfitForCustomerViewModel p = new ProfitForCustomerViewModel(id.Value);

            return View("CustomerProfitDetails", p);
        }
        //end Zaatar



        [HttpGet]
        public ActionResult SearchForTran()
        {
            return View("SearchForTran");
        }

        [HttpGet]
        
        public ActionResult GetTranProfit(string fromDate, string toDate)
        {
            DateTime from = DateTime.Parse(fromDate);
            DateTime to = DateTime.Parse(toDate);

            var trans = db.Tran.Where(s => s.BookingDate > from && s.BookingDate < to)
                .ToList();

            ProfitForTranListViewModel p =  new ProfitForTranListViewModel(trans);

            ViewBag.fromDate = from;
            ViewBag.toDate = to;
            return PartialView("TranProfitDetailsList", p);
        }

        //end Zaatar
	}
}