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
    public class ShippingTypeController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        // GET: /ShippingType/
        public async Task<ActionResult> Index()
        {
            var shippingtype = db.ShippingType.Include(s => s.ShippingMode);
            return View(await shippingtype.ToListAsync());
        }

        // GET: /ShippingType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingType shippingtype = await db.ShippingType.FindAsync(id);
            if (shippingtype == null)
            {
                return HttpNotFound();
            }
            return View(shippingtype);
        }

        // GET: /ShippingType/Create
        public ActionResult Create()
        {
            ViewBag.ShippingModeID = new SelectList(db.ShippingMode, "ShippingModeID", "ShippingModeName");
            return View();
        }

        // POST: /ShippingType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ShippingTypeID,Updated,Updator,ShippingTypeName,ShippingModeID,Description")] ShippingType shippingtype)
        {
            if (ModelState.IsValid)
            {
                db.ShippingType.Add(shippingtype);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ShippingModeID = new SelectList(db.ShippingMode, "ShippingModeID", "ShippingModeName", shippingtype.ShippingModeID);
            return View(shippingtype);
        }

        // GET: /ShippingType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingType shippingtype = await db.ShippingType.FindAsync(id);
            if (shippingtype == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShippingModeID = new SelectList(db.ShippingMode, "ShippingModeID", "ShippingModeName", shippingtype.ShippingModeID);
            return View(shippingtype);
        }

        // POST: /ShippingType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ShippingTypeID,Updated,Updator,ShippingTypeName,ShippingModeID,Description")] ShippingType shippingtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shippingtype).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ShippingModeID = new SelectList(db.ShippingMode, "ShippingModeID", "ShippingModeName", shippingtype.ShippingModeID);
            return View(shippingtype);
        }

        // GET: /ShippingType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingType shippingtype = await db.ShippingType.FindAsync(id);
            if (shippingtype == null)
            {
                return HttpNotFound();
            }
            return View(shippingtype);
        }

        // POST: /ShippingType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ShippingType shippingtype = await db.ShippingType.FindAsync(id);
            db.ShippingType.Remove(shippingtype);
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
