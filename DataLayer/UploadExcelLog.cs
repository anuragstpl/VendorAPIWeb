//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VendorAPI.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class UploadExcelLog
    {
        public int ID { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<System.DateTime> UploadDate { get; set; }
    
        public virtual User User { get; set; }
    }
}
