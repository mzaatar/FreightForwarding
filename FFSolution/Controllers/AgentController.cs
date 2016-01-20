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
    public partial class AgentController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        // GET: /Agent/
        public async Task<ActionResult> Index()
        {
            var agent = db.Agent.Include(a => a.Country).Include(a => a.Country);
            return View(await agent.ToListAsync());
        }

        // GET: /Agent/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = await db.Agent.FindAsync(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // GET: /Agent/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName");
            return View();
        }

        // POST: /Agent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="AgentID,Updated,Updator,AgentName,CountryID,PhoneNumber,Notes")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                db.Agent.Add(agent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName", agent.CountryID);
            return View(agent);
        }

        // GET: /Agent/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = await db.Agent.FindAsync(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName", agent.CountryID);
            return View(agent);
        }

        // POST: /Agent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="AgentID,Updated,Updator,AgentName,CountryID,PhoneNumber,Notes")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.Country, "CountryID", "CountryName", agent.CountryID);
            return View(agent);
        }

        // GET: /Agent/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = await db.Agent.FindAsync(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: /Agent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Agent agent = await db.Agent.FindAsync(id);
            db.Agent.Remove(agent);
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
