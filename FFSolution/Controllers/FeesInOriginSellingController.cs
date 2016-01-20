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
using FFSolution.BusinessLogic;

namespace FFSolution.Controllers
{
    public class FeesInOriginSellingController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        // GET: /FeesInOriginSelling/
        public  ActionResult Index()
        {
            var sub_feesinoriginselling = db.FeesInOriginSelling.Include(s => s.Currency).Include(s => s.Currency1).Include(s => s.Currency2).Include(s => s.Currency3).Include(s => s.Currency4).Include(s => s.Currency5).Include(s => s.Currency6).Include(s => s.Currency7).Include(s => s.Currency8).Include(s => s.Currency9).Include(s => s.Tran);
            return View( sub_feesinoriginselling.ToList());
        }

        // GET: /FeesInOriginSelling/Details/5
        public  ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeesInOriginSelling sub_feesinoriginselling =  db.FeesInOriginSelling.Find(id);
            if (sub_feesinoriginselling == null)
            {
                return HttpNotFound();
            }
            return View(sub_feesinoriginselling);
        }

        // GET: /FeesInOriginSelling/Edit/5
        public  ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeesInOriginSelling sub_feesinoriginselling =  db.FeesInOriginSelling.Find(id);
            if (sub_feesinoriginselling == null)
            {
                return HttpNotFound();
            }
            ViewBag.CIQCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.CIQCurrencyID);
            ViewBag.COCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.COCurrencyID);
            ViewBag.CourierCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.CourierCurrencyID);
            ViewBag.CustomsClearanceCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.CustomsClearanceCurrencyID);
            ViewBag.EuropeAllInCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.EuropeAllInCurrencyID);
            ViewBag.InsuranceCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.InsuranceCurrencyID);
            ViewBag.OthersCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.OthersCurrencyID);
            ViewBag.SealFeesCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.SealFeesCurrencyID);
            ViewBag.THCCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.THCCurrencyID);
            ViewBag.TruckCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.TruckCurrencyID);
            ViewBag.AdditionalField1CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.AdditionalField1CurrencyID);
            ViewBag.AdditionalField2CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.AdditionalField2CurrencyID);
            return View(sub_feesinoriginselling);
        }

        // POST: /FeesInOriginSelling/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TranID,Updated,Updator,IsChina,THC,THCCurrencyID,Truck,TruckCurrencyID,CIQ,CIQCurrencyID,CO,COCurrencyID,SealFees,SealFeesCurrencyID,Courier,CourierCurrencyID,Insurance,InsuranceCurrencyID,CustomsClearance,CustomsClearanceCurrencyID,Others,OthersCurrencyID,EuropeAllIn,EuropeAllInCurrencyID,AdditionalField1,AdditionalField1CurrencyID,AdditionalField2,AdditionalField2CurrencyID")] FeesInOriginSelling sub_feesinoriginselling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_feesinoriginselling).State = EntityState.Modified;
                 db.SaveChanges();
                //return RedirectToAction("Index");
                 Calculations.CalcTran(sub_feesinoriginselling.TranID, 4);
				return RedirectToAction("Details", "Tran", new { id = sub_feesinoriginselling.TranID });
            }
            ViewBag.CIQCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.CIQCurrencyID);
            ViewBag.COCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.COCurrencyID);
            ViewBag.CourierCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.CourierCurrencyID);
            ViewBag.CustomsClearanceCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.CustomsClearanceCurrencyID);
            ViewBag.EuropeAllInCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.EuropeAllInCurrencyID);
            ViewBag.InsuranceCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.InsuranceCurrencyID);
            ViewBag.OthersCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.OthersCurrencyID);
            ViewBag.SealFeesCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.SealFeesCurrencyID);
            ViewBag.THCCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.THCCurrencyID);
            ViewBag.TruckCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.TruckCurrencyID);
            ViewBag.AdditionalField1CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.AdditionalField1CurrencyID);
            ViewBag.AdditionalField2CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginselling.AdditionalField2CurrencyID);
            
            return View(sub_feesinoriginselling);
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
