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
    public class TranTotalsController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        // GET: /TranTotals/
        public  ActionResult Index()
        {
            var trantotals = db.TranTotals.Include(t => t.Currency).Include(t => t.Tran);
            return View( trantotals.ToList());
        }

        // GET: /TranTotals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<TranTotals> trantotals = db.TranTotals.Where(x => x.TranID == id).ToList();
            if (trantotals == null)
            {
                return HttpNotFound();
            }
            return View(trantotals);
        }

        // GET: /TranTotals/Create
        public ActionResult Create()
        {
            ViewBag.CurrencyID = new SelectList(db.Currency, "CurrencyID", "Updator");
            ViewBag.TranID = new SelectList(db.Tran, "TranID", "Updator");
            return View();
        }

        // POST: /TranTotals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Create([Bind(Include="TranID,FinType,CurrencyID,FinTotal")] TranTotals trantotals)
        {
            if (ModelState.IsValid)
            {
                db.TranTotals.Add(trantotals);
                 db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trantotals.CurrencyID);
            ViewBag.TranID = new SelectList(db.Tran, "TranID", "Updator", trantotals.TranID);
            return View(trantotals);
        }

        // GET: /TranTotals/Edit/5
        public  ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranTotals trantotals =  db.TranTotals.Find(id);
            if (trantotals == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trantotals.CurrencyID);
            ViewBag.TranID = new SelectList(db.Tran, "TranID", "Updator", trantotals.TranID);
            return View(trantotals);
        }

        // POST: /TranTotals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Edit([Bind(Include="TranID,FinType,CurrencyID,FinTotal")] TranTotals trantotals)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trantotals).State = EntityState.Modified;
                 db.SaveChanges();
                //return RedirectToAction("Index");
				return RedirectToAction("Details", "Tran", new { id = trantotals.TranID });
            }
            ViewBag.CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", trantotals.CurrencyID);
            ViewBag.TranID = new SelectList(db.Tran, "TranID", "Updator", trantotals.TranID);
            return View(trantotals);
        }

        // GET: /TranTotals/Delete/5
        public  ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TranTotals trantotals =  db.TranTotals.Find(id);
            if (trantotals == null)
            {
                return HttpNotFound();
            }
            return View(trantotals);
        }

        // POST: /TranTotals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  ActionResult DeleteConfirmed(int id)
        {
            TranTotals trantotals =  db.TranTotals.Find(id);
            db.TranTotals.Remove(trantotals);
             db.SaveChanges();
            return RedirectToAction("Index");
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
