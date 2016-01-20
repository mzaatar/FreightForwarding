using FFSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace FFSolution.BusinessLogic
{
    public partial class FFWorkFlow
    {
        private List<string> ValidateTranToBeCompleted(Tran tran)
        {
            List<string> errors = new List<string>();
            if (tran.CourierID == null)
            {
                errors.Add("Courier is Missing");
            }
            if (tran.BookingDate == null)
            {
                errors.Add("Booking Date is Missing");
            }
            if (tran.BLNo == null)
            {
                errors.Add("B/L Number is Missing");
            }
            if (tran.ConsigneeID == null)
            {
                errors.Add("Consignee is Missing");
            }

            if (tran.ETD == null)
            {
                errors.Add("ETD is Missing");
            }

            if (tran.TranDetail.PaymentFinished == false)
            {
                errors.Add("Payment finsihed flag is not set, there is still amount of money remaining");
            }

            //var oNetResult = CheckOriginNet(tran.FeesInOriginNet);
            //if (oNetResult.Count > 0)
            //{
            //    errors.AddRange(oNetResult);
            //}

            //var oSellingResult = CheckOriginSelling(tran.FeesInOriginSelling);
            //if (oSellingResult.Count > 0)
            //{
            //    errors.AddRange(oSellingResult);
            //}
            return errors;
        }

        private List<string> CheckOriginNet(FeesInOriginNet oNEt)
        {
            List<string> errors = new List<string>();
            if (oNEt.IsChina)
            {
                if (oNEt.Others.Value == 0
                   && oNEt.CIQ.Value == 0
                   && oNEt.CO.Value == 0
                   && oNEt.Courier.Value == 0
                   && oNEt.CustomsClearance.Value == 0
                   && oNEt.Insurance.Value == 0
                   && oNEt.SealFees.Value == 0
                   && oNEt.THC.Value == 0
                   && oNEt.Truck.Value == 0)
                {
                    errors.Add("Origin Selling Net amounts Missing");
                }
            }
            else
            {
                if (oNEt.EuropeAllIn.Value == 0)
                {
                    errors.Add("Origin Selling Net amounts Missing");
                }
            }
            return errors;
        }

        private List<string> CheckOriginSelling(FeesInOriginSelling oSelling)
        {
            List<string> errors = new List<string>();
            if (oSelling.IsChina)
            {
                if (oSelling.Others.Value == 0
                   && oSelling.CIQ.Value == 0
                   && oSelling.CO.Value == 0
                   && oSelling.Courier.Value == 0
                   && oSelling.CustomsClearance.Value == 0
                   && oSelling.Insurance.Value == 0
                   && oSelling.SealFees.Value == 0
                   && oSelling.THC.Value == 0
                   && oSelling.Truck.Value == 0)
                {
                    errors.Add("Origin Selling Net amounts Missing");
                }
            }
            else
            {
                if(oSelling.EuropeAllIn.Value == 0)
                {
                    errors.Add("Origin Selling Net amounts Missing");
                }
            }
            return errors;
        }


        private List<string> ValidateTranToBeAccountant(Tran tran)
        {
            List<string> errors = new List<string>();
            return errors;
        }

        private List<string> ValidateTranToBeOperations(Tran tran)
        {
            List<string> errors = new List<string>();
            return errors;
        }

    }


}