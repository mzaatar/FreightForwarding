using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFSolution.Models
{
    public class ProfitForTranListViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Transaction IDs</param>
        public ProfitForTranListViewModel(List<int> ids)
        {
            using (FFAdminDBEntities db = new FFAdminDBEntities())
            {

                if (ids != null || ids.Count() > 0)
                {
                    this.ProfitsVM = new List<ProfitViewModel>();
                    // get trans

                   this.Trans = db.Tran.Where(t => ids.Contains(t.TranID)).ToList();

                    // calculate all profits per currency
                    foreach (var x in Trans)
                    {
                        this.ProfitsVM.Add(new ProfitViewModel(x.TranID));
                    }

                    // create grouping
                    ProfitGrouping = new List<TranProfit>();
                    CreateProfitGrouping();

                    // calculate the profit
                    foreach (var x in ProfitGrouping)
                    {
                        x.Profit = x.TotalEarned - x.TotalPaid;
                    }

                }
            }
        }

        public ProfitForTranListViewModel(List<Tran> trans)
        {
            using (FFAdminDBEntities db = new FFAdminDBEntities())
            {

                if (trans != null || trans.Count() > 0)
                {
                    this.ProfitsVM = new List<ProfitViewModel>();
                    // get trans

                    this.Trans = trans;

                    // calculate all profits per currency
                    foreach (var x in Trans)
                    {
                        this.ProfitsVM.Add(new ProfitViewModel(x.TranID));
                    }

                    // create grouping
                    ProfitGrouping = new List<TranProfit>();
                    CreateProfitGrouping();

                    // calculate the profit
                    foreach (var x in ProfitGrouping)
                    {
                        x.Profit = x.TotalEarned - x.TotalPaid;
                    }

                }
            }
        }


        public List<Tran> Trans { get; set; }
        public List<ProfitViewModel> ProfitsVM { get; set; }

        public List<TranProfit> ProfitGrouping { get; set; }

        private void CreateProfitGrouping()
        {
            foreach (var pvm in this.ProfitsVM)
            {
                foreach (var p in pvm.Profits)
                {
                    var g = this.ProfitGrouping.FirstOrDefault(x => x.CurrencyID == p.CurrencyID);
                    // add new grouping
                    if (g == null)
                    {
                        this.ProfitGrouping.Add(new TranProfit()
                        {
                            Currency = p.Currency,
                            TotalEarned = p.TotalEarned,
                            TotalPaid = p.TotalPaid

                        });
                    }
                    else
                    {
                        // add it
                        g.TotalPaid += p.TotalPaid;
                        g.TotalEarned += p.TotalEarned;
                    }
                }

            }
        }
    }
}