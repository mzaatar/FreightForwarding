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
using FFSolution.BusinessLogic;

namespace FFSolution.Controllers
{
    public class TranChargesController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();
       
        // GET: /TranCharges/
        public  ActionResult Index()
        {
            var trancharges = db.TranCharges.
                Include(t => t.Currency).
                Include(t => t.Currency1).
                Include(t => t.Currency2).
                Include(t => t.Currency3).
                Include(t => t.Currency4).
                //Include(t => t.Currency5).
                //Include(t => t.Currency6).
                Include(t => t.Tran);
            return View( trancharges.ToList());
        }

        // GET: /TranCharges/Details/5
        public  ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranCharges trancharges =  db.TranCharges.Find(id);
           
            if (trancharges == null)
            {
                return HttpNotFound();
            }
            return View(trancharges);
        }

        // GET: /TranCharges/Edit/5
        public  ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranCharges trancharges =  db.TranCharges.Find(id);
            if (trancharges == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankTransferFeesCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancharges.BankTransferFeesCurrencyID);
            //ViewBag.LineShippingChargeCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancharges.LineShippingChargeCurrencyID);
            //ViewBag.LineShippingChargeSellingCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancharges.LineShippingChargeSellingCurrencyID);
            ViewBag.OtherFeesCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancharges.OtherFeesCurrencyID);
            ViewBag.PaidCommissionCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancharges.PaidCommissionCurrencyID);

            ViewBag.BankPercentageCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancharges.BankPercentageCurrencyID);
            ViewBag.CommissionPercentageCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancharges.CommissionPercentageCurrencyID);
            
            ViewBag.TranID = new SelectList(db.Tran, "TranID", "Updator", trancharges.TranID);
           
            return View(trancharges);
        }

        // POST: /TranCharges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TranID,Updated,Updator,CommissionPercentage,PaidCommission,PaidCommissionCurrencyID,BankPercentage,BankTransferFees,BankTransferFeesCurrencyID,OtherFees,OtherFeesCurrencyID,BankPercentageCurrencyID,CommissionPercentageCurrencyID")] TranCharges trancharges)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trancharges).State = EntityState.Modified;
                 db.SaveChanges();
                //return RedirectToAction("Index");
                 Calculations.CalcTran(trancharges.TranID, 5);
				return RedirectToAction("Details", "Tran", new { id = trancharges.TranID });
            }
            ViewBag.BankTransferFeesCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancharges.BankTransferFeesCurrencyID);
            //ViewBag.LineShippingChargeCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancharges.LineShippingChargeCurrencyID);
            //ViewBag.LineShippingChargeSellingCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancharges.LineShippingChargeSellingCurrencyID);
            ViewBag.OtherFeesCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancharges.OtherFeesCurrencyID);
            ViewBag.PaidCommissionCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancharges.PaidCommissionCurrencyID);

            ViewBag.BankPercentageCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancharges.BankPercentageCurrencyID);
            ViewBag.CommissionPercentageCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancharges.CommissionPercentageCurrencyID);

           // ViewBag.TranID = new SelectList(db.Tran, "TranID", "Updator", trancharges.TranID);
           
            return View(trancharges);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
