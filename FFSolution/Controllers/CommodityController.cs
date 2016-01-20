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
    public partial class CommodityController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        // GET: /Commodity/
        public async Task<ActionResult> Index()
        {
            return View(await db.Commodity.ToListAsync());
        }

        // GET: /Commodity/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commodity commodity = await db.Commodity.FindAsync(id);
            if (commodity == null)
            {
                return HttpNotFound();
            }
            return View(commodity);
        }

        // GET: /Commodity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Commodity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="CommodityID,Updated,Updator,CommodityName,CommodityDescription")] Commodity commodity)
        {
            if (ModelState.IsValid)
            {
                db.Commodity.Add(commodity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(commodity);
        }

        // GET: /Commodity/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commodity commodity = await db.Commodity.FindAsync(id);
            if (commodity == null)
            {
                return HttpNotFound();
            }
            return View(commodity);
        }

        // POST: /Commodity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="CommodityID,Updated,Updator,CommodityName,CommodityDescription")] Commodity commodity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commodity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(commodity);
        }

        // GET: /Commodity/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commodity commodity = await db.Commodity.FindAsync(id);
            if (commodity == null)
            {
                return HttpNotFound();
            }
            return View(commodity);
        }

        // POST: /Commodity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Commodity commodity = await db.Commodity.FindAsync(id);
            db.Commodity.Remove(commodity);
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
