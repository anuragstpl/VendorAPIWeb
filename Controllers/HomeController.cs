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

            if (userEntity!=null && userEntity.UserID > 0)
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
                TempData["Message"] = "Incorrect UserID or Password.";
            }
            return RedirectToAction(viewName);
        }

        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = new DataTable("ServiceProviders");
            dt.Columns.AddRange(
            new DataColumn[9] {
            new DataColumn("ServiceProviderCode"),
            new DataColumn("Name"),
            new DataColumn("Status"),
            new DataColumn("GoLiveDate"),
            new DataColumn("ProjectManager"),
            new DataColumn("Phase"),
            new DataColumn("Fees"),
            new DataColumn("Type"),
            new DataColumn("Update")});

            DataTable dtIssue = new DataTable("IssueList");
            dtIssue.Columns.AddRange(
            new DataColumn[5] {
            new DataColumn("ServiceProviderCode"),
            new DataColumn("Name"),
            new DataColumn("Item"),
            new DataColumn("Issue"),
            new DataColumn("Owner")});

            List<ServiceProviderEntity> serviceProviders = serviceProviderHelper.GetServiceProviders();

            foreach (var serviceProvider in serviceProviders)
            {
                dt.Rows.Add(serviceProvider.Name, serviceProvider.Status, serviceProvider.GoLiveDate, serviceProvider.ProjectManager, serviceProvider.Phase, serviceProvider.Fees, serviceProvider.Type, serviceProvider.Update);

                foreach (var issue in serviceProvider.IssuesList)
                {
                    dtIssue.Rows.Add(issue.ServiceProviderCode, serviceProvider.Name, issue.Item, issue.Issue, issue.Owner);
                }
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt); ds.Tables.Add(dtIssue);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ServiceProviderDetails.xlsx");
                }
            }
        }

        [Authorize]
        public ActionResult DashBoard()
        {
            DashboardData dashboardData = serviceProviderHelper.GetDashboardData();
            ViewBag.SPBilledTotalAmount = dashboardData.SPBilledTotalAmount;
            ViewBag.NumberOfSPTesting = dashboardData.NumberOfSPTesting;
            ViewBag.ProvidersInProduction = dashboardData.ProvidersInProduction;
            ViewBag.ProviderWentLiveLastWeek = dashboardData.ProviderWentLiveLastWeek;
            ViewBag.QAGoLive = dashboardData.QAGoLive;
            ViewBag.NoOfSPBilled = dashboardData.NoOfSPBilled;
            ViewBag.SPBeingWhiteListed = dashboardData.SPBeingWhiteListed;
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
                TempData["Message"] = "Profile updated successfully.";
            }
            catch (Exception ex) { TempData["Message"] = "some issue occured. please try again"; }
            return RedirectToAction("UpdateUserProfile", "Home");
        }

        public ActionResult AdminSettings()
        {
            return RedirectToAction("Settings");
        }

        public ActionResult Settings()
        {
            UserProfileData userProfileData = new UserProfileData();

            userProfileData = userHelper.GetUserProfileData(Convert.ToInt32(Session["UserID"]));
            return View(userProfileData);
        }

        public ActionResult SettingsPost(UserProfileData userProfileData)
        {
            try
            {
                userProfileData.UserId = Convert.ToInt32(Session["UserID"]);
                userHelper.UpdateEmail(userProfileData);
                TempData["Message"] = "Email updated successfuly.";
            }
            catch (Exception ex) { TempData["Message"] = "some issue occured. please try again"; }
            return RedirectToAction("Settings", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult ForgotPasswordPost(UserEntity userEntity)
        {
            try
            {
                userEntity = userHelper.GetPassword(userEntity);
                if (userEntity != null && !string.IsNullOrEmpty(userEntity.Password))
                {
                    EmailSetting emailSetting = new EmailSetting();
                    string Body = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/EmailTemplates/ForgotPasswordMail.html"));

                    bool isMailSend = emailSetting.SendMail(userEntity.Email, "", "Forgot Password Notification", Body.Replace("newpassword", userEntity.Password).Replace("currDate", DateTime.Now.ToString("MMMM dd, yyyy")));
                    if (isMailSend)
                        TempData["Message"] = "Your password sent on your email.";
                }
                else { TempData["Message"] = "Your email does not exists."; }
            }
            catch (Exception ex) { TempData["Message"] = "some issue occured. please try again."; }
            return RedirectToAction("ForgotPassword", "Home");
        }

        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase FileUpload)
        {
            ServiceProviderHelper serviceProviderHelper = new ServiceProviderHelper();
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

                            var excelFile = new ExcelQueryFactory(pathToExcelFile);//
                            var providerList = from provider in excelFile.Worksheet<ServiceProviderEntity>("ServiceProviderList") select provider;
                            var issueList = from issue in excelFile.Worksheet<IssuesEntity>("IssueList") select issue;

                            //ValidateExcelFile();
                            List<ServiceProviderEntity> serviceProviderEntityList = new List<ServiceProviderEntity>();

                            foreach (var provider in providerList)
                            {
                                if (provider.Name != null)
                                {
                                    ServiceProviderEntity serviceProviderEntity = new ServiceProviderEntity();

                                    serviceProviderEntity.ServiceProviderCode = provider.ServiceProviderCode;
                                    serviceProviderEntity.Name = provider.Name;
                                    serviceProviderEntity.Status = provider.Status;
                                    serviceProviderEntity.GoLiveDate = provider.GoLiveDate;
                                    serviceProviderEntity.ProjectManager = provider.ProjectManager;
                                    serviceProviderEntity.Phase = provider.Phase;
                                    serviceProviderEntity.Fees = provider.Fees;
                                    serviceProviderEntity.Type = provider.Type;
                                    serviceProviderEntity.Update = provider.Update;
                                    //serviceProviderEntity.IssuesList = issueList == null ? null : new List<IssuesEntity>() { new IssuesEntity() { Item = issueList.Where(x => x.ServiceProviderCode == provider.ServiceProviderCode).Select(y => y.Item).FirstOrDefault(), Issue = issueList.Where(x => x.ServiceProviderCode == provider.ServiceProviderCode).Select(y => y.Issue).FirstOrDefault(), Owner = issueList.Where(x => x.ServiceProviderCode == provider.ServiceProviderCode).Select(y => y.Owner).FirstOrDefault() } };

                                    List<IssuesEntity> IssuesEntityList = new List<IssuesEntity>();
                                    foreach (var issue in issueList.Where(i => i.ServiceProviderCode == provider.ServiceProviderCode))
                                    {
                                        if (issue.Issue != null)
                                        {
                                            IssuesEntity issuesEntity = new IssuesEntity();
                                            issuesEntity.ServiceProviderCode = issue.ServiceProviderCode;
                                            issuesEntity.Item = issue.Item;
                                            issuesEntity.Issue = issue.Issue;
                                            issuesEntity.Owner = issue.Owner;

                                            IssuesEntityList.Add(issuesEntity);
                                        }
                                    }

                                    if (IssuesEntityList.Count > 0)
                                        serviceProviderEntity.IssuesList = IssuesEntityList;
                                    serviceProviderEntityList.Add(serviceProviderEntity);
                                }
                            }

                            if (serviceProviderEntityList.Count > 0)
                            {
                                serviceProviderHelper.AddProvider(serviceProviderEntityList);

                                EmailSetting emailSetting = new EmailSetting();
                                string Body = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/EmailTemplates/ExcelUploadMail.html"));

                                bool isMailSend = emailSetting.SendMail("irfan.siddiqui@srmtechsol.com", "", "Provider Details Upload Notification", Body.Replace("currDate", DateTime.Now.ToString("MMMM dd, yyyy")));
                                viewName = "uploadconfirmation";
                            }
                        }
                        else
                        {
                            TempData["Message"] = "Only Excel file format is allowed";
                            viewName = "AdminDashboard";
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Only Excel file format is allowed";
                        viewName = "AdminDashboard";
                    }
                }
                else
                {
                    TempData["Message"] = "Please select file.";
                    return RedirectToAction("AdminDashboard", "Home");
                }
            }
            catch (Exception ex) { TempData["Message"] = "some issue occured. please try again"; }
            return RedirectToAction(viewName);
        }

        private string ValidateExcelFile(DataTable dataTable)
        {
            string errorMsg = string.Empty;

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][0].ToString() == "")
                {
                    int RowNo = i + 2;
                    errorMsg = "Please enter service provider name in row " + RowNo + "'"; 
                }
            }
            return errorMsg;
        }
        public ActionResult About()
        {
            TempData["Message"] = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            TempData["Message"] = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}