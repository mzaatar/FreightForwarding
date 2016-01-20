//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FFSolution.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TranCharges
    {
        public int TranID { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string Updator { get; set; }
        public Nullable<decimal> CommissionPercentage { get; set; }
        public Nullable<int> CommissionPercentageCurrencyID { get; set; }
        public Nullable<decimal> PaidCommission { get; set; }
        public Nullable<int> PaidCommissionCurrencyID { get; set; }
        public Nullable<decimal> BankPercentage { get; set; }
        public Nullable<int> BankPercentageCurrencyID { get; set; }
        public Nullable<decimal> BankTransferFees { get; set; }
        public Nullable<int> BankTransferFeesCurrencyID { get; set; }
        public Nullable<decimal> OtherFees { get; set; }
        public Nullable<int> OtherFeesCurrencyID { get; set; }
    
        public virtual Currency Currency { get; set; }
        public virtual Currency Currency1 { get; set; }
        public virtual Currency Currency2 { get; set; }
        public virtual Currency Currency3 { get; set; }
        public virtual Currency Currency4 { get; set; }
        public virtual Tran Tran { get; set; }
    }
}
