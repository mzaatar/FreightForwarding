using FFSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFSolution.BusinessLogic
{
    public class Calculations
    {
        public static void CalcTran(int TranID, int mode)
            // 1 dis net , 2 dis seleling , 3 origin net , 4 origin selling , 5 charges , 6 all
        {
            using (FFAdminDBEntities db = new Models.FFAdminDBEntities())
            {
                switch (mode)
                {
                    case 0 :
                        break;
                    case 1:
                        db.Update_Totals_FeesInDischargePortNet(TranID);
                        break;
                    case 2:
                        db.Update_Totals_FeesInDischargePortSelling(TranID);
                        break;
                    case 3:
                        db.Update_Totals_FeesInOriginNet(TranID);
                        break;
                    case 4:
                        db.Update_Totals_FeesInOriginSelling(TranID);
                        break;
                    case 5:
                        db.Update_Totals_TranCharges(TranID);
                        break;
                    case 6:
                        db.Update_Totals_TranContainer(TranID);
                        break;
                    case 7:
                        db.Update_Totals_FeesInDischargePortNet(TranID);
                        db.Update_Totals_FeesInDischargePortSelling(TranID);
                        db.Update_Totals_FeesInOriginNet(TranID);
                        db.Update_Totals_FeesInOriginSelling(TranID);
                        db.Update_Totals_TranCharges(TranID);
                        db.Update_Totals_TranContainer(TranID);
                        break;
                }

                // delete zero totals before calc profit
                db.DeleteZeroTotals(TranID);

                // calc totals
                CalcProfit(TranID);
                // update workflow system
                //new FFWorkFlow().UpdateTranStatus(TranID);
            }
        }


        private static void CalcProfit(int TranID)
        {
            List<TranTotals> listOfTranTotals;

            using (FFAdminDBEntities db = new Models.FFAdminDBEntities())
            {
                listOfTranTotals = db.TranTotals.Where(y => y.TranID == TranID).ToList();

                if (listOfTranTotals == null || listOfTranTotals.Count == 0)
                    return;
                else
                {
                    // delete all profit and recalculate them again
                    db.TranProfit.RemoveRange(
                        db.TranProfit.Where(s=>s.TranID == TranID)
                        );
                    db.SaveChanges();

                    // add new profits
                    foreach (var x in listOfTranTotals)
                    {
                        TranProfit p = db.TranProfit.FirstOrDefault(c => c.TranID == TranID && c.CurrencyID == x.CurrencyID);
                        if (p == null)
                        {
                            p = db.TranProfit.Add(
                                new TranProfit()
                                {
                                    CurrencyID = x.CurrencyID,
                                    TranID = x.TranID
                                });
                            CalcProfitValue(x, p);
                            db.SaveChanges();
                            continue;
                        }
                        CalcProfitValue(x, p);
                    }
                }

                //save 
                db.SaveChanges();
            }
        }

        private static void CalcProfitValue(TranTotals total,TranProfit profit)
        {
            if (total.FinType == "FeesInDischargePortNet")
            {
                profit.TotalPaid += total.FinTotal;
            }
            else if (total.FinType == "FeesInDischargePortSelling")
            {
                profit.TotalEarned += total.FinTotal;
            }
            else if (total.FinType == "FeesInOriginNet")
            {
                profit.TotalPaid += total.FinTotal;
            }
            else if (total.FinType == "FeesInOriginSelling")
            {
                profit.TotalEarned += total.FinTotal;
            }
            else if (total.FinType == "TranChargesNet")
            {
                profit.TotalPaid += total.FinTotal;
            }
            else if (total.FinType == "TranChargesSelling")
            {
                profit.TotalEarned += total.FinTotal;
            }
            else if (total.FinType == "LineShippingNet")
            {
                profit.TotalPaid += total.FinTotal;
            }
            else if (total.FinType == "LineShippingSelling")
            {
                profit.TotalEarned += total.FinTotal;
            }
        }


       
    }
}