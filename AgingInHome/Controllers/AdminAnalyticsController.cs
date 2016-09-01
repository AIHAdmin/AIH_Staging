using AgingInHome.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Configuration;
using AgingInHome.Helpers;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using AgingInHome.BLL.Enums;
using AgingInHome.DAL;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Data;
using System.Xml;
using System.Security.Claims;
using System.Web.Security;
using DotNetOpenAuth.OAuth2;
using System.Text;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Json;
using System.Threading;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Analytics.v3.Data;
using Google.Apis.Analytics.v3;

namespace AgingInHome.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminAnalyticsController : Controller
    {
        
        public ActionResult Index()
        {
            getgoogleToken();
            GetMarkup();
            return View();
        }
        [NonAction]
        public void getgoogleToken()
        {
            string[] Scopes = { "https://www.googleapis.com/auth/analytics.readonly" };
            string GoogleFolder = "Google";
            try
            {
                string GoogleFolderpath = Server.MapPath("~/");
                //Set location for Google Token to be locally stored
                var googleTokenLocation = Path.Combine(GoogleFolderpath, GoogleFolder);
                string path = Server.MapPath("~/client_secret.json");

                DirectoryInfo dInfo = new DirectoryInfo(Server.MapPath("~/Google"));
                DirectorySecurity dSecurity = dInfo.GetAccessControl();
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                dInfo.SetAccessControl(dSecurity);

                //Load the Client Configuration in JSON Format as a stream which is used for API Calls
                var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                //This will create a Token Response User File on the GoogleFolder 
                // indicated on your Application
                var credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                   new ClientSecrets
                   {
                       ClientId = "1020508869686-0bl3se2g2bq53202km7el352baeckgsp.apps.googleusercontent.com",
                       ClientSecret = "j6wGj_t5vxAsoW-BG1iYOrsJ"
                   },
                    Scopes,
                    "user",
                    CancellationToken.None,
                  new FileDataStore(googleTokenLocation, true)).Result;
                if (credentials.Token.IsExpired(credentials.Flow.Clock))
                {
                    if (credentials.RefreshTokenAsync(CancellationToken.None).Result)
                    {
                        ViewBag.AccessToken = credentials.Token.AccessToken;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    ViewBag.AccessToken = credentials.Token.AccessToken;
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                //ViewBag.AccessToken = "5---" + sb.ToString();
            }
        }

        public ActionResult ActionManageMarkup()
        {
            return View();
        }

        public JsonResult ManageMarkup(bool includeInactive = false, bool _search = false, long nd = 0, int rows = 1, int page = 1, string sidx = "", string sord = "")  //Gets the todo Lists.
        {
            var _markupModel = new MarkupModel();
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            long TotalCount = 0;
            var todoListsResults = _markupModel.GetMarkup();
            int totalRecords = todoListsResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            //if (sord.ToUpper() == "DESC")
            //{
            //    todoListsResults = todoListsResults.OrderByDescending(s => s.Markup1);
            //    todoListsResults = todoListsResults.Skip(pageIndex * pageSize).Take(pageSize);
            //}
            //else
            //{
            //    todoListsResults = todoListsResults.OrderBy(s => s.Markup1);
            //    todoListsResults = todoListsResults.Skip(pageIndex * pageSize).Take(pageSize);
            //}
            var pages = TotalCount % rows == 0 ? TotalCount / rows : (TotalCount / rows) + 1;
            var result = new
            {
                Data = todoListsResults,
                Page = page,
                PageSize = rows,
                records = totalRecords,
                total = pages == 0 ? 1 : pages,
                SortColumn = sidx,
                SortOrder = sord
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string CreateMarkup(MarkupModel markupModel)
        {
            string msg = string.Empty;
            try
            {
                var _markupModel = new MarkupModel();
                if (markupModel.Id == 0)
                {
                    if (markupModel.Id == 0)
                    {
                        if (markupModel.IsDefault == true && markupModel.IsActive == true)
                        {
                            if (ResetdefaultMarkup())
                            {
                                _markupModel.AddMarkup(markupModel);
                                msg = "Saved Successfully";
                            }
                        }
                        else if (markupModel.IsDefault == true && markupModel.IsActive == false)
                        {
                            msg = "Default Markup should be active!";
                        }
                        else
                        {
                            _markupModel.AddMarkup(markupModel);
                            msg = "Saved Successfully";
                        }
                    }
                }
                return msg;
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        public string Edit(MarkupModel markupModel)
        {
            string msg = string.Empty;
            try
            {
                var _markupModel = new MarkupModel();
                if (markupModel.Id != 0)
                {
                    if (ModelState.IsValid)
                    {
                        if (markupModel.IsDefault == true && markupModel.IsActive ==true)
                        {
                            if (ResetdefaultMarkup())
                            {
                                new MarkupModel().UpdateMarkup(markupModel);
                                msg = "Updated Successfully";
                            }
                        }
                        else if (markupModel.IsDefault == true && markupModel.IsActive == false)
                        {
                            msg = "Default Markup should be active";
                        }
                        else
                        {
                            new MarkupModel().UpdateMarkup(markupModel);
                            msg = "Updated Successfully";
                        }
                    }
                    else
                    {
                        msg = "Validation data not successfull";
                    }
                }

            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        public string Delete(int Id)
        {
            string msg = string.Empty;
            try
            {
                new MarkupModel().DeleteMarkup(Id);
                msg = "Deleted successfully";
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        public void insertAnalytics(AnalyticStatsModel analyticStatsModel)
        {
            string msg = string.Empty;
            try
            {
                var _analyticStatsModel = new AnalyticStatsModel();
                if (analyticStatsModel.Id == 0)
                {
                    if (analyticStatsModel.Id == 0)
                    {
                        _analyticStatsModel.AddAnalyticStats(analyticStatsModel);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
        }

        public bool ResetdefaultMarkup()
        {
            bool retval= false;
            try
            {
                var _markupModel = new MarkupModel();
                new MarkupModel().ResetDefaultMarkup();
                retval = true;
            }
            catch (Exception ex)
            {
                retval = false;
            }
            return retval;
        }

        //public bool CreateProfile()
        //{
            
        //    Google.Apis.Analytics.v3.ManagementResource.WebpropertiesResource test = new ManagementResource.WebpropertiesResource();
        //    test.Insert(
        //        new Webproperty
        //           {
        //               WebsiteUrl = "Aginginhome.com",
        //               Name = "Deepinder"
        //           }, "81499090");
        //}

        public void GetMarkup()
        {
            var _markupModel = new MarkupModel();
            var todoListsResults = _markupModel.GetDefaultMarkup();
            ViewBag.Markup = todoListsResults.Markup1;
        }

        public string AddProviderAnalytics(AnalyticsModel analyticsModel)
        {
            string msg = string.Empty;
            try
            {
                var _analyticsModel = new AnalyticsModel();
                if (analyticsModel.Id == 0)
                {
                    _analyticsModel.AddProviderAnalytic(analyticsModel);
                    msg = "Saved Successfully";
                }
                return msg;
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        public string EditProviderAnalytics(AnalyticsModel analyticsModel)
        {
            string msg = string.Empty;
            try
            {
                var _analyticsModel = new AnalyticsModel();
                if (analyticsModel.Id != 0)
                {
                    if (ModelState.IsValid)
                    {

                        new AnalyticsModel().UpdateProviderAnalytic(analyticsModel);
                        msg = "Updated Successfully";
                    }
                    else
                    {
                        msg = "Validation data not successfull";
                    }
                }

            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        public string DeleteProviderAnalytics(int Id)
        {
            string msg = string.Empty;
            try
            {
                new AnalyticsModel().DeleteProviderAnalytic(Id);
                msg = "Deleted successfully";
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        public void GetProviderAnalytics()
        {
            var _analyticsModel = new AnalyticsModel();
            var todoListsResults = _analyticsModel.GetProviderAnalytic();
        }

        public JsonResult ManageProviderAnalytic(bool includeInactive = false, bool _search = false, long nd = 0, int rows = 1, int page = 1, string sidx = "", string sord = "")  //Gets the todo Lists.
        {
            var _analyticsModel = new AnalyticsModel();
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            long TotalCount = 0;
            var todoListsResults = _analyticsModel.GetProviderAnalytic();
            int totalRecords = todoListsResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            var pages = TotalCount % rows == 0 ? TotalCount / rows : (TotalCount / rows) + 1;
            var result = new
            {
                Data = todoListsResults,
                Page = page,
                PageSize = rows,
                records = totalRecords,
                total = pages == 0 ? 1 : pages,
                SortColumn = sidx,
                SortOrder = sord
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActionManageProviderAnalytic()
        {
            return View();
        }
	}
}