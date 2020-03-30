using DataLayer.DataHelper;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendorAPI.Models;
using ClosedXML.Excel;
using System.Data;
using System.IO;

namespace VendorAPI.Controllers
{
    public class HomeController : Controller
    {
        ServiceProviderHelper serviceProviderHelper = new ServiceProviderHelper();
        UserHelper userHelper = new UserHelper();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserEntity userEntity)
        {
            string viewName = "";
            bool isUserValid = userHelper.LoginUser(userEntity);
            if (isUserValid)
            {
                viewName = "Dashboard";
            }
            else
            {
                viewName = "Index";
                ViewBag.Error = "Incorrect UserID or Password.";
            }
            return RedirectToAction(viewName);
        }

        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = new DataTable("ServiceProviders");
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("Name"),
new DataColumn("Status"),
new DataColumn("GoLiveDate"),
new DataColumn("ProjectManager"),
            new DataColumn("Phase"),
            new DataColumn("Fees"),
            new DataColumn("Type"),
            new DataColumn("Update")});

            List<ServiceProviderEntity> serviceProviders = serviceProviderHelper.GetServiceProviders();

            foreach (var serviceProvider in serviceProviders)
            {
                dt.Rows.Add(serviceProvider.ProviderName, serviceProvider.Status, serviceProvider.GoLiveDate, serviceProvider.ProjectManager, serviceProvider.Phase, serviceProvider.Fees, serviceProvider.Type, serviceProvider.Update);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Details.xlsx");
                }
            }
        }

        public ActionResult DashBoard()
        {
            DashboardData dashboardData = serviceProviderHelper.GetDashboardData();
            ViewBag.BilledAmount = dashboardData.BilledAmount;
            ViewBag.NumberOfSPTesting = dashboardData.NumberOfSPTesting;
            ViewBag.ProvidersInProduction = dashboardData.ProvidersInProduction;
            ViewBag.ProviderWentLive = dashboardData.ProviderWentLive;
            ViewBag.QAGoLive = dashboardData.QAGoLive;
            ViewBag.SPBilled = dashboardData.SPBilled;
            List<ServiceProviderEntity> serviceProviders = serviceProviderHelper.GetServiceProviders();
            return View(serviceProviders);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}