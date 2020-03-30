using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorAPI.Models
{
    public class ServiceProvider
    {
        public string ProviderName { get; set; }
        public string GoLiveDate { get; set; }
        public string ProjectManager { get; set; }
        public string Phase { get; set; }
        public string Fees { get; set; }
        public string Type { get; set; }
        public string Update { get; set; }
        public List<Issues> IssuesList { get; set; }
    }
}