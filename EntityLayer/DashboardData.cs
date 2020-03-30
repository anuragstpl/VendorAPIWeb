using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class DashboardData
    {
        public int ProvidersInProduction { get; set; }
        public int ProviderWentLive { get; set; }
        public int NumberOfSPTesting { get; set; }
        public int SPBeingWhiteListed { get; set; }
        public int QAGoLive { get; set; }
        public int SPBilled { get; set; }
        public string BilledAmount { get; set; }
    }
}
