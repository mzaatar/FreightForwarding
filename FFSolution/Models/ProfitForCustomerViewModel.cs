using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFSolution.Models
{
    public class ProfitForCustomerViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Customer ID</param>
        public ProfitForCustomerViewModel(int id)
        {
            using (FFAdminDBEntities db = new FFAdminDBEntities())
            {
                this.Customer = db.Customer.Find(id);

                if (Customer != null)
                {

                    TranList = new ProfitForTranListViewModel(
                        db.Tran.Where(s => s.ConsigneeID == Customer.CustomerID)
                        .Select(s=> s.TranID).ToList()
                        );
                }
            }
        }
        public Customer Customer { get; set; }

        public ProfitForTranListViewModel TranList { get; set; }
    }
}