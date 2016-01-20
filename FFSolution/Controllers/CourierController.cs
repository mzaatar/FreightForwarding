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
    public class CourierController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        // GET: /Courier/
        public async Task<ActionResult> Index()
        {
            return View(await db.Courier.ToListAsync());
        }

        // GET: /Courier/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courier courier = await db.Courier.FindAsync(id);
            if (courier == null)
            {
                return HttpNotFound();
            }
            return View(courier);
        }

        // GET: /Courier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Courier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CourierID,Updated,Updator,CourierName,CourierDescription,CourierAddress,CourierTelephone,AccountNumberUSD,AccountNumberEUR,AccountNumberEGP")] Courier courier)
        {
            if (ModelState.IsValid)
            {
                db.Courier.Add(courier);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(courier);
        }

        // GET: /Courier/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courier courier = await db.Courier.FindAsync(id);
            if (courier == null)
            {
                return HttpNotFound();
            }
            return View(courier);
        }

        // POST: /Courier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CourierID,Updated,Updator,CourierName,CourierDescription,CourierAddress,CourierTelephone,AccountNumberUSD,AccountNumberEUR,AccountNumberEGP")] Courier courier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courier).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(courier);
        }

        // GET: /Courier/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courier courier = await db.Courier.FindAsync(id);
            if (courier == null)
            {
                return HttpNotFound();
            }
            return View(courier);
        }

        // POST: /Courier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Courier courier = await db.Courier.FindAsync(id);
            db.Courier.Remove(courier);
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
