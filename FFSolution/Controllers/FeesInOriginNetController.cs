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
    public class FeesInOriginNetController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        // GET: /FeesInOriginNet/
        public  ActionResult Index()
        {
            var sub_feesinoriginnet = db.FeesInOriginNet.Include(s => s.Currency).Include(s => s.Currency1).Include(s => s.Currency2).Include(s => s.Currency3).Include(s => s.Currency4).Include(s => s.Currency5).Include(s => s.Currency6).Include(s => s.Currency7).Include(s => s.Currency8).Include(s => s.Currency9).Include(s => s.Tran);
            return View( sub_feesinoriginnet.ToList());
        }

        // GET: /FeesInOriginNet/Details/5
        public  ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeesInOriginNet sub_feesinoriginnet =  db.FeesInOriginNet.Find(id);
            if (sub_feesinoriginnet == null)
            {
                return HttpNotFound();
            }
            return View(sub_feesinoriginnet);
        }


        // GET: /FeesInOriginNet/Edit/5
        public  ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeesInOriginNet sub_feesinoriginnet =  db.FeesInOriginNet.Find(id);
            if (sub_feesinoriginnet == null)
            {
                return HttpNotFound();
            }
            ViewBag.CIQCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.CIQCurrencyID);
            ViewBag.COCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.COCurrencyID);
            ViewBag.CourierCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.CourierCurrencyID);
            ViewBag.CustomsClearanceCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.CustomsClearanceCurrencyID);
            ViewBag.EuropeAllInCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.EuropeAllInCurrencyID);
            ViewBag.InsuranceCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.InsuranceCurrencyID);
            ViewBag.OthersCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.OthersCurrencyID);
            ViewBag.SealFeesCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.SealFeesCurrencyID);
            ViewBag.THCCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.THCCurrencyID);
            ViewBag.TruckCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.TruckCurrencyID);
            ViewBag.AdditionalField1CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.AdditionalField1CurrencyID);
            ViewBag.AdditionalField2CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.AdditionalField2CurrencyID);
            return View(sub_feesinoriginnet);
        }

        // POST: /FeesInOriginNet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TranID,Updated,Updator,IsChina,THC,THCCurrencyID,Truck,TruckCurrencyID,CIQ,CIQCurrencyID,CO,COCurrencyID,SealFees,SealFeesCurrencyID,Courier,CourierCurrencyID,Insurance,InsuranceCurrencyID,CustomsClearance,CustomsClearanceCurrencyID,Others,OthersCurrencyID,EuropeAllIn,EuropeAllInCurrencyID,AdditionalField1,AdditionalField1CurrencyID,AdditionalField2,AdditionalField2CurrencyID")] FeesInOriginNet sub_feesinoriginnet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_feesinoriginnet).State = EntityState.Modified;
                 db.SaveChanges();
                //return RedirectToAction("Index");
                 Calculations.CalcTran(sub_feesinoriginnet.TranID , 3);
				return RedirectToAction("Details", "Tran", new { id = sub_feesinoriginnet.TranID });
            }
            ViewBag.CIQCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.CIQCurrencyID);
            ViewBag.COCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.COCurrencyID);
            ViewBag.CourierCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.CourierCurrencyID);
            ViewBag.CustomsClearanceCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.CustomsClearanceCurrencyID);
            ViewBag.EuropeAllInCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.EuropeAllInCurrencyID);
            ViewBag.InsuranceCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.InsuranceCurrencyID);
            ViewBag.OthersCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.OthersCurrencyID);
            ViewBag.SealFeesCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.SealFeesCurrencyID);
            ViewBag.THCCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.THCCurrencyID);
            ViewBag.TruckCurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.TruckCurrencyID);
            ViewBag.AdditionalField1CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.AdditionalField1CurrencyID);
            ViewBag.AdditionalField2CurrencyID = new SelectList(db.Currency, "CurrencyID", "CurrencyCode", sub_feesinoriginnet.AdditionalField2CurrencyID);
            return View(sub_feesinoriginnet);
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
