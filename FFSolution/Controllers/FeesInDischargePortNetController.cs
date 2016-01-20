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
    public class FeesInDischargePortNetController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        // GET: /FeesInDischargePortNet/
        public  ActionResult Index()
        {
            var sub_feesindischargeportnet = db.FeesInDischargePortNet.Include(s => s.Currency).Include(s => s.Currency1).Include(s => s.Currency2).Include(s => s.Currency3).Include(s => s.Currency4).Include(s => s.Currency5).Include(s => s.Currency6).Include(s => s.Currency7).Include(s => s.Currency8).Include(s => s.Tran);
            return View(sub_feesindischargeportnet.ToList());
        }

        // GET: /FeesInDischargePortNet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeesInDischargePortNet sub_feesindischargeportnet = db.FeesInDischargePortNet.Find(id);
            if (sub_feesindischargeportnet == null)
            {
                return HttpNotFound();
            }
            return View(sub_feesindischargeportnet);
        }

        // GET: /FeesInDischargePortNet/Edit/5
        public  ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeesInDischargePortNet sub_feesindischargeportnet =  db.FeesInDischargePortNet.Find(id);
            if (sub_feesindischargeportnet == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClearanceCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.ClearanceCurrencyID);
            ViewBag.CustomsCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.CustomsCurrencyID);
            ViewBag.LicenseCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.LicenseCurrencyID);
            ViewBag.OthersCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.OthersCurrencyID);
            ViewBag.OtherCustomsFeesCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.OtherCustomsFeesCurrencyID);
            ViewBag.PacingCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.PacingCurrencyID);
            ViewBag.ReceptCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.ReceptCurrencyID);
            ViewBag.THCCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.THCCurrencyID);
            ViewBag.TruckCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.TruckCurrencyID);

            ViewBag.AdditionalField1CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.AdditionalField1CurrencyID);
            ViewBag.AdditionalField2CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.AdditionalField2CurrencyID);

            return View(sub_feesindischargeportnet);
        }

        // POST: /FeesInDischargePortNet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TranID,Updated,Updator,CustomsPersonName,THC,THCCurrencyID,Truck,TruckCurrencyID,Pacing,PacingCurrencyID,License,LicenseCurrencyID,Recept,ReceptCurrencyID,Customs,CustomsCurrencyID,Clearance,ClearanceCurrencyID,OtherCustomsFees,OtherCustomsFeesCurrencyID,Others,OthersCurrencyID,AdditionalField1,AdditionalField1CurrencyID,AdditionalField2,AdditionalField2CurrencyID")] FeesInDischargePortNet sub_feesindischargeportnet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_feesindischargeportnet).State = EntityState.Modified;
                 db.SaveChanges();
               // return RedirectToAction("Index");
                 Calculations.CalcTran(sub_feesindischargeportnet.TranID, 1);
                 return RedirectToAction("Details", "Tran", new { id = sub_feesindischargeportnet.TranID });
            }
            ViewBag.ClearanceCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.ClearanceCurrencyID);
            ViewBag.CustomsCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.CustomsCurrencyID);
            ViewBag.LicenseCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.LicenseCurrencyID);
            ViewBag.OthersCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.OthersCurrencyID);
            ViewBag.OtherCustomsFeesCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.OtherCustomsFeesCurrencyID);
            ViewBag.PacingCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.PacingCurrencyID);
            ViewBag.ReceptCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.ReceptCurrencyID);
            ViewBag.THCCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.THCCurrencyID);
            ViewBag.TruckCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.TruckCurrencyID);

            ViewBag.AdditionalField1CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.AdditionalField1CurrencyID);
            ViewBag.AdditionalField2CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportnet.AdditionalField2CurrencyID);

            
            return View(sub_feesindischargeportnet);
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
