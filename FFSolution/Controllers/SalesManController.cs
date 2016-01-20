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
    public partial class SalesManController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();
       
        // GET: /SalesMan/
        public async Task<ActionResult> Index()
        {
            return View(await db.SalesMan.ToListAsync());
        }

        // GET: /SalesMan/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesMan salesman = await db.SalesMan.FindAsync(id);
            if (salesman == null)
            {
                return HttpNotFound();
            }
            return View(salesman);
        }

        // GET: /SalesMan/Create
        public ActionResult Create()
        {
            using (FFSecurityDBEntities db2 = new FFSecurityDBEntities())
            {
                ViewBag.UserName = new SelectList(db2.AspNetUsers, "UserName", "UserName").ToList();
            }
            return View();
        }

        // POST: /SalesMan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="SalesManID,Updated,Updator,SalesManName,SalesManPhone,UserName")] SalesMan salesman)
        {
            if (ModelState.IsValid)
            {
                db.SalesMan.Add(salesman);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(salesman);
        }

        // GET: /SalesMan/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesMan salesman = await db.SalesMan.FindAsync(id);
            ViewBag.salesManName = salesman.UserName;

            using (FFSecurityDBEntities db2 = new FFSecurityDBEntities())
            {
                ViewBag.UserName = new SelectList(db2.AspNetUsers, "UserName", "UserName").ToList();
            }

            if (salesman == null)
            {
                return HttpNotFound();
            }
            return View(salesman);
        }

        // POST: /SalesMan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="SalesManID,Updated,Updator,SalesManName,SalesManPhone,UserName")] SalesMan salesman)
        {
           //if (HttpContext.User.IsInRole("sales"))
           // {
           //     salesman.UserName = HttpContext.User.Identity.Name;
           // }
           using (FFSecurityDBEntities db2 = new FFSecurityDBEntities())
           {
               ViewBag.UserName = new SelectList(db2.AspNetUsers, "UserName", "UserName" , salesman.UserName).ToList();
           }

            if (ModelState.IsValid)
            {
                db.Entry(salesman).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (HttpContext.User.IsInRole("sales"))
                {
                    return RedirectToAction("Details", "SalesMan");
                }
                return RedirectToAction("Index");
            }
            return View(salesman);
        }

        //// GET: /SalesMan/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SalesMan salesman = await db.SalesMan.FindAsync(id);
        //    if (salesman == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(salesman);
        //}

        //// POST: /SalesMan/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    SalesMan salesman = await db.SalesMan.FindAsync(id);
        //    db.SalesMan.Remove(salesman);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

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
