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
    public class ShippingModeController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        // GET: /ShippingMode/
        public async Task<ActionResult> Index()
        {
            var shippingmode = db.ShippingMode.Include(s => s.Measurement);
            return View(await shippingmode.ToListAsync());
        }

        // GET: /ShippingMode/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingMode shippingmode = await db.ShippingMode.FindAsync(id);
            if (shippingmode == null)
            {
                return HttpNotFound();
            }
            return View(shippingmode);
        }

        // GET: /ShippingMode/Create
        public ActionResult Create()
        {
            ViewBag.MeasurementID = new SelectList(db.Measurement, "MeaurementID", "MeaurementName");
            return View();
        }

        // POST: /ShippingMode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ShippingModeID,Updated,Updator,ShippingModeName,MeasurementID,Description")] ShippingMode shippingmode)
        {
            if (ModelState.IsValid)
            {
                db.ShippingMode.Add(shippingmode);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MeasurementID = new SelectList(db.Measurement, "MeaurementID", "MeaurementName", shippingmode.MeasurementID);
            return View(shippingmode);
        }

        // GET: /ShippingMode/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingMode shippingmode = await db.ShippingMode.FindAsync(id);
            if (shippingmode == null)
            {
                return HttpNotFound();
            }
            ViewBag.MeasurementID = new SelectList(db.Measurement, "MeaurementID", "MeaurementName", shippingmode.MeasurementID);
            return View(shippingmode);
        }

        // POST: /ShippingMode/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ShippingModeID,Updated,Updator,ShippingModeName,MeasurementID,Description")] ShippingMode shippingmode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shippingmode).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MeasurementID = new SelectList(db.Measurement, "MeaurementID", "MeaurementName", shippingmode.MeasurementID);
            return View(shippingmode);
        }

        // GET: /ShippingMode/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingMode shippingmode = await db.ShippingMode.FindAsync(id);
            if (shippingmode == null)
            {
                return HttpNotFound();
            }
            return View(shippingmode);
        }

        // POST: /ShippingMode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ShippingMode shippingmode = await db.ShippingMode.FindAsync(id);
            db.ShippingMode.Remove(shippingmode);
            await db.SaveChangesAsync();
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
