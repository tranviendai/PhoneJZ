//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhoneJZ.App_Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public Nullable<int> productID { get; set; }
        public Nullable<int> detailID { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<decimal> unitPrice { get; set; }
    
        public virtual OrderPro OrderPro { get; set; }
        public virtual Product Product { get; set; }
    }
}