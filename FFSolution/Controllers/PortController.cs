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
    public partial class PortController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        // GET: /Port/
        public async Task<ActionResult> Index()
        {
            var port = db.Port.Include(p => p.Country);
            return View(await port.ToListAsync());
        }

        // GET: /Port/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Port port = await db.Port.FindAsync(id);
            if (port == null)
            {
                return HttpNotFound();
            }
            return View(port);
        }

        // GET: /Port/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName");
            return View();
        }

        // POST: /Port/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PortID,Updated,Updator,PortName,CountryID")] Port port)
        {
            if (ModelState.IsValid)
            {
                db.Port.Add(port);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName", port.CountryID);
            return View(port);
        }

        // GET: /Port/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Port port = await db.Port.FindAsync(id);
            if (port == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName", port.CountryID);
            return View(port);
        }

        // POST: /Port/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PortID,Updated,Updator,PortName,CountryID")] Port port)
        {
            if (ModelState.IsValid)
            {
                db.Entry(port).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName", port.CountryID);
            return View(port);
        }

        // GET: /Port/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Port port = await db.Port.FindAsync(id);
            if (port == null)
            {
                return HttpNotFound();
            }
            return View(port);
        }

        // POST: /Port/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Port port = await db.Port.FindAsync(id);
            db.Port.Remove(port);
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
