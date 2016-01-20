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
    public class UserController : Controller
    {
        private FFSecurityDBEntities db = new FFSecurityDBEntities();

        [Authorize(Roles="administrators")]
        // GET: /User/
        public async Task<ActionResult> Index()
        {
            return View(await db.AspNetUsers.ToListAsync());
        }

        [Authorize(Roles = "administrators")]
        // GET: /User/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspnetusers = await db.AspNetUsers.FindAsync(id);
            if (aspnetusers == null)
            {
                return HttpNotFound();
            }
            return View(aspnetusers);
        }

    

        // GET: /User/Edit/5
        [Authorize(Roles = "administrators")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspnetusers = await db.AspNetUsers.FindAsync(id);
            if (aspnetusers == null)
            {
                return HttpNotFound();
            }
            return View(aspnetusers);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrators")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UserName,PasswordHash,SecurityStamp,Discriminator,IsAdministrator,IsAccounting,IsOperation,IsSales,IsCustomerService,CanLogin,PlainPassword,IsCustomClearance")] AspNetUsers aspnetusers)
        {
            if (ModelState.IsValid)
            {
                var u = db.AspNetUsers.Find(aspnetusers.Id);
                if(u != null)
                {
                    try { u.IsAccounting = aspnetusers.IsAccounting; }
                    catch { u.IsAccounting = false; }
                    try { u.IsSales = aspnetusers.IsSales; }
                    catch { u.IsSales = false; }
                    try { u.IsCustomerService = aspnetusers.IsCustomerService; }
                    catch { u.IsCustomerService = false; }
                    try { u.IsOperation = aspnetusers.IsOperation; }
                    catch { u.IsOperation = false; }
                    try { u.IsAdministrator = aspnetusers.IsAdministrator; }
                    catch { u.IsAdministrator = false; }
                    try { u.IsCustomClearance = aspnetusers.IsCustomClearance; }
                    catch { u.IsCustomClearance = false; }
                    try { u.CanLogin = aspnetusers.CanLogin; }
                    catch { u.CanLogin = false; }
                    try { u.Discriminator = aspnetusers.Discriminator;} catch{ u.Discriminator = "";}
                    u.PlainPassword = aspnetusers.PlainPassword;
                    await db.SaveChangesAsync();
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return RedirectToAction("Index");
            }
            return View(aspnetusers);
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
