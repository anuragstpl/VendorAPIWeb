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
    
    public partial class ServiceProviderIssue
    {
        public int SPIssueID { get; set; }
        public string VendorCode { get; set; }
        public string IssueItem { get; set; }
        public string Issue { get; set; }
        public string Owner { get; set; }
    
        public virtual ServiceProvider ServiceProvider { get; set; }
    }
}
