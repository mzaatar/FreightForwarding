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
    using System.ComponentModel.DataAnnotations;
    [MetadataType(typeof(TranMetaData))]
    public partial class Tran
    {
        public TranContainersViewModel containers = new TranContainersViewModel();
    }
    public partial class TranMetaData
    {    
        public int TranID { get; set; }

        //[DataType(DataType.Date)]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-M-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Updated { get; set; }
        public string Updator { get; set; }

         [System.ComponentModel.DisplayName("Transaction Reference Number")]
        public string TranRefNumber { get; set; }

        //[DataType(DataType.Date)]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         [System.ComponentModel.DisplayName("Reservation Date")]
        public Nullable<System.DateTime> ReservationDate { get; set; }

        //[DataType(DataType.Date)]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         [System.ComponentModel.DisplayName("Booking Date")]
        public Nullable<System.DateTime> BookingDate { get; set; }


        [System.ComponentModel.DisplayName("B/L No")]
        public string BLNo { get; set; }
        public Nullable<int> ShipperID { get; set; }

        [Required]
        public Nullable<int> ConsigneeID { get; set; }

         [System.ComponentModel.DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        public Nullable<int> AgentID { get; set; }
        public Nullable<int> SalesManID { get; set; }
        
        public Nullable<int> CommodityID { get; set; }
        public int ShippingTermID { get; set; }
        [Required]
        public int OriginCountryID { get; set; }
        public int POL { get; set; }
        public int POD { get; set; }
        [DataType(DataType.Date),DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        
        public Nullable<System.DateTime> ETD { get; set; }
        [DataType(DataType.Date),DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ETA { get; set; }
        [System.ComponentModel.DisplayName("T/T")]
        public Nullable<int> TT { get; set; }

         [System.ComponentModel.DisplayName("Final Distination")]
        public string FinalDistination { get; set; }
        public string Notes { get; set; }
        
        [Required]
        public Nullable<int> CourierID { get; set; }


        public virtual Agent Agent { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual Country Country { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Customer Customer1 { get; set; }
        public virtual FeesInDischargePortNet FeesInDischargePortNet { get; set; }
        public virtual FeesInDischargePortSelling FeesInDischargePortSelling { get; set; }
        public virtual FeesInOriginNet FeesInOriginNet { get; set; }
        public virtual FeesInOriginSelling FeesInOriginSelling { get; set; }
        public virtual Port Port { get; set; }
        public virtual Port Port1 { get; set; }
        public virtual Courier Courier { get; set; }
        public virtual SalesMan SalesMan { get; set; }
        public virtual ShippingTerm ShippingTerm { get; set; }
        public virtual TranCharges TranCharges { get; set; }
        
        public virtual TranContainersViewModel TranContainer { get; set; }
        public virtual TranDetail TranDetail { get; set; }
        public virtual ICollection<TranProfit> TranProfit { get; set; }
        public virtual ICollection<TranTotals> TranTotals { get; set; }
    }
}
