using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FFSolution.Models
{
    [MetadataType(typeof(TranContainerMetaData))]
    public partial class TranContainer
    {

    }
    public class TranContainerMetaData
    {
        public int TranContainerID { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string Updator { get; set; }
        public int TranID { get; set; }
        public int ShippingTypeID { get; set; }
        [System.ComponentModel.DisplayName("Number of Containers of this type")]
        public int Count { get; set; }
        [System.ComponentModel.DisplayName("Line Shipping Charge")]
        public Nullable<decimal> LineShippingCharge { get; set; }

        [System.ComponentModel.DisplayName("Line Shipping Charge Currency")]
        public Nullable<int> LineShippingChargeCurrencyID { get; set; }

        [System.ComponentModel.DisplayName("Line Shipping Charge Selling")]
        public Nullable<decimal> LineShippingChargeSelling { get; set; }
        [System.ComponentModel.DisplayName("Line Shipping Charge Selling Currency")]
        public Nullable<int> LineShippingChargeSellingCurrencyID { get; set; }
        [System.ComponentModel.DisplayName("Line Shipping Charge Total")]
        public Nullable<decimal> LineShippingChargeTotal { get; set; }
        [System.ComponentModel.DisplayName("Line Shipping Charge Selling Total")]
        public Nullable<decimal> LineShippingChargeSellingTotal { get; set; }

        public virtual ShippingType ShippingType { get; set; }
        public virtual Tran Tran { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Currency Currency1 { get; set; }
    }
}