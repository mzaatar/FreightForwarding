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
    public partial class TranContainerController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();


        // GET: /TranContainer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranContainer trancontainer = db.TranContainer.Find(id);
            if (trancontainer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShippingTypeID = new SelectList(db.ShippingType, "ShippingTypeID", "ShippingTypeName", trancontainer.ShippingTypeID);
            //ViewBag.TranID = new SelectList(db.Tran, "TranID", "TranID", trancontainer.TranID);
            ViewBag.LineShippingChargeCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancontainer.LineShippingChargeCurrencyID);
            ViewBag.LineShippingChargeSellingCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancontainer.LineShippingChargeSellingCurrencyID);

            var tran = db.Tran.FirstOrDefault(t => t.TranID == trancontainer.TranID);
            if (tran == null)
            {
                ViewBag.ActiveTran = null;
            }
            else
            {
                ViewBag.ActiveTran = tran;
            }

            return View(trancontainer);
        }

        // POST: /TranContainer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TranContainerID,Updated,Updator,TranID,ShippingTypeID,Count,LineShippingCharge,LineShippingChargeCurrencyID,LineShippingChargeSelling,LineShippingChargeSellingCurrencyID,LineShippingChargeTotal,LineShippingChargeSellingTotal")] TranContainer trancontainer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trancontainer).State = EntityState.Modified;
                db.SaveChanges();
                Calculations.CalcTran(trancontainer.TranID, 6);
                return RedirectToAction("Details", "Tran", new { id = trancontainer.TranID });
            }
            ViewBag.ShippingTypeID = new SelectList(db.ShippingType, "ShippingTypeID", "ShippingTypeName", trancontainer.ShippingTypeID);
            ViewBag.TranID = new SelectList(db.Tran, "TranID", "TranID", trancontainer.TranID);
            ViewBag.LineShippingChargeCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancontainer.LineShippingChargeCurrencyID);
            ViewBag.LineShippingChargeSellingCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancontainer.LineShippingChargeSellingCurrencyID);

            var tran = db.Tran.FirstOrDefault(t => t.TranID == trancontainer.TranID);
            if (tran == null)
            {
                ViewBag.ActiveTran = null;
            }
            else
            {
                ViewBag.ActiveTran = tran;
            }
            return View(trancontainer);
        }

        // GET: /TranContainer/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranContainer trancontainer = await db.TranContainer.FindAsync(id);
            if (trancontainer == null)
            {
                return HttpNotFound();
            }
            return View(trancontainer);
        }

        // POST: /TranContainer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TranContainer trancontainer = await db.TranContainer.FindAsync(id);
            db.TranContainer.Remove(trancontainer);
            Calculations.CalcTran(trancontainer.TranID, 6);
            await db.SaveChangesAsync();
            Calculations.CalcTran(trancontainer.TranID, 6);
            return RedirectToAction("Details", "Tran", new { id = trancontainer.TranID });
        }


        // GET: /TranContainer/Details/5
        public async Task<ActionResult> DetailsByTranID(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<TranContainer> trancontainers = await db.TranContainer.Where(t => t.TranID == id.Value).ToListAsync();
            if (trancontainers == null)
            {
                return HttpNotFound();
            }
            ViewBag.TranID = id.Value;
            return View("Details", trancontainers);
        }

        // GET: /TranContainer/Details/5
        public ActionResult CreateByTranID(int? id)
        {
            var activeIDs = db.TranContainer.Where(t => t.TranID == id.Value).Select(x => x.ShippingTypeID);

            ViewBag.ShippingTypeID = new SelectList(db.ShippingType.Where(x => !activeIDs.Contains(x.ShippingTypeID))
                , "ShippingTypeID", "ShippingTypeName");
            ViewBag.TranID = new SelectList(db.Tran, "TranID", "BLNo");
            var tran = db.Tran.FirstOrDefault(t => t.TranID == id.Value);
            if (tran == null)
            {
                ViewBag.ActiveTran = null;
            }
            else
            {
                ViewBag.ActiveTran = tran;
            }
            ViewBag.LineShippingChargeCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode");
            ViewBag.LineShippingChargeSellingCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode");
            return View();
        }


        //POST: /TranContainer/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateByTranID([Bind(Include = "TranContainerID,Updated,Updator,TranID,ShippingTypeID,Count,LineShippingCharge,LineShippingChargeCurrencyID,LineShippingChargeSelling,LineShippingChargeSellingCurrencyID,LineShippingChargeTotal,LineShippingChargeSellingTotal")] TranContainer trancontainer)
        {
            if (ModelState.IsValid)
            {
                db.TranContainer.Add(trancontainer);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");

                Calculations.CalcTran(trancontainer.TranID, 6);
                return RedirectToAction("Details", "Tran", new { id = trancontainer.TranID });
            }

            ViewBag.ShippingTypeID = new SelectList(db.ShippingType, "ShippingTypeID", "ShippingTypeName", trancontainer.ShippingTypeID);
            ViewBag.TranID = new SelectList(db.Tran, "TranID", "TranID", trancontainer.TranID);
            ViewBag.LineShippingChargeCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancontainer.LineShippingChargeCurrencyID);
            ViewBag.LineShippingChargeSellingCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trancontainer.LineShippingChargeSellingCurrencyID);
            return View();
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
