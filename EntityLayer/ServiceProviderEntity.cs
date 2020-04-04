using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class ServiceProviderEntity
    {
        public string Name { get; set; }
        public Nullable<System.DateTime> GoLiveDate { get; set; }
        public string ProjectManager { get; set; }
        public string Phase { get; set; }
        public decimal Fees { get; set; }
        public string Type { get; set; }
        public string Update { get; set; }
        public List<IssuesEntity> IssuesList { get; set; }
        public string Status { get; set; }
    }
}
