using ClosedXML.Excel;
using DataLayer.DataHelper;
using EntityLayer;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VendorAPI.Models;

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
            userEntity = userHelper.LoginUser(userEntity);

            if (userEntity.UserID > 0)
            {
                Session["UserID"] = userEntity.UserID;
                FormsAuthentication.SetAuthCookie(userEntity.Username, true);
                if (userEntity.UserRoleID == 1)
                {
                    viewName = "AdminDashboard";
                }
                else
                {
                    viewName = "Dashboard";
                }
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
                dt.Rows.Add(serviceProvider.Name, serviceProvider.Status, serviceProvider.GoLiveDate, serviceProvider.ProjectManager, serviceProvider.Phase, serviceProvider.Fees, serviceProvider.Type, serviceProvider.Update);
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

        [Authorize]
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

        [Authorize]
        public ActionResult AdminDashboard()
        {
            return View();
        }

        public ActionResult UploadConfirmation()
        {
            return View();
        }

        public ActionResult UpdateProfile()
        {
            return RedirectToAction("UpdateUserProfile");
        }

        public ActionResult UpdateUserProfile()
        {
            UserProfileData userProfileData = new UserProfileData();

            userProfileData = userHelper.GetUserProfileData(Convert.ToInt32(Session["UserID"]));
            return View(userProfileData);
        }


        public ActionResult UpdateUserProfilePost(UserProfileData userProfileData)
        {
            try
            {
                userProfileData.UserId = Convert.ToInt32(Session["UserID"]);
                userHelper.UpdateUser(userProfileData);
                TempData["Message"] = "Profile updated successfuly.";
            }
            catch (Exception ex) { ViewBag.message = "some issue occured. please try again"; }
            return RedirectToAction("UpdateUserProfile", "Home");
        }

        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase FileUpload)
        {
            ServiceProviderHelper serviceProviderHelper = new ServiceProviderHelper();
            ServiceProviderEntity serviceProviderEntity = null;

            string viewName = "";
            try
            {
                if (FileUpload != null)
                {
                    if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        string filename = FileUpload.FileName;

                        if (filename.EndsWith(".xlsx") || filename.EndsWith(".xls"))
                        {
                            string targetpath = Server.MapPath("~/UploadExcel/");
                            FileUpload.SaveAs(targetpath + filename);
                            string pathToExcelFile = targetpath + filename;

                            string sheetName = "Sheet1";

                            var excelFile = new ExcelQueryFactory(pathToExcelFile);//
                            var providerDetails = from a in excelFile.Worksheet<ServiceProviderEntity>(sheetName) select a;

                            foreach (var provider in providerDetails)
                            {
                                if (provider.Name != null)
                                {
                                    DateTime? GoLiveDate = null;
                                    GoLiveDate = Convert.ToDateTime(provider.GoLiveDate);

                                    serviceProviderEntity = new ServiceProviderEntity();

                                    serviceProviderEntity.Name = provider.Name;
                                    serviceProviderEntity.Status = provider.Status;
                                    serviceProviderEntity.GoLiveDate = provider.GoLiveDate;
                                    serviceProviderEntity.ProjectManager = provider.ProjectManager;
                                    serviceProviderEntity.Phase = provider.Phase;
                                    serviceProviderEntity.Fees = provider.Fees;
                                    serviceProviderEntity.Type = provider.Type;
                                    serviceProviderEntity.Update = provider.Update;

                                    serviceProviderHelper.AddProvider(serviceProviderEntity);

                                    EmailSetting emailSetting = new EmailSetting();
                                    string Body = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/EmailTemplates/ExcelUploadMail.html"));

                                    bool isMailSend = emailSetting.SendMail("irfan.siddiqui@srmtechsol.com", "", Body);
                                    viewName = "uploadconfirmation";
                                }
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Only Excel file format is allowed";
                            viewName = "AdminDashboard";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Only Excel file format is allowed";
                        viewName = "AdminDashboard";
                    }
                }
                else
                {
                    ViewBag.Message = "Please select file.";
                    return RedirectToAction("AdminDashboard", "Home");
                }
            }
            catch (Exception ex) { ViewBag.message = "some issue occured. please try again"; }
            return RedirectToAction(viewName);
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

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}