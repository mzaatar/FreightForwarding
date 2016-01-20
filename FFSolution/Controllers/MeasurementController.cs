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
    public class MeasurementController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        // GET: /Measurement/
        public async Task<ActionResult> Index()
        {
            return View(await db.Measurement.ToListAsync());
        }

        // GET: /Measurement/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurement measurement = await db.Measurement.FindAsync(id);
            if (measurement == null)
            {
                return HttpNotFound();
            }
            return View(measurement);
        }

        // GET: /Measurement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Measurement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="MeaurementID,Updated,Updator,MeasurementName,MeasurementDescription")] Measurement measurement)
        {
            if (ModelState.IsValid)
            {
                db.Measurement.Add(measurement);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(measurement);
        }

        // GET: /Measurement/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurement measurement = await db.Measurement.FindAsync(id);
            if (measurement == null)
            {
                return HttpNotFound();
            }
            return View(measurement);
        }

        // POST: /Measurement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="MeaurementID,Updated,Updator,MeasurementName,MeasurementDescription")] Measurement measurement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(measurement).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(measurement);
        }

        // GET: /Measurement/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurement measurement = await db.Measurement.FindAsync(id);
            if (measurement == null)
            {
                return HttpNotFound();
            }
            return View(measurement);
        }

        // POST: /Measurement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Measurement measurement = await db.Measurement.FindAsync(id);
            db.Measurement.Remove(measurement);
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
