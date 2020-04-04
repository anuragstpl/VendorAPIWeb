using DataLayer.UnitOfWork;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataHelper
{
    public class ServiceProviderHelper
    {
        VendorAPIRepository uow = null;

        public List<ServiceProviderEntity> GetServiceProviders()
        {
            List<ServiceProviderEntity> serviceProviderEntities = new List<ServiceProviderEntity>();
            using (uow = new VendorAPIRepository())
            {
                serviceProviderEntities = uow.ServiceProviderRepository.Get()
                    .Join(uow.StatusRepository.Get(), spr => spr.StatusID, stp => stp.StatusID, (spr, stp) => new { spr, stp })
                    .Join(uow.UserRepository.Get(), upr => upr.spr.ProjectManagerID, utp => utp.UserID, (upr, utp) => new { upr, utp })
                    .Join(uow.PhaseRepository.Get(), ppr => ppr.upr.spr.PhaseID, ptp => ptp.PhaseID, (ppr, ptp) => new { ppr, ptp })
                    .Join(uow.TypeRepository.Get(), tpr => tpr.ppr.upr.spr.PhaseID, ttp => ttp.TypeID, (tpr, ttp) => new { tpr, ttp })
                    .Select(sprd => new ServiceProviderEntity
                    {
                        Fees = Convert.ToDecimal(sprd.tpr.ppr.upr.spr.Fees),
                        GoLiveDate = sprd.tpr.ppr.upr.spr.GoLiveDate,
                        Name = sprd.tpr.ppr.upr.spr.Name.ToString(),
                        Update = sprd.tpr.ppr.upr.spr.Update.ToString(),
                        Status = sprd.tpr.ppr.upr.stp.StatusName,
                        ProjectManager = sprd.tpr.ppr.utp.Username,
                        Phase = sprd.tpr.ptp.Phase1,
                        Type = sprd.ttp.Type1,
                        IssuesList = sprd.tpr.ppr.upr.spr.ServiceProviderIssues.Select(spis => new IssuesEntity
                        {
                            Issue = spis.Issue,
                            ItemNo = spis.IssueItem,
                            Owner = spis.User.Username
                        }).ToList()
                    })
                    .ToList();
            }
            return serviceProviderEntities;
        }

        public DashboardData GetDashboardData()
        {
            DashboardData dashboardData = new DashboardData();
            using (uow = new VendorAPIRepository())
            {
                List<ServiceProviderEntity> serviceProviders = GetServiceProviders();
                try
                {
                    dashboardData.BilledAmount = serviceProviders.Sum(x => Convert.ToDouble(x.Fees)).ToString();
                    dashboardData.NumberOfSPTesting = serviceProviders.Where(x => x.Phase == "3").Count();
                    dashboardData.ProvidersInProduction = serviceProviders.Where(x => x.Status == "6").Count();
                    dashboardData.ProviderWentLive = serviceProviders.Where(x => x.Status == "6" || x.Status == "5").Count();
                    dashboardData.QAGoLive = serviceProviders.Where(x => x.Status == "4" || x.Status == "5").Count();
                    dashboardData.SPBilled = serviceProviders.Where(x => Convert.ToDouble(x.Fees) > 0).Count();
                }
                catch
                {

                }
            }
            return dashboardData;
        }

        public void AddProvider(ServiceProviderEntity serviceProviderEntity)
        {
            try
            {
                using (uow = new VendorAPIRepository())
                {
                    ServiceProvider serviceProvider = new ServiceProvider();

                    serviceProvider.Name = serviceProviderEntity.Name;
                    serviceProvider.StatusID = 2;// serviceProviderEntity.Status;
                    serviceProvider.GoLiveDate = serviceProviderEntity.GoLiveDate;
                    serviceProvider.ProjectManagerID = 1;// serviceProviderEntity.Name;
                    serviceProvider.PhaseID = 2;// serviceProviderEntity.Name;
                    serviceProvider.Fees = serviceProviderEntity.Fees;
                    serviceProvider.TypeID = 2;// serviceProviderEntity.Type;
                    serviceProvider.Update = serviceProviderEntity.Update;

                    uow.ServiceProviderRepository.Insert(serviceProvider);
                    uow.Save();
                }
            }
            catch (Exception ex) { }
        }
    }
}
