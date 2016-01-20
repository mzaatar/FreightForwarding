using FFSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace FFSolution.BusinessLogic
{
    public partial  class FFWorkFlow
    {
        FFAdminDBEntities db = new FFAdminDBEntities();


        internal List<string> UpdateTranStatus(int tranID, int statusFrom, int statusTo)
        {
            var tran = db.Tran.FirstOrDefault(t => t.TranID == tranID);
            var result = new List<string>();
            if (tran != null )
            {
                result = ValidateTranForNextStep(tran, statusFrom, statusTo);
                if (result.Count() == 0 )
                {    
                    tran.TranDetail.StatusID = statusTo;
                    db.SaveChangesAsync();
                    return result;
                }
                else
                {
                    return result;
                }
            }
            result.Add("Error finding the transaction");
            return result;
        }

        private List<string> ValidateTranForNextStep(Tran tran, int statusFrom, int statusTo)
        {
            if(statusFrom == 1 && statusTo == 2 )
            {
                return ValidateTranToBeOperations(tran);
            }
            else if (statusFrom == 2 && statusTo == 3)
            {
                return ValidateTranToBeAccountant(tran);
            }
            else if (statusFrom == 3 && statusTo == 4)
            {
                return ValidateTranToBeCompleted(tran);
            }
            return new List<string>();
        }

        #region Notification
        internal IEnumerable<KeyValuePair<int, string>> GetTranETA()
        {
            IEnumerable<KeyValuePair<int, string>> returnedTrans = null;

            var d = DateTime.Now.Date.Day;
            var m = DateTime.Now.Date.Month;
            var y = DateTime.Now.Date.Year;

            //var trans = db.Tran.Where(
            //    x => x.ETA.Value.Day <= d + 10 &&
            //        x.ETA.Value.Month == m &&
            //        x.ETA.Value.Year == y &&
            //        x.ETA.Value >= DateTime.Now)
            //        .ToList();
            DateTime etaNotificationRangeDate =  DateTime.Now.AddDays(7);
            var trans = db.Tran.Where(
                x => x.ETA.Value >= DateTime.Now &&
                    x.ETA.Value <= etaNotificationRangeDate)
                    .ToList();

            if(trans!= null )
                returnedTrans = trans.Select(t => new KeyValuePair<int, string>(t.TranID, "ETA is soon, BLNo : "+t.BLNo));

            return returnedTrans;
        }

        internal IEnumerable<KeyValuePair<int, string>> GetTranToBePaid()
        {
            IEnumerable<KeyValuePair<int, string>> returnedTrans = null;

            var d = DateTime.Now.Date.Day;
            var m = DateTime.Now.Date.Month;
            var y = DateTime.Now.Date.Year;

            var trans = db.Tran.Where(
                x => x.TranDetail.CollectDate.Value.Day <= d + 4 &&
                    x.TranDetail.CollectDate.Value.Month == m &&
                    x.TranDetail.CollectDate.Value.Year == y &&
                    x.TranDetail.PaymentFinished == false)
                    .ToList();

            if (trans != null)
                returnedTrans = trans.Select(t => new KeyValuePair<int, string>(t.TranID, "Today Payment, BLNo : " + t.BLNo));

            return returnedTrans;
        }

        internal IEnumerable<KeyValuePair<int, string>> GetTranForOperations()
        {
            IEnumerable<KeyValuePair<int, string>> returnedTrans = null;

            var trans = db.Tran.Where(
                x => x.TranDetail.StatusID == 2)
                    .ToList();

            if (trans != null)
                returnedTrans = trans.Select(t => new KeyValuePair<int, string>(t.TranID, "Waiting For Operations, BLNo : " + t.BLNo));

            return returnedTrans;
        }

        internal IEnumerable<KeyValuePair<int, string>> GetTranForAccounting()
        {
            IEnumerable<KeyValuePair<int, string>> returnedTrans = null;

            var trans = db.Tran.Where(
                x => x.TranDetail.StatusID == 3)
                    .ToList();

            if (trans != null)
                returnedTrans = trans.Select(t => new KeyValuePair<int, string>(t.TranID, "Waiting For Accounting, BLNo : " + t.BLNo));

            return returnedTrans;
        }

        internal IEnumerable<KeyValuePair<int, string>> GetTranForSales()
        {
            IEnumerable<KeyValuePair<int, string>> returnedTrans = null;

            var trans = db.Tran.Where(
                x => x.TranDetail.StatusID == 1)
                    .ToList();

            if (trans != null)
                returnedTrans = trans.Select(t => new KeyValuePair<int, string>(t.TranID, "Waiting for Sales (Incomplete), customer name : " + t.Customer.CustomerName));

            return returnedTrans;
        }
        #endregion
    }

}