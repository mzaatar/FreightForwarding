using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FFSolution.Models;
using FFSolution.Commons;

namespace FFSolution.Controllers
{
    public partial class TranController : Controller
    {
        private FFAdminDBEntities db = new FFAdminDBEntities();

        
        // GET: /Tran/
        public ActionResult Index()
        {
            var tran = db.Tran
                .Include(t => t.Customer)
                .Include(t => t.Customer1);

            //var tran = db.Tran.Include(t => t.Agent).Include(t => t.Commodity).Include(t => t.Country).Include(t => t.Customer).Include(t => t.Customer1).Include(t => t.SalesMan).Include(t => t.ShippingTerm).Include(t => t.ShippingType).Include(t => t.FeesInDischargePortNet).Include(t => t.FeesInDischargePortSelling).Include(t => t.FeesInOriginNet).Include(t => t.FeesInOriginSelling).Include(t => t.TranCharges).Include(t => t.TranDetail).Include(t => t.TranTotals).Include(t => t.Route);
            return View(tran.ToList());
        }

        // GET: /Tran/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tran tran = db.Tran.Find(id);
            //get the tran container before load teh transaction
            ViewBag.TCs = db.TranContainer.Where(t => t.TranID == id).ToList();
            if (tran == null)
            {
                return HttpNotFound();
            }
            return View(tran);
        }


        [Authorize(Roles = "administrators,sales,operation")]
        // GET: /Tran/Create
        public ActionResult Create()
        {
            ViewBag.AgentID = new SelectList(db.Agent, "AgentID", "AgentName");
            ViewBag.CommodityID = new SelectList(db.Commodity, "CommodityID", "CommodityName");
            ViewBag.OriginCountryID = new SelectList(db.Country, "CountryID", "CountryName");
            ViewBag.ConsigneeID = new SelectList(db.Customer, "CustomerID", "CustomerName");
            ViewBag.ShipperID = new SelectList(db.Customer, "CustomerID", "CustomerName");
            ViewBag.CourierID = new SelectList(db.Courier, "CourierID", "CourierName");
            ViewBag.SalesManID = new SelectList(db.SalesMan, "SalesManID", "SalesManName");
            ViewBag.ShippingTermID = new SelectList(db.ShippingTerm, "ShippingTermID", "ShippingTermName");
            ViewBag.ShippingTypeID = new SelectList(db.ShippingType, "ShippingTypeID", "ShippingTypeName");

            ViewBag.POL = new SelectList(db.Port, "PortID", "PortName");
            ViewBag.POD = new SelectList(db.Port, "PortID", "PortName");
            try
            {
                int salesmanID = SalesManLogic.GetSalesManID(User.Identity.Name);
                ViewBag.CurrentSalesManName = db.SalesMan.FirstOrDefault(s => s.SalesManID == salesmanID).SalesManName;
            }
            catch
            {
                ViewBag.CurrentSalesManName = "none";
            }
            
            // zaatar 
            ViewBag.countryList = new SelectList(db.Country.AsEnumerable(), "CountryID", "CountryName", "-- Select Agent Country --");//db.Country.ToList();

            
            ViewBag.ShippingTypes = db.ShippingType.ToList();
            return View();
        }

        // POST: /Tran/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrators,sales,operation")]
        public ActionResult Create([Bind(Include = "TranID,Updated,Updator,TranRefNumber,ReservationDate,BookingDate,BLNo,ShipperID,ConsigneeID,PhoneNumber,AgentID,SalesManID,ShippingTypeID,ShippingTypeNumbers,CommodityID,ShippingTermID,OriginCountryID,POL,POD,ETD,ETA,TT,FinalDistination,Notes,CourierID")] Tran tran)
        {
            tran.TranRefNumber = Helpers.CreateTranRefNumber();
           
            //get sales man ID to be saved in the tran.SalesManID
            if(tran.SalesManID == null )
            {
                tran.SalesManID = SalesManLogic.GetSalesManID(User.Identity.Name);
            }

            if (ModelState.IsValid)
            {
                db.Tran.Add(tran);
                db.SaveChanges();
                BusinessLogic.Calculations.CalcTran(tran.TranID, 6);
                //BusinessLogic.Calculations.CalcTotals(tran.TranID);
                return RedirectToAction("Index");
            }

            ViewBag.AgentID = new SelectList(db.Agent, "AgentID", "AgentName", tran.AgentID);
            ViewBag.CommodityID = new SelectList(db.Commodity, "CommodityID", "CommodityName", tran.CommodityID);
            ViewBag.OriginCountryID = new SelectList(db.Country, "CountryID", "CountryName", tran.OriginCountryID);
            ViewBag.ConsigneeID = new SelectList(db.Customer, "CustomerID", "CustomerName", tran.ConsigneeID);
            ViewBag.ShipperID = new SelectList(db.Customer, "CustomerID", "CustomerName", tran.ShipperID);
            ViewBag.CourierID = new SelectList(db.Courier, "CourierID", "CourierName", tran.CourierID);
            ViewBag.SalesManID = new SelectList(db.SalesMan, "SalesManID", "SalesManName", tran.SalesManID);
            ViewBag.ShippingTermID = new SelectList(db.ShippingTerm, "ShippingTermID", "ShippingTermName", tran.ShippingTermID);
            // ZAATAR-MULTIST ViewBag.ShippingTypeID = new SelectList(db.ShippingType, "ShippingTypeID", "ShippingTypeName", tran.ShippingTypeID);
           
            ViewBag.POL = new SelectList(db.Port, "PortID", "PortName", tran.POL);
            ViewBag.POD = new SelectList(db.Port, "PortID", "PortName", tran.POD);

            
            // zaatar 
            ViewBag.countryList = new SelectList(db.Country.AsEnumerable(), "CountryID", "CountryName", "-- Select Agent Country --");//db.Country.ToList();


            try
            {
                int salesmanID = SalesManLogic.GetSalesManID(User.Identity.Name);
                ViewBag.CurrentSalesManName = db.SalesMan.FirstOrDefault(s => s.SalesManID == salesmanID).SalesManName;
            }
            catch
            {
                ViewBag.CurrentSalesManName = "none";
            }


            return View(tran);
        }

        // GET: /Tran/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tran tran = db.Tran.Find(id);
            if (tran == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgentID = new SelectList(db.Agent, "AgentID", "AgentName", tran.AgentID);
            ViewBag.CommodityID = new SelectList(db.Commodity, "CommodityID", "CommodityName", tran.CommodityID);
            ViewBag.OriginCountryID = new SelectList(db.Country, "CountryID", "CountryName", tran.OriginCountryID);
            ViewBag.ConsigneeID = new SelectList(db.Customer, "CustomerID", "CustomerName", tran.ConsigneeID);
            ViewBag.ShipperID = new SelectList(db.Customer, "CustomerID", "CustomerName", tran.ShipperID);
            ViewBag.CourierID = new SelectList(db.Courier, "CourierID", "CourierName", tran.CourierID);
            ViewBag.SalesManID = new SelectList(db.SalesMan, "SalesManID", "SalesManName", tran.SalesManID);
            ViewBag.ShippingTermID = new SelectList(db.ShippingTerm, "ShippingTermID", "ShippingTermName", tran.ShippingTermID);
            // ZAATAR-MULTIST ViewBag.ShippingTypeID = new SelectList(db.ShippingType, "ShippingTypeID", "ShippingTypeName", tran.ShippingTypeID);
            ViewBag.POL = new SelectList(db.Port, "PortID", "PortName", tran.POL);
            ViewBag.POD = new SelectList(db.Port, "PortID", "PortName", tran.POD);
            if (tran.SalesMan != null)
                ViewBag.SalesManName = tran.SalesMan.SalesManName;
            else
                ViewBag.SalesManName = "None";
            return View(tran);
        }

        // POST: /Tran/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TranID,Updated,Updator,TranRefNumber,ReservationDate,BookingDate,BLNo,ShipperID,ConsigneeID,PhoneNumber,AgentID,SalesManID,ShippingTypeID,ShippingTypeNumbers,CommodityID,ShippingTermID,OriginCountryID,POL,POD,ETD,ETA,TT,FinalDistination,Notes,CourierID")] Tran tran)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tran).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgentID = new SelectList(db.Agent, "AgentID", "AgentName", tran.AgentID);
            ViewBag.CommodityID = new SelectList(db.Commodity, "CommodityID", "CommodityName", tran.CommodityID);
            ViewBag.OriginCountryID = new SelectList(db.Country, "CountryID", "CountryName", tran.OriginCountryID);
            ViewBag.ConsigneeID = new SelectList(db.Customer, "CustomerID", "CustomerName", tran.ConsigneeID);
            ViewBag.ShipperID = new SelectList(db.Customer, "CustomerID", "CustomerName", tran.ShipperID);
            ViewBag.CourierID = new SelectList(db.Courier, "CourierID", "CourierName", tran.CourierID);
            ViewBag.SalesManID = new SelectList(db.SalesMan, "SalesManID", "SalesManName", tran.SalesManID);
            ViewBag.ShippingTermID = new SelectList(db.ShippingTerm, "ShippingTermID", "ShippingTermName", tran.ShippingTermID);
            // ZAATAR-MULTIST ViewBag.ShippingTypeID = new SelectList(db.ShippingType, "ShippingTypeID", "ShippingTypeName", tran.ShippingTypeID);
            ViewBag.POL = new SelectList(db.Port, "PortID", "PortName", tran.POL);
            ViewBag.POD = new SelectList(db.Port, "PortID", "PortName", tran.POD);
            return View(tran);
        }

        // GET: /Tran/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tran tran = db.Tran.Find(id);
            if (tran == null)
            {
                return HttpNotFound();
            }
            return View(tran);
        }

        // POST: /Tran/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tran tran = db.Tran.Find(id);
            db.Tran.Remove(tran);
            db.SaveChanges();
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
