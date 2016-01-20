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
    public class RoleController : Controller
    {
        private FFSecurityDBEntities db = new FFSecurityDBEntities();

        // GET: /Role/
        public async Task<ActionResult> Index()
        {
            return View(await db.AspNetRoles.ToListAsync());
        }

        // GET: /Role/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRoles aspnetroles = await db.AspNetRoles.FindAsync(id);
            if (aspnetroles == null)
            {
                return HttpNotFound();
            }
            return View(aspnetroles);
        }

        // GET: /Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name")] AspNetRoles aspnetroles)
        {
            if (ModelState.IsValid)
            {
                db.AspNetRoles.Add(aspnetroles);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(aspnetroles);
        }

        // GET: /Role/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRoles aspnetroles = await db.AspNetRoles.FindAsync(id);
            if (aspnetroles == null)
            {
                return HttpNotFound();
            }
            return View(aspnetroles);
        }

        // POST: /Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name")] AspNetRoles aspnetroles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspnetroles).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(aspnetroles);
        }

        // GET: /Role/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRoles aspnetroles = await db.AspNetRoles.FindAsync(id);
            if (aspnetroles == null)
            {
                return HttpNotFound();
            }
            return View(aspnetroles);
        }

        // POST: /Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            AspNetRoles aspnetroles = await db.AspNetRoles.FindAsync(id);
            db.AspNetRoles.Remove(aspnetroles);
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
