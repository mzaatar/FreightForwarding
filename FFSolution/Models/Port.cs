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
    
    public partial class Port
    {
        public Port()
        {
            this.Tran = new HashSet<Tran>();
            this.Tran1 = new HashSet<Tran>();
        }
    
        public int PortID { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string Updator { get; set; }
        public string PortName { get; set; }
        public int CountryID { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual ICollection<Tran> Tran { get; set; }
        public virtual ICollection<Tran> Tran1 { get; set; }
    }
}
