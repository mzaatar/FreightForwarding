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
    public class FeesInDischargePortSellingController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        // GET: /FeesInDischargePortSelling/
        public  ActionResult Index()
        {
            var sub_feesindischargeportselling = db.FeesInDischargePortSelling.Include(s => s.Currency).Include(s => s.Currency1).Include(s => s.Currency2).Include(s => s.Currency3).Include(s => s.Currency4).Include(s => s.Currency5).Include(s => s.Currency6).Include(s => s.Currency7).Include(s => s.Currency8).Include(s => s.Tran);
            return View( sub_feesindischargeportselling.ToList());
        }

        // GET: /FeesInDischargePortSelling/Details/5
        public  ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeesInDischargePortSelling sub_feesindischargeportselling =  db.FeesInDischargePortSelling.Find(id);
            if (sub_feesindischargeportselling == null)
            {
                return HttpNotFound();
            }
            return View(sub_feesindischargeportselling);
        }

        //// GET: /FeesInDischargePortSelling/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ClearanceCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode");
        //    ViewBag.CustomsCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode");
        //    ViewBag.LicenseCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode");
        //    ViewBag.OthersCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode");
        //    ViewBag.OtherCustomsFeesCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode");
        //    ViewBag.PacingCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode");
        //    ViewBag.ReceptCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode");
        //    ViewBag.THCCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode");
        //    ViewBag.TruckCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode");

        //    ViewBag.AdditionalField1CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode");
        //    ViewBag.AdditionalField2CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode");

        //    return View();
        //}

        //// POST: /FeesInDischargePortSelling/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "TranID,Updated,Updator,CustomsPersonName,THC,THCCurrencyID,Truck,TruckCurrencyID,Pacing,PacingCurrencyID,License,LicenseCurrencyID,Recept,ReceptCurrencyID,Customs,CustomsCurrencyID,Clearance,ClearanceCurrencyID,OtherCustomsFees,OtherCustomsFeesCurrencyID,Others,OthersCurrencyID,AdditionalField1,AdditionalField1CurrencyID,AdditionalField2,AdditionalField2CurrencyID")] FeesInDischargePortSelling sub_feesindischargeportselling)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.FeesInDischargePortSelling.Add(sub_feesindischargeportselling);
        //         db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ClearanceCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.ClearanceCurrencyID);
        //    ViewBag.CustomsCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.CustomsCurrencyID);
        //    ViewBag.LicenseCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.LicenseCurrencyID);
        //    ViewBag.OthersCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.OthersCurrencyID);
        //    ViewBag.OtherCustomsFeesCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.OtherCustomsFeesCurrencyID);
        //    ViewBag.PacingCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.PacingCurrencyID);
        //    ViewBag.ReceptCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.ReceptCurrencyID);
        //    ViewBag.THCCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.THCCurrencyID);
        //    ViewBag.TruckCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.TruckCurrencyID);

        //    ViewBag.AdditionalField1CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.AdditionalField1CurrencyID);
        //    ViewBag.AdditionalField2CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.AdditionalField2CurrencyID);
            
        //    return View(sub_feesindischargeportselling);
        //}

        // GET: /FeesInDischargePortSelling/Edit/5
        public  ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeesInDischargePortSelling sub_feesindischargeportselling =  db.FeesInDischargePortSelling.Find(id);
            if (sub_feesindischargeportselling == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClearanceCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.ClearanceCurrencyID);
            ViewBag.CustomsCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.CustomsCurrencyID);
            ViewBag.LicenseCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.LicenseCurrencyID);
            ViewBag.OthersCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.OthersCurrencyID);
            ViewBag.OtherCustomsFeesCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.OtherCustomsFeesCurrencyID);
            ViewBag.PacingCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.PacingCurrencyID);
            ViewBag.ReceptCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.ReceptCurrencyID);
            ViewBag.THCCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.THCCurrencyID);
            ViewBag.TruckCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.TruckCurrencyID);
            ViewBag.AdditionalField1CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.AdditionalField1CurrencyID);
            ViewBag.AdditionalField2CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.AdditionalField2CurrencyID);
            return View(sub_feesindischargeportselling);
        }

        // POST: /FeesInDischargePortSelling/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TranID,Updated,Updator,CustomsPersonName,THC,THCCurrencyID,Truck,TruckCurrencyID,Pacing,PacingCurrencyID,License,LicenseCurrencyID,Recept,ReceptCurrencyID,Customs,CustomsCurrencyID,Clearance,ClearanceCurrencyID,OtherCustomsFees,OtherCustomsFeesCurrencyID,Others,OthersCurrencyID,AdditionalField1,AdditionalField1CurrencyID,AdditionalField2,AdditionalField2CurrencyID")] FeesInDischargePortSelling sub_feesindischargeportselling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_feesindischargeportselling).State = EntityState.Modified;
                 db.SaveChanges();
                //return RedirectToAction("Index");
                 Calculations.CalcTran(sub_feesindischargeportselling.TranID, 2);
				return RedirectToAction("Details", "Tran", new { id = sub_feesindischargeportselling.TranID });
            }
            ViewBag.ClearanceCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.ClearanceCurrencyID);
            ViewBag.CustomsCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.CustomsCurrencyID);
            ViewBag.LicenseCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.LicenseCurrencyID);
            ViewBag.OthersCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.OthersCurrencyID);
            ViewBag.OtherCustomsFeesCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.OtherCustomsFeesCurrencyID);
            ViewBag.PacingCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.PacingCurrencyID);
            ViewBag.ReceptCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.ReceptCurrencyID);
            ViewBag.THCCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.THCCurrencyID);
            ViewBag.TruckCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.TruckCurrencyID);
            ViewBag.AdditionalField1CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.AdditionalField1CurrencyID);
            ViewBag.AdditionalField2CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesindischargeportselling.AdditionalField2CurrencyID);
            return View(sub_feesindischargeportselling);
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
