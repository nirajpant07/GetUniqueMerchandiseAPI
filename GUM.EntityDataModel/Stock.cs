//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GUM.EntityDataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stock
    {
        public long ProductID { get; set; }
        public long SizeID { get; set; }
        public int Quantity { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
    }
}
