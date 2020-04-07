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
                        VendorCode = sprd.tpr.ppr.upr.spr.VendorCode,
                        Fees = sprd.tpr.ppr.upr.spr.Fees,
                        GoLiveDate = sprd.tpr.ppr.upr.spr.GoLiveDate,
                        Name = sprd.tpr.ppr.upr.spr.Name.ToString(),
                        Update = sprd.tpr.ppr.upr.spr.Update.ToString(),
                        StatusId = sprd.tpr.ppr.upr.stp.StatusID,
                        Status = sprd.tpr.ppr.upr.stp.StatusName,
                        ProjectManager = sprd.tpr.ppr.utp.Username,
                        PhaseId = sprd.tpr.ptp.PhaseID,
                        Phase = sprd.tpr.ptp.Phase1,
                        TypeId = sprd.ttp.TypeID,
                        Type = sprd.ttp.Type1,
                        IssuesList = sprd.tpr.ppr.upr.spr.ServiceProviderIssues.Select(spis => new IssuesEntity
                        {
                            VendorCode = spis.VendorCode,
                            Issue = spis.Issue,
                            Item = spis.IssueItem,
                            Owner = spis.Owner
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
                    dashboardData.SPBeingWhiteListed = serviceProviders.Where(x => x.PhaseId == 5).Count();
                    dashboardData.NumberOfSPTesting = serviceProviders.Where(x => x.PhaseId == 1).Count();
                    dashboardData.ProvidersInProduction = serviceProviders.Where(x => x.StatusId == 3).Count();
                    dashboardData.ProviderWentLiveLastWeek = serviceProviders.Where(x => x.StatusId == 3).Count();
                    dashboardData.QAGoLive = serviceProviders.Where(x => x.PhaseId == 2).Count();

                    int noOfSPBillcount = 0;
                    double Totabill = 0.00;
                    foreach (var item in serviceProviders)
                    {
                        try
                        {
                            Convert.ToDouble(item.Fees.Replace("$", ""));
                            noOfSPBillcount++;
                        }
                        catch { }
                        try
                        {
                            Convert.ToDouble(item.Fees.Replace("$", ""));
                            Totabill += Convert.ToDouble(item.Fees.Replace("$", ""));
                        }
                        catch { }
                    }
                    dashboardData.NoOfSPBilled = noOfSPBillcount;
                    dashboardData.SPBilledTotalAmount = String.Format("{0:#,0.00}", Totabill);
                }
                catch (Exception ex)
                {
                }
            }
            return dashboardData;
        }

        public void AddProvider(List<ServiceProviderEntity> serviceProviderEntityList)
        {
            try
            {
                ServiceProvider serviceProvider = null;
                ServiceProviderIssue serviceProviderIssue = null;

                using (uow = new VendorAPIRepository())
                {
                    if (serviceProviderEntityList != null && serviceProviderEntityList.Count > 0)
                    {
                        foreach (var serviceProviderEntity in serviceProviderEntityList)
                        {
                            bool isStatusExists = uow.StatusRepository.Get().Any(x => x.StatusName == serviceProviderEntity.Status);
                            if (!isStatusExists)
                            {
                                Status status = new Status();
                                status.StatusName = serviceProviderEntity.Status;
                                uow.StatusRepository.Insert(status);
                                uow.Save();
                            }

                            bool isPhaseExists = uow.PhaseRepository.Get().Any(x => x.Phase1 == serviceProviderEntity.Phase);
                            if (!isPhaseExists)
                            {
                                Phase phase = new Phase();
                                phase.Phase1 = serviceProviderEntity.Phase;
                                uow.PhaseRepository.Insert(phase);
                                uow.Save();
                            }

                            bool isTypeExists = uow.TypeRepository.Get().Any(x => x.Type1 == serviceProviderEntity.Type);
                            if (!isTypeExists)
                            {
                                Type type = new Type();
                                type.Type1 = serviceProviderEntity.Type;
                                uow.TypeRepository.Insert(type);
                                uow.Save();
                            }

                            serviceProvider = uow.ServiceProviderRepository.Get().Where(x => x.VendorCode == serviceProviderEntity.VendorCode).FirstOrDefault();

                            if (serviceProvider != null)
                            {
                                serviceProvider.Name = serviceProviderEntity.Name;
                                serviceProvider.StatusID = uow.StatusRepository.Get().Where(s => s.StatusName.Trim() == serviceProviderEntity.Status.Trim()).Select(y => y.StatusID).FirstOrDefault();
                                serviceProvider.GoLiveDate = serviceProviderEntity.GoLiveDate;
                                serviceProvider.ProjectManagerID = 1;
                                serviceProvider.PhaseID = uow.PhaseRepository.Get().Where(s => s.Phase1.Trim() == serviceProviderEntity.Phase.Trim()).Select(y => y.PhaseID).FirstOrDefault();
                                serviceProvider.Fees = serviceProviderEntity.Fees;
                                serviceProvider.TypeID = uow.TypeRepository.Get().Where(s => s.Type1.Trim() == serviceProviderEntity.Type.Trim()).Select(y => y.TypeID).FirstOrDefault();
                                serviceProvider.Update = serviceProviderEntity.Update;

                                uow.ServiceProviderRepository.Update(serviceProvider);
                            }
                            else
                            {
                                serviceProvider = new ServiceProvider();
                                serviceProvider.VendorCode = serviceProviderEntity.VendorCode;
                                serviceProvider.Name = serviceProviderEntity.Name;
                                serviceProvider.StatusID = uow.StatusRepository.Get().Where(s => s.StatusName.Trim() == serviceProviderEntity.Status.Trim()).Select(y => y.StatusID).FirstOrDefault();
                                serviceProvider.GoLiveDate = serviceProviderEntity.GoLiveDate;
                                serviceProvider.ProjectManagerID = 1;
                                serviceProvider.PhaseID = uow.PhaseRepository.Get().Where(s => s.Phase1.Trim() == serviceProviderEntity.Phase.Trim()).Select(y => y.PhaseID).FirstOrDefault();
                                serviceProvider.Fees = serviceProviderEntity.Fees;
                                serviceProvider.TypeID = uow.TypeRepository.Get().Where(s => s.Type1.Trim() == serviceProviderEntity.Type.Trim()).Select(y => y.TypeID).FirstOrDefault();
                                serviceProvider.Update = serviceProviderEntity.Update;

                                uow.ServiceProviderRepository.Insert(serviceProvider);
                            }
                            uow.Save();

                            if (serviceProviderEntity.IssuesList != null && serviceProviderEntity.IssuesList.Count > 0)
                            {
                                foreach (var issue in serviceProviderEntity.IssuesList)
                                {
                                    serviceProviderIssue = uow.ServiceProviderIssueRepository.Get().Where(x => x.VendorCode == issue.VendorCode && x.IssueItem == issue.Item).FirstOrDefault();

                                    if (serviceProviderIssue != null)
                                    {
                                        serviceProviderIssue.VendorCode = serviceProviderEntity.VendorCode;
                                        serviceProviderIssue.IssueItem = issue.Item;
                                        serviceProviderIssue.Issue = issue.Issue;
                                        serviceProviderIssue.Owner = issue.Owner;
                                        uow.ServiceProviderIssueRepository.Update(serviceProviderIssue);
                                    }
                                    else
                                    {
                                        serviceProviderIssue = new ServiceProviderIssue();
                                        serviceProviderIssue.VendorCode = serviceProviderEntity.VendorCode;
                                        serviceProviderIssue.IssueItem = issue.Item;
                                        serviceProviderIssue.Issue = issue.Issue;
                                        serviceProviderIssue.Owner = issue.Owner;
                                        uow.ServiceProviderIssueRepository.Insert(serviceProviderIssue);
                                    }
                                }
                                uow.Save();
                            }
                        }
                        UploadExcelLog uploadExcelLog = new UploadExcelLog();
                        uploadExcelLog.UserId = 1;//need to dynamic
                        uploadExcelLog.UploadDate = DateTime.Now;
                        uow.UploadExcelLogRepository.Insert(uploadExcelLog);
                        uow.Save();
                    }
                }
            }
            catch (Exception ex) { }
        }
    }
}
