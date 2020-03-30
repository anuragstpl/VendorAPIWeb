using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorAPI.Models
{
    public class Issues
    {
        public string ItemNo { get; set; }
        public string Issue { get; set; }
        public string Owner { get; set; }
    }
}