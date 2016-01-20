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
    public class ShippingTermController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        // GET: /ShippingTerm/
        public async Task<ActionResult> Index()
        {
            return View(await db.ShippingTerm.ToListAsync());
        }

        // GET: /ShippingTerm/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingTerm shippingterm = await db.ShippingTerm.FindAsync(id);
            if (shippingterm == null)
            {
                return HttpNotFound();
            }
            return View(shippingterm);
        }

        // GET: /ShippingTerm/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ShippingTerm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ShippingTermID,Updated,Updator,ShippingTermName,ShippingTermDescription")] ShippingTerm shippingterm)
        {
            if (ModelState.IsValid)
            {
                db.ShippingTerm.Add(shippingterm);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(shippingterm);
        }

        // GET: /ShippingTerm/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingTerm shippingterm = await db.ShippingTerm.FindAsync(id);
            if (shippingterm == null)
            {
                return HttpNotFound();
            }
            return View(shippingterm);
        }

        // POST: /ShippingTerm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ShippingTermID,Updated,Updator,ShippingTermName,ShippingTermDescription")] ShippingTerm shippingterm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shippingterm).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(shippingterm);
        }

        // GET: /ShippingTerm/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingTerm shippingterm = await db.ShippingTerm.FindAsync(id);
            if (shippingterm == null)
            {
                return HttpNotFound();
            }
            return View(shippingterm);
        }

        // POST: /ShippingTerm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ShippingTerm shippingterm = await db.ShippingTerm.FindAsync(id);
            db.ShippingTerm.Remove(shippingterm);
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
