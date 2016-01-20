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

namespace FFSolution.Models
{
    public class TranContainersViewModel
    {
        
        public TranContainersViewModel(int tranId)
        {
            BuildShippingTypeList();
            using (FFAdminDBEntities db = new FFAdminDBEntities())
            {
                //db.TranContainer.Include(a => a.ShippingType);
                TranContainers = db.TranContainer.Include(a => a.ShippingType).Where(t => t.TranID == tranId).ToList();
                TranID = tranId;
                StatusID = db.Tran.FirstOrDefault().TranDetail.StatusID.Value;
            }
        }

        public TranContainersViewModel()
        {
            BuildShippingTypeList();
            TranContainers = new List<TranContainer>();
            using (FFAdminDBEntities db = new FFAdminDBEntities())
            {
                foreach(var shiptype in db.ShippingType.ToList())
                {
                    TranContainers.Add(new TranContainer() {
                        
                        ShippingType = shiptype,
                        Count = 0
                    });
                }
            }
            TranID = -1;
            StatusID = -1;
        }
        public void BuildShippingTypeList()
        {
            using (FFAdminDBEntities db = new FFAdminDBEntities())
            {
                ShippingTypeList = db.ShippingType.ToList();
            }
        }

        public List<ShippingType> ShippingTypeList = new List<ShippingType>();
        public List<TranContainer> TranContainers = new List<TranContainer>();

        public int TranID = -1;
        public int StatusID = -1;
    }
}