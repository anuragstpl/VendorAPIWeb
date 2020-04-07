using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class IssuesEntity
    {
        public string VendorCode { get; set; }
        public string Item { get; set; }
        public string Issue { get; set; }
        public string Owner { get; set; }
    }
}
