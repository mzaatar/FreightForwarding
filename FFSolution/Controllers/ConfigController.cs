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
    public class ConfigController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        // GET: /Config/
        public async Task<ActionResult> Index()
        {
            return View(await db.Config.ToListAsync());
        }

        // GET: /Config/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = await db.Config.FindAsync(id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // GET: /Config/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Config/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ConfigID,Updated,Updator,SystemPassword,DefaultCurrency")] Config config)
        {
            if (ModelState.IsValid)
            {
                db.Config.Add(config);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", config.DefaultCurrency);
            return View(config);
        }

        // GET: /Config/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = await db.Config.FindAsync(id);
            if (config == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", config.DefaultCurrency);
            return View(config);
        }

        // POST: /Config/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ConfigID,Updated,Updator,SystemPassword,DefaultCurrency")] Config config)
        {
            if (ModelState.IsValid)
            {
                db.Entry(config).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", config.DefaultCurrency);
            return View(config);
        }

        // GET: /Config/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = await db.Config.FindAsync(id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // POST: /Config/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Config config = await db.Config.FindAsync(id);
            db.Config.Remove(config);
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
