//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.ServiceProviderIssues = new HashSet<ServiceProviderIssue>();
        }
    
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<int> UserRoleID { get; set; }
    
        public virtual ICollection<ServiceProviderIssue> ServiceProviderIssues { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
