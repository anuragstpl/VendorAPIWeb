using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class ServiceProviderEntity
    {
        public string ProviderName { get; set; }
        public string GoLiveDate { get; set; }
        public string ProjectManager { get; set; }
        public string Phase { get; set; }
        public string Fees { get; set; }
        public string Type { get; set; }
        public string Update { get; set; }
        public List<IssuesEntity> IssuesList { get; set; }
        public string Status { get; set; }
    }
}
