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
    public class TranDetailController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        // GET: /TranDetail/
        public async Task<ActionResult> Index()
        {
            var trandetail = db.TranDetail.Include(t => t.Currency).Include(t => t.Tran);
            return View(await trandetail.ToListAsync());
        }

        // GET: /TranDetail/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranDetail trandetail = await db.TranDetail.FindAsync(id);
            if (trandetail == null)
            {
                return HttpNotFound();
            }
            return View(trandetail);
        }

        // GET: /TranDetail/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranDetail trandetail = await db.TranDetail.FindAsync(id);
            if (trandetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.DemurrageCostCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trandetail.DemurrageCostCurrencyID);
            ViewBag.TranID = new SelectList(db.Tran, "TranID", "Updator", trandetail.TranID);
            return View(trandetail);
        }

        // POST: /TranDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TranID,Updated,Updator,DemurrageFreeDays,DemurrageOverDays,DemurrageCost,DemurrageCostCurrencyID,CollectDate,RemainingAmountNotes,PaymentFinished,AdditionalField1Label,AdditionalField2Label,StatusID")] TranDetail trandetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trandetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                Calculations.CalcTran(trandetail.TranID, 0);
                return RedirectToAction("Details", "Tran", new { id = trandetail.TranID });
            }
            ViewBag.DemurrageCostCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trandetail.DemurrageCostCurrencyID);
            ViewBag.TranID = new SelectList(db.Tran, "TranID", "Updator", trandetail.TranID);
            return RedirectToAction("Details", "Tran", new { id = trandetail.TranID });
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
